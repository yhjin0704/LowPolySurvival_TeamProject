using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : PlayerEquipState
{
    public void ChangeController()
    {
        PlayerManager.Instance.Player.controller.ChangeSwordAnimator();
    }
}

