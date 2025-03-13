using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
    public GameObject dropItemPrefab;
    public int minDropCount;
    public int maxDropCount;
}

public class TreeScript : MonoBehaviour
{
    public float maxHp = 100f;
    public float currentHp;

    public DropItem[] dropItems;

    private Collider collider_;
    private Renderer[] renderer_;
    private GameObject stompRenderer;
    private GameObject stompPrefab;

    private bool isBreak = false;

    void Awake()
    {
        currentHp = maxHp;

        collider_ = GetComponentInChildren<Collider>();
        renderer_ = GetComponentsInChildren<Renderer>();
    }

    // ������ �μ��� �� ȣ��Ǵ� �޼���
    public void Break()
    {
        isBreak = true;

        // ������ ��� ó��
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

        // deadTreeTrunkPrefab�� �����Ǿ� �ִٸ�, ���� ���� ��ġ���� �ν��Ͻ�ȭ�Ͽ� �ڽ����� �߰��մϴ�.
        if (deadTreeTrunkPrefab != null)
        {
            currentDeadTrunk = Instantiate(deadTreeTrunkPrefab, transform.position, transform.rotation, transform);
            // �ν��Ͻ�ȭ�� �ص��� ��ġ�� �θ�(����)�� ���� ��ǥ �������� ����
            currentDeadTrunk.transform.localPosition = Vector3.zero;
        }

        StartCoroutine(CRespawnCoroutine());
    }

    // ��Ȱ��ȭ�� ���� �ڷ�ƾ (��: 5�� �� ����)
    IEnumerator CRespawnCoroutine()
    {
        yield return new WaitForSeconds(5f);

        // ���� ���� �ص� ����
        if (currentDeadTrunk != null)
        {
            Destroy(currentDeadTrunk);
        }

        // Collider�� Renderer �ٽ� Ȱ��ȭ
        if (collider_ != null)
            collider_.enabled = true;
        foreach (Renderer rd in renderer_)
        {
            rd.enabled = true;
        }

        currentHp = maxHp;
        isBreak = false;
    }
}