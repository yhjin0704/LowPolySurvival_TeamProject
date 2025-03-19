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
        PlayerManager.Instance.Player.controller.ChangeAnimatior(-1);
        PlayerManager.Instance.Player.controller.SetDamage(punchDamage);
        PlayerManager.Instance.Player.controller.SetAttackStamina(punchStamina);
    }
}

public class PlayerAttackEquip : PlayerEquipState
{
    public void ChangeController(ItemData data)
    {
        PlayerManager.Instance.Player.controller.ChangeAnimatior(data.ID);
        PlayerManager.Instance.Player.controller.SetDamage(data.damage);
        PlayerManager.Instance.Player.controller.SetAttackStamina(data.useStamina);
    }

    public void ChangeController()
    {
        return;
    }
}

public class PlayerCupEquip : PlayerEquipState
{
    public void ChangeController(ItemData data)
    {
        PlayerManager.Instance.Player.controller.ChangeAnimatior(data.ID);
        PlayerManager.Instance.Player.controller.SetDamage(data.damage);
        PlayerManager.Instance.Player.controller.SetAttackStamina(data.useStamina);
    }

    public void ChangeController()
    {
    }
}

