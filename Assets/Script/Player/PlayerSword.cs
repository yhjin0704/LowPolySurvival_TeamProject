using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSword : PlayerEquipState
{
    private float swordDamage = 50f;
    private float swordStamina = 20f;
    public void ChangeController()
    {
        PlayerManager.Instance.Player.controller.DisableAllEquipItem();
        PlayerManager.Instance.Player.controller.ChangeSwordAnimator();
        PlayerManager.Instance.Player.controller.SetDamage(swordDamage);
        PlayerManager.Instance.Player.controller.SetAttackStamina(swordStamina);
        PlayerManager.Instance.Player.controller.ActiveSword();

    }
}

