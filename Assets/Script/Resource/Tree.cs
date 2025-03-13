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

    // 나무가 부서질 때 호출되는 메서드
    public void Break()
    {
        isBreak = true;

        // 아이템 드랍 처리
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

        // deadTreeTrunkPrefab이 지정되어 있다면, 현재 나무 위치에서 인스턴스화하여 자식으로 추가합니다.
        if (deadTreeTrunkPrefab != null)
        {
            currentDeadTrunk = Instantiate(deadTreeTrunkPrefab, transform.position, transform.rotation, transform);
            // 인스턴스화된 밑둥의 위치를 부모(나무)의 로컬 좌표 기준으로 조정
            currentDeadTrunk.transform.localPosition = Vector3.zero;
        }

        StartCoroutine(CRespawnCoroutine());
    }

    // 재활성화를 위한 코루틴 (예: 5초 후 복원)
    IEnumerator CRespawnCoroutine()
    {
        yield return new WaitForSeconds(5f);

        // 죽은 나무 밑둥 제거
        if (currentDeadTrunk != null)
        {
            Destroy(currentDeadTrunk);
        }

        // Collider와 Renderer 다시 활성화
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