using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfHitRunAnimal : RunAwayAnimal
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void PassiveUpdate()
    {
        base.PassiveUpdate();
    }

    public override void TakeDamage(float _damage)
    {
        health -= _damage;
        if (health < 0)
        {
            Break();
        }

        StartCoroutine(CDamageFlash());

        SetState(EAIState.Running);
    }
}
