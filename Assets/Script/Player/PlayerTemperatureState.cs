using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdState : PlayerTemperartureState
{
    public void ChangeTemperarture()
    {
        PlayerManager.Instance.Player.controller.isTempDamaged = true;
    }
}

public class NormalState : PlayerTemperartureState
{

    public void ChangeTemperarture()
    {
        PlayerManager.Instance.Player.condition.ResetThirstValuePlus();
        PlayerManager.Instance.Player.controller.isTempDamaged = false;
    }
}

public class HotState : PlayerTemperartureState
{
    private float thirsty = 5f;

    public void ChangeTemperarture()
    {
        PlayerManager.Instance.Player.controller.isTempDamaged = true;
        PlayerManager.Instance.Player.condition.ThirstValuePlus(thirsty);
    }
}