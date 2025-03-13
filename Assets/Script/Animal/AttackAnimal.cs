using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EAIState
{
    Idle,
    Wandering,
    Attacking
}

public class AttackAnimal : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public ItemData[] dropOnDepth;

    [Header("AI")]
    private NavMeshAgent agent;
    public float detectDistance; // 목표 지점까지의 최소 거리
    private EAIState aiState;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    [Header("Combat")]
    public int damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;

    private float playerDistance;

    public float fieldIfView = 120f;

    private Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        SetState(EAIState.Wandering);
    }

    void Update()
    {
        //playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);

        animator.SetBool("Moving", aiState != EAIState.Idle);

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

    public void SetState(EAIState state)
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

    void PassiveUpdate()
    {
        if (aiState == EAIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(EAIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        if (playerDistance < detectDistance)
        {
            SetState(EAIState.Attacking);
        }
    }

    void WanderToNewLocation()
    {
        if (aiState != EAIState.Idle) return;

        SetState(EAIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30) break;
        }

        return hit.position;
    }

    void AttackingUpdate()
    {
        //if (playerDistance < attackDistance && IsPlayerInFieldOfView())
        //{
        //    agent.isStopped = true;
        //    if (Time.time - lastAttackTime > attackRate)
        //    {
        //        lastAttackTime = Time.time;
        //        Player.Instance.controller.GetComponent<IDamagable>().TakePhysicalDamage(damage);
        //    }
        //}
        //else
        //{
        //    if (playerDistance < detectDistance)
        //    {
        //        agent.isStopped = false;
        //        NavMeshPath path = new NavMeshPath();

        //        if (agent.CalculatePath(CharacterManager.Instance.Player.transform.position, path))
        //        {
        //            agent.SetDestination(CharacterManager.Instance.Player.transform.position);
        //        }
        //        else
        //        {
        //            agent.SetDestination(transform.position);
        //            agent.isStopped = true;
        //            SetState(EAIState.Wandering);
        //        }
        //    }
        //    else
        //    {
        //        agent.SetDestination(transform.position);
        //        agent.isStopped = true;
        //        SetState(EAIState.Wandering);
        //    }
        //}
    }

    //bool IsPlayerInFieldOfView()
    //{
    //    Vector3 directionToPlayer = CharacterManager.Instance.Player.transform.position - transform.position;
    //    float angle = Vector3.Angle(transform.forward, directionToPlayer);
    //    return angle < fieldIfView / 2;
    //}

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            Die();
        }

        StartCoroutine(DamageFlash());
    }

    void Die()
    {
        for (int i = 0; i < dropOnDepth.Length; i++)
        {
            Instantiate(dropOnDepth[i].dropPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    IEnumerator DamageFlash()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = new Color(1f, 0.6f, 0.6f);
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = Color.white;
        }
    }

}