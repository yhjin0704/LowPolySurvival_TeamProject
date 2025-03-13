using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : PlayerEquipState
{
    public void ChangeController()
    {
        PlayerManager.Instance.Player.controller.ChangePunchAnimator();
    }
}
