using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerEquipState
{

    public abstract void ChangeController(ItemData data);
    public abstract void ChangeController();
}

public interface PlayerTemperartureState
{
    public abstract void ChangeTemperarture();
}

public class PlayerState
{
    private PlayerEquipState state;
    private PlayerTemperartureState temperartureState;

    public PlayerState(PlayerEquipState state, PlayerTemperartureState TemperartureState)
    {
        this.state = state;
        this.temperartureState = TemperartureState;
    }

    public void setState(PlayerEquipState state)
    {
        this.state = state;
    }

    public void SetTemperartureState(PlayerTemperartureState TemperartureState)
    {
        this.temperartureState = TemperartureState;
    }

    public void Change(ItemData data) 
    {
        PlayerManager.Instance.Player.controller.DisableAllEquipItem();
        state.ChangeController(data);
    }

    public void Change()
    {
        PlayerManager.Instance.Player.controller.DisableAllEquipItem();
        state.ChangeController();
    }
    public void TempChange()
    {
        temperartureState.ChangeTemperarture();
    }
}