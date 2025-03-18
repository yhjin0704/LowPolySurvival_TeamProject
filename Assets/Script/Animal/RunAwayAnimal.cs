using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayAnimal : Animal
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        switch (aiState)
        {
            case EAIState.Idle:
            case EAIState.Wandering:
                PassiveUpdate();
                break;
            case EAIState.Running:
                break;
        }
    }

    protected override void PassiveUpdate()
    {
        base.PassiveUpdate();
    }

    public override void SetState(EAIState _state)
    {
        aiState = _state;

        switch (aiState)
        {
            case EAIState.Idle:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;
            case EAIState.Wandering:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                break;
            case EAIState.Running:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }

        animator.speed = agent.speed / walkSpeed;
    }
}
