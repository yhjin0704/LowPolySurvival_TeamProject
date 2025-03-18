using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEditor.Animations;
using DropResource;
using System.Linq;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;
    PlayerPunch punchState;
    PlayerAttackEquip equipState;
    PlayerState playerState;
    private PlayerCondition condition;

    private GameObject equipSword;
    private GameObject equipAxe;
    private List<Transform> equipPos;

    [Header("Override Animator")]
    private Animator animator;
    public AnimatorController defaultController;
    public AnimatorOverrideController swordController;

    [Header("Movement")]
    private Vector2 curMovementInput;
    public float moveSpeed;
    public float dashSpeed;
    public float jumpPower;
    public float resultSpeed;
    public float dashStamina;
    public float jumpStamina;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    [Header("Combat")]
    public float attackRate;
    private bool attacking;
    private bool LeftPunch;
    public float attackDistance;
    private float nowDamage;
    public float attackStamina;
    public LayerMask hitLayer;
    private bool isEquip = false;

    private Vector2 mouseDelta;

    private Camera camera;

    [HideInInspector]
    public bool canLook = true;
    public bool isDash = false;
    public Action inventory;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        equipPos = GameObject.Find("EquipPos").GetComponentsInChildren<Transform>().Where(t => t != transform).ToList();
        equipPos.RemoveAt(0);
        //Debug.Log(equipPos[0].name);
        equipSword = GameObject.Find("EquipPos").transform.Find("Equip_Sword").gameObject;
        equipAxe = GameObject.Find("EquipPos").transform.Find("Equip_Axe").gameObject;
        equipPos.Add(equipSword.transform);
        equipPos.Add(equipAxe.transform);

        punchState = new PlayerPunch();
        equipState = new PlayerAttackEquip();
        playerState = new PlayerState(punchState);
        playerState.Change();
        Cursor.lockState = CursorLockMode.Locked;
        resultSpeed = moveSpeed;
        camera = Camera.main;
        condition = PlayerManager.Instance.Player.condition;

    }

    private void Update()
    {
        IsGrounded();
    }
    private void FixedUpdate()
    {
        Move();
        if (isDash)
        {
            condition.ConsumeStamina(dashStamina);
        }

        if (condition.IsStaminaZero() || Approximately(rigidbody.velocity, Vector3.zero, 0.1f))
        {
            ToggleDash();
        }
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            animator.SetTrigger("Jump");
            rigidbody.AddForce(Vector2.up * (jumpPower), ForceMode.Impulse);
            condition.ConsumeStamina(jumpStamina);
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            ToggleDash();
        }
    }

    void ToggleDash()
    {
        if (!isDash && !condition.IsStaminaZero())
        {
            resultSpeed = dashSpeed * moveSpeed;
            if(!Approximately(rigidbody.velocity, Vector3.zero, 0.1f))
            {
                isDash = true;
                animator.SetBool("IsDash", true);
            }
        }
        else
        {
            resultSpeed = moveSpeed;
            isDash = false;
            animator.SetBool("IsDash", false);
        }
    }


    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= resultSpeed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;

        if(!Approximately(rigidbody.velocity, Vector3.zero, 0.1f))
        {
            animator.SetBool("IsWalk", true);
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                animator.SetBool("IsGround", true);
                return true;
            }
        }
        animator.SetBool("IsGround", false);
        return false;
    }

    public void OnAttackInput()
    {
        if (!attacking && !condition.IsStaminaZero())
        {
            attacking = true;
            if (!LeftPunch)
            {
                animator.SetTrigger("LeftPunch");
                LeftPunch = true;
            }
            else
            {
                animator.SetTrigger("RightPunch");
                LeftPunch = false;
            }

            Invoke(nameof(OnCanAttack), attackRate);
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }

    public void OnHit()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //Ray ray = new Ray(transform.position + new Vector3(0, 0.5f,0), Vector3.forward * attackDistance);
        Debug.DrawRay(ray.origin, ray.direction, Color.white);
        RaycastHit hit;

        condition.ConsumeStamina(attackStamina);

        if (Physics.Raycast(ray, out hit, attackDistance, hitLayer))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.TryGetComponent(out IBreakableObject breakbleObject))
            {
                Debug.Log("ÆÝÄ¡ÆÝÄ¡");
                breakbleObject.TakeDamage(nowDamage);
            }
        }
    }

    public void OnInventoryButton(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void ChangeAnimatior(int id)
    {
        switch (id)
        { 
            case 100:
                animator.runtimeAnimatorController = swordController;
                break;
            case 101:
                animator.runtimeAnimatorController = swordController;
                break;
            default:
                animator.runtimeAnimatorController = defaultController;
                break;
        }
    }

    public void SetDamage(float damage)
    {
        nowDamage = damage;
    }

    public void SetAttackStamina(float value)
    {
        attackStamina = value;
    }

    public void DisableAllEquipItem()
    {
        foreach (Transform objects in equipPos)
        {
            objects.gameObject.SetActive(false);
        }
    }

    public void UnEquip()
    {
        DisableAllEquipItem();
        isEquip = false;
        playerState.setState(punchState);
        playerState.Change();
    }

    public void EquipAttackState(ItemData data)
    {
        playerState.setState(equipState);
        playerState.Change(data);
    }

    public void EquipAttack(ItemData data)
    {
        switch (data.ID)
        {
            case 100:
                EquipAttackState(data);
                if (isEquip == false)
                {
                    isEquip = true;
                    equipSword.SetActive(true);
                }
                break;
            case 101:
                EquipAttackState(data);
                if (isEquip == false)
                {
                    isEquip = true;
                    equipAxe.SetActive(true);
                }
                break;
        }
    }

    public bool Approximately(Vector3 a, Vector3 b, float threshold)
    {
        float dist = Vector3.Distance(a, b);
        return dist <= threshold;
    }
}