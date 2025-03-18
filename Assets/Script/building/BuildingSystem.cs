using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    
        public GameObject player;
        public GameObject buildingPrefab;  // ��ġ�� �ǹ��� ������ (Build ������ ����)
        public Transform buildingPlacementPoint;  // �ǹ� ��ġ�� ��ġ
        public ReSourceManager resourceManager;  // �ڿ� ���� �ý���
        public UIInventory uiinventory;
        public BuildingPlacer buildingPlacer;

        // �� �ǹ��� �ʿ��� �ڿ���
        public enum BuildingType { Tent, Campfire, Fence }
        public BuildingType buildingType;

        private Dictionary<BuildingType, (int wood, int rock, int brunch)> buildingRequirements = new Dictionary<BuildingType, (int wood, int rock, int brunch)>
        {
        { BuildingType.Tent, (5, 0, 2) },
        { BuildingType.Campfire, (5, 5, 0) },
        { BuildingType.Fence, (10, 0, 0) }
        };

        private bool canBuild = false;  // ��ġ ���� ����

        public void Build()
        {
            // �ǹ��� �´� �ڿ� �䱸�� ��������
            var (requiredWood, requiredRock, requiredBrunch) = buildingRequirements[buildingType];

            if (resourceManager.CanBuild(requiredWood, requiredRock, requiredBrunch))
            {
                // �ǹ� ��ġ (Build �������� ���)
                Instantiate(buildingPrefab, buildingPlacementPoint.position, Quaternion.identity);
                // �ڿ� ����
                resourceManager.UseResources(requiredWood, requiredRock, requiredBrunch);
                Debug.Log($"{buildingType} �ǹ��� ���������� ��ġ�Ǿ����ϴ�.");
            }
            else
            {
                Debug.LogWarning($"{buildingType} �ǹ��� ��ġ�� �� �����ϴ�. �ڿ��� �����մϴ�.");
            }
        }
}
