using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum AttackEquip
{
    Punch,
    Sword,
    Axe
}

public class Equipment : MonoBehaviour
{
    public Equip curEquip;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = PlayerManager.Instance.Player.controller;
    }

    // Update is called once per frame
    public void OnAttackInput(InputAction.CallbackContext context)
    {         
        if (context.phase == InputActionPhase.Performed && controller.canLook)
        {
            controller.OnAttackInput();   
        }
    }
}
