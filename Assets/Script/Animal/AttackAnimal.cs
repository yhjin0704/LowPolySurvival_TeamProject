using DropResource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackAnimal : Animal
{

    [Header("Combat")]
    public int damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;

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
            case EAIState.Attacking:
                AttackingUpdate();
                break;
        }
    }

    public override void SetState(EAIState state)
    {
        aiState = state;

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
            case EAIState.Attacking:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }

        animator.speed = agent.speed / walkSpeed;
    }

    protected override void PassiveUpdate()
    {
        base.PassiveUpdate();

        if (playerDistance < detectDistance)
        {
            SetState(EAIState.Attacking);
        }
    }

    void AttackingUpdate()
    {
        if (playerDistance < attackDistance && IsPlayerInFieldOfView())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate)
            {
                animator.speed = 1;
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time;
                PlayerManager.Instance.Player.condition.TakeDamage(damage);
            }
        }
        else
        {
            if (playerDistance < detectDistance)
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();

                if (agent.CalculatePath(PlayerManager.Instance.Player.transform.position, path))
                {
                    agent.SetDestination(PlayerManager.Instance.Player.transform.position);
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(EAIState.Wandering);
                }
            }
            else
            {
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                SetState(EAIState.Wandering);
            }
        }
    }
}