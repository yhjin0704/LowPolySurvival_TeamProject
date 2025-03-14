using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : PlayerEquipState
{
    private float punchDamage = 5f;
    public void ChangeController()
    {
        PlayerManager.Instance.Player.controller.DisableAllEquipItem();
        PlayerManager.Instance.Player.controller.ChangePunchAnimator();
        PlayerManager.Instance.Player.controller.SetDamage(punchDamage);
    }
}
