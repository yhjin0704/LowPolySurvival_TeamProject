using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSword : PlayerEquipState
{
    private float swordDamage = 50f;
    public void ChangeController()
    {
        PlayerManager.Instance.Player.controller.DisableAllEquipItem();
        PlayerManager.Instance.Player.controller.ChangeSwordAnimator();
        PlayerManager.Instance.Player.controller.SetDamage(swordDamage);
        PlayerManager.Instance.Player.controller.ActiveSword();
    }
}

