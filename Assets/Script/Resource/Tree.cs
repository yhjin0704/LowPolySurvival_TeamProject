using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DropResource;

public class Tree : MonoBehaviour, IBreakableObject, IRespawnable
{
    public float maxHp = 100f;
    public float currentHp;

    public float respawnTime = 60f;

    public DropItem[] dropItems;
    public GameObject fruitPrefab;

    private Collider collider_;
    private Renderer[] renderer_;
    private GameObject stumpRenderer;
    private GameObject stumpPrefab;

    private bool isBreak = false;

    void Awake()
    {
        currentHp = maxHp;

        collider_ = GetComponentInChildren<Collider>();
        renderer_ = GetComponentsInChildren<Renderer>();

        if (dropItems == null || dropItems.Length == 0)
        {
            dropItems = new DropItem[2];

            dropItems[0] = new DropItem();
            dropItems[0].dropItemPrefab = Resources.Load<GameObject>("Prefabs/Tree/Branch");
            dropItems[0].minDropCount = 0;
            dropItems[0].maxDropCount = 3;

            dropItems[1] = new DropItem();
            dropItems[1].dropItemPrefab = Resources.Load<GameObject>("Prefabs/Tree/Wood");
            dropItems[1].minDropCount = 1;
            dropItems[1].maxDropCount = 2;
        }

        if (fruitPrefab == null)
        {
            fruitPrefab = Resources.Load<GameObject>("Prefabs/Food/Apple");
        }

        stumpPrefab = Resources.Load<GameObject>("Prefabs/Tree/TreeStump");
    }

    public void TakeDamage(float _damage)
    {
        if (isBreak)
            return;

        currentHp -= _damage;
        int fruitDropProbability = Random.Range(0, 5);
        if (fruitDropProbability < 2)
        {
            Vector3 dropPosition = new Vector3(
                        transform.position.x + Random.Range(-1f, 1f),
                        transform.position.y + 1,
                        transform.position.z + Random.Range(-1f, 1f)
                    );
            Instantiate(fruitPrefab, dropPosition, Quaternion.identity);
        }

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

        if (stumpPrefab != null)
        {
            stumpRenderer = Instantiate(stumpPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f), transform);
        }

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

        if (stumpRenderer != null)
        {
            Destroy(stumpRenderer);
        }
    }
}