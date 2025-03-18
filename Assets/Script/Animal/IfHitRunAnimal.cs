using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfHitRunAnimal : RunAwayAnimal
{
    public override void TakeDamage(float _damage)
    {
        maxHealth -= _damage;
        if (maxHealth < 0)
        {
            Break();
        }

        StartCoroutine(CDamageFlash());

        SetState(EAIState.Running);
    }
}
