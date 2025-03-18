using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingSystem : MonoBehaviour
{


    public GameObject player;
    public GameObject buildingPrefab;  // ��ġ�� �ǹ��� ������
    public Transform buildingPlacementPoint;  // �ǹ� ��ġ�� ��ġ
    public ReSourceManager resourceManager;   // �ڿ� ���� �ý���
    private Vector3 placementPosition;
    private int selectedBuildingIndex = 0; // ���õ� �ǹ� �ε���

    // �ǹ� ������ ����Ʈ (������ + �ڿ� �䱸 ����)
    public List<BuildingData> buildingDataList;

    private void Start()
    {
        player = PlayerManager.Instance.Player.gameObject;
    }

    void Update()
    {
        // PlacementManager�� ã��, �ʱ�ȭ
        placementManager = GetComponent<PlacementManager>();
    }

    public void Build()
    {
        if (resourceManager.CanBuild(requiredWood, requiredRock))
        {
            // �ڿ� �䱸�� Ȯ��
            
            if (resourceManager.CanBuild(selectedBuilding.wood, selectedBuilding.rock, selectedBuilding.branch))
            {
                // �ǹ� ��ġ
                Instantiate(selectedBuilding.prefab, placementPosition, player.transform.rotation);

        }
        else
        {
            Debug.LogWarning("�ڿ��� �����Ͽ� �ǹ��� ��ġ�� �� �����ϴ�. (�ʿ�: Wood 10, Rock 10)");
        }
    }

    // ��ġ ������ ��ġ���� Ȯ���ϴ� �Լ�
    public bool IsValidPlacement(Vector3 position)
    {
        RaycastHit hit;
        // Raycast�� �Ʒ��� ���
        if (Physics.Raycast(position, Vector3.down, out hit, 5f))
        {
            // 'Ground' ���̾����� Ȯ��
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                return true; // ��ġ ����
            }
        }

        return false; // ��ġ�� �� ���� ��
    }

    public void UpdateBuildingPlacementPosition()
    {
        // �÷��̾� �� 5 ���� ��ġ�� ��ġ�� ��ġ ������Ʈ
        buildingPlacementPoint.position = player.transform.position + player.transform.forward * 5f;
    }
}