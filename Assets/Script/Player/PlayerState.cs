using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerEquipState
{

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

    public void Change() 
    {
        state.ChangeController();
    }
}