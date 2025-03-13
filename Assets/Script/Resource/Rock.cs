using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DropResource;
using UnityEngine.AI;

public class Rock : MonoBehaviour, IBreakableObject, IRespawnable
{
    public float maxHp = 100f;
    public float currentHp;

    public float respawnTime = 5f;

    public DropItem[] dropItems;

    private Collider collider_;
    private Renderer[] renderer_;
    private GameObject debrisRenderer;
    private GameObject debrisPrefab;
    private NavMeshObstacle navObstacle;

    private bool isBreak = false;

    void Awake()
    {
        currentHp = maxHp;

        collider_ = GetComponentInChildren<Collider>();
        renderer_ = GetComponentsInChildren<Renderer>();
        navObstacle = GetComponent<NavMeshObstacle>();

        if (dropItems == null || dropItems.Length == 0)
        {
            dropItems = new DropItem[3];

            dropItems[0] = new DropItem();
            dropItems[0].dropItemPrefab = Resources.Load<GameObject>("Prefabs/Rock/Flint");
            dropItems[0].minDropCount = 0;
            dropItems[0].maxDropCount = 2;

            dropItems[1] = new DropItem();
            dropItems[1].dropItemPrefab = Resources.Load<GameObject>("Prefabs/Rock/Nitre");
            dropItems[1].minDropCount = 0;
            dropItems[1].maxDropCount = 1;

            dropItems[2] = new DropItem();
            dropItems[2].dropItemPrefab = Resources.Load<GameObject>("Prefabs/Rock/ResourceRock");
            dropItems[2].minDropCount = 2;
            dropItems[2].maxDropCount = 3;
        }

        debrisPrefab = Resources.Load<GameObject>("Prefabs/Rock/RockDebris");
    }

    public void TakeDamage(float _damage)
    {
        if (isBreak)
            return;

        currentHp -= _damage;

        if (currentHp <= 0)
        {
            Break();
        }
    }

    public void Break()
    {
        isBreak = true;

        if (dropItems != null)
        {
            for (int i = 0; i < dropItems.Length; i++)
            {
                int dropCount = Random.Range(dropItems[i].minDropCount, dropItems[i].maxDropCount + 1);
                for (int j = 0; j < dropCount; j++)
                {
                    Vector3 dropPosition = new Vector3(
                        transform.position.x + Random.Range(-1f, 1f),
                        transform.position.y + 1,
                        transform.position.z + Random.Range(-1f, 1f)
                    );
                    Instantiate(dropItems[i].dropItemPrefab, dropPosition, Quaternion.identity);
                }
            }
        }

        if (collider_ != null)
            collider_.enabled = false;

        foreach (Renderer rd in renderer_)
        {
            rd.enabled = false;
        }

        if (debrisPrefab != null)
        {
            debrisRenderer = Instantiate(debrisPrefab, transform.position, transform.rotation, transform);
        }

        if (navObstacle != null)
            navObstacle.enabled = false;

        StartCoroutine(CRespawnCoroutine(respawnTime));
    }

    public IEnumerator CRespawnCoroutine(float _respawnTime)
    {
        yield return new WaitForSeconds(_respawnTime);

        currentHp = maxHp;
        isBreak = false;

        if (collider_ != null)
            collider_.enabled = true;

        foreach (Renderer rd in renderer_)
        {
            rd.enabled = true;
        }

        if (navObstacle != null)
            navObstacle.enabled = true;

        if (debrisRenderer != null)
        {
            Destroy(debrisRenderer);
        }
    }
}