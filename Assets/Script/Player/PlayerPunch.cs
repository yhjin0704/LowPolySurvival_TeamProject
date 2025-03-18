using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : PlayerEquipState
{
    private float punchDamage = 5f;
    private float punchStamina = 10f;
    public void ChangeController(ItemData data)
    {
        return;
    }

    public void ChangeController()
    {
        PlayerManager.Instance.Player.controller.ChangePunchAnimator();
        PlayerManager.Instance.Player.controller.SetDamage(punchDamage);
        PlayerManager.Instance.Player.controller.SetAttackStamina(punchStamina);
    }
}
