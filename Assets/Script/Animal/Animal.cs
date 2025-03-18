using DropResource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EAIState
{
    Idle,
    Wandering,
    Attacking,
    Running
}

public class Animal : MonoBehaviour, IBreakableObject
{
    [Header("Stats")]
    public float maxHealth;
    protected float currentHealth;
    public float walkSpeed;
    public float runSpeed;
    public DropItem[] dropOnDepth;

    [Header("AI")]
    protected NavMeshAgent agent;
    public float detectDistance; // 목표 지점까지의 최소 거리
    protected EAIState aiState;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    protected float playerDistance;

    public float fieldIfView = 120f;

    protected Animator animator;
    protected SkinnedMeshRenderer[] meshRenderers;

    protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;

        SetState(EAIState.Wandering);
    }

    protected virtual void Update()
    {
        playerDistance = Vector3.Distance(transform.position, PlayerManager.Instance.Player.transform.position);

        animator.SetBool("Moving", aiState != EAIState.Idle);

    }

    protected void OnEnable()
    {
        currentHealth = maxHealth;

        SetState(EAIState.Wandering);
    }

    protected virtual void PassiveUpdate()
    {
        if (aiState == EAIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(EAIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }
    }

    public virtual void SetState(EAIState _state)
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
        }

        animator.speed = agent.speed / walkSpeed;
    }

    protected void WanderToNewLocation()
    {
        if (aiState != EAIState.Idle) return;

        SetState(EAIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }

    protected Vector3 GetWanderLocation()
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

    protected bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = PlayerManager.Instance.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldIfView / 2;
    }

    public virtual void TakeDamage(float _damage)
    {
        maxHealth -= _damage;
        if (maxHealth < 0)
        {
            Break();
        }

        StartCoroutine(CDamageFlash());
    }

    public void Break()
    {
        for (int i = 0; i < dropOnDepth.Length; i++)
        {
            Instantiate(dropOnDepth[i].dropItemPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        }

        gameObject.SetActive(false);
    }

    protected IEnumerator CDamageFlash()
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
