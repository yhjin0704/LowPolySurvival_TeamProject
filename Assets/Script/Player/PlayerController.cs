using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Animator animator;
    [Header("Movement")]
    private Vector2 curMovementInput;
    public float moveSpeed;
    public float dashSpeed;
    public float jumpPower;
    public float resultSpeed;
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
    public int punchDamage;

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
        Cursor.lockState = CursorLockMode.Locked;
        resultSpeed = moveSpeed;
        camera = Camera.main;
    }

    private void Update()
    {
        IsGrounded();
    }
    private void FixedUpdate()
    {
        Move();
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
            animator.SetBool("IsWalk", true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            animator.SetBool("IsWalk", false);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            animator.SetTrigger("Jump");
            rigidbody.AddForce(Vector2.up * (jumpPower), ForceMode.Impulse);
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
        if (isDash)
        {
            resultSpeed = moveSpeed;
            isDash = false;
            animator.SetBool("IsDash", false);
        }
        else
        {
            resultSpeed = dashSpeed * moveSpeed;
            isDash = true;
            animator.SetBool("IsDash", true);
        }
    }


    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= resultSpeed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
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
        if (!attacking)
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
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            Debug.Log("ÆÝÄ¡ÆÝÄ¡");
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
}
