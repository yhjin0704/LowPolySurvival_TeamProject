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

public class PlayerSword : PlayerEquipState
{
    public void ChangeController(ItemData data)
    {
        PlayerManager.Instance.Player.controller.ChangeSwordAnimator();
        PlayerManager.Instance.Player.controller.SetDamage(data.damage);
        PlayerManager.Instance.Player.controller.SetAttackStamina(data.useStamina);
        PlayerManager.Instance.Player.controller.ActiveSword();

    }

    public void ChangeController()
    {
        return;
    }
}

