using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfVeiwRunAnimal : RunAwayAnimal
{
    protected override void PassiveUpdate()
    {
        base.PassiveUpdate();

        if (playerDistance < detectDistance && IsPlayerInFieldOfView())
        {
            SetState(EAIState.Running);
        }
    }
}
