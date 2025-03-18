using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

