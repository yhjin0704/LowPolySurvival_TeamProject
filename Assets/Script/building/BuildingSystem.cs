using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject player;
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
        // �ǹ� ����
        if (Input.GetKeyDown(KeyCode.Alpha1)) { selectedBuildingIndex = 0; Build(); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { selectedBuildingIndex = 1; Build(); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { selectedBuildingIndex = 2; Build(); }

        // �ǹ� ��ġ ��ġ ������Ʈ
        UpdateBuildingPlacementPosition();
    }

    [System.Serializable]
    public struct BuildingData
    {
        public GameObject prefab;
        public int wood;
        public int rock;
        public int branch;
    }

    // �ǹ� ��ġ �Լ�
    public void Build()
    {
        // ���õ� �ǹ� ������ ��������
        BuildingData selectedBuilding = buildingDataList[selectedBuildingIndex];

        // ��ġ�� ��ġ ���
        placementPosition = buildingPlacementPoint.position;

        // ��ġ�� ��ȿ���� üũ
        if (IsValidPlacement(placementPosition))
        {
            // �ڿ� �䱸�� Ȯ��
            
            if (resourceManager.CanBuild(selectedBuilding.wood, selectedBuilding.rock, selectedBuilding.branch))
            {
                // �ǹ� ��ġ
                Instantiate(selectedBuilding.prefab, placementPosition, player.transform.rotation);

                // �ڿ� ����
                resourceManager.UseResources(selectedBuilding.wood, selectedBuilding.rock, selectedBuilding.branch);
                Debug.Log($"{selectedBuilding.prefab.name} �ǹ��� ���������� ��ġ�Ǿ����ϴ�.");
            }
            else
            {
                Debug.LogWarning($"{selectedBuilding.prefab.name} �ǹ��� ��ġ�� �� �����ϴ�. �ڿ��� �����մϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("��ġ�� �� ���� ��ġ�Դϴ�.");
        }
    }

    // ��ġ ������ ��ġ���� Ȯ���ϴ� �Լ�
    public bool IsValidPlacement(Vector3 position)
    {
        RaycastHit hit;
        // Raycast�� �Ʒ��� ���
        if (Physics.Raycast(position, Vector3.down, out hit, 1f))
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