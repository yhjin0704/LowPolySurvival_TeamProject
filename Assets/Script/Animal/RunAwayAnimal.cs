using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAwayAnimal : Animal
{
    [Header("RunAway")]
    public float runAwayDistance;

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
                RunningUpdate();
                break;
        }
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

    protected void RunningUpdate()
    {
        if (playerDistance > runAwayDistance)
        {
            agent.isStopped = true;
            {
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                SetState(EAIState.Wandering);
            }
        }
        else
        {
            agent.isStopped = false;

            Vector3 runAwayDir = transform.position - PlayerManager.Instance.Player.transform.position;
            runAwayDir.Normalize();

            Vector3 runAwayDestination = transform.position + runAwayDir * runAwayDistance;

            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(runAwayDestination, path))
            {
                agent.SetDestination(runAwayDestination);
            }
            else
            {
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                SetState(EAIState.Wandering);
            }
        }
    }

    public override void TakeDamage(float _damage)
    {
        maxHealth -= _damage;
        if (maxHealth < 0)
        {
            Break();
            return;
        }

        StartCoroutine(CDamageFlash());

        SetState(EAIState.Running);
    }
}
