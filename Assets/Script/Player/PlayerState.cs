using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerEquipState
{

    public abstract void ChangeController(ItemData data);
    public abstract void ChangeController();
}

public class PlayerState : MonoBehaviour
{
   private PlayerEquipState state;

    public PlayerState(PlayerEquipState state)
    {
        this.state = state;
    }

    public void setState(PlayerEquipState state)
    {
        this.state = state;
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

    public PlayerEquipState ReturnState()
    {
        return this.state;
    }

    public PlayerTemperartureState GetTempState()
    {
        return this.temperartureState;
    }
}