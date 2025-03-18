using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    
        public GameObject player;
        public GameObject buildingPrefab;  // 배치할 건물의 프리팹 (Build 프리팹 연결)
        public Transform buildingPlacementPoint;  // 건물 배치할 위치
        public ReSourceManager resourceManager;  // 자원 관리 시스템
        public UIInventory uiinventory;
        public BuildingPlacer buildingPlacer;

        // 각 건물에 필요한 자원들
        public enum BuildingType { Tent, Campfire, Fence }
        public BuildingType buildingType;

        private Dictionary<BuildingType, (int wood, int rock, int brunch)> buildingRequirements = new Dictionary<BuildingType, (int wood, int rock, int brunch)>
        {
        { BuildingType.Tent, (5, 0, 2) },
        { BuildingType.Campfire, (5, 5, 0) },
        { BuildingType.Fence, (10, 0, 0) }
        };

        private bool canBuild = false;  // 배치 가능 여부

        public void Build()
        {
            // 건물에 맞는 자원 요구량 가져오기
            var (requiredWood, requiredRock, requiredBrunch) = buildingRequirements[buildingType];

            if (resourceManager.CanBuild(requiredWood, requiredRock, requiredBrunch))
            {
                // 건물 배치 (Build 프리팹을 사용)
                Instantiate(buildingPrefab, buildingPlacementPoint.position, Quaternion.identity);
                // 자원 차감
                resourceManager.UseResources(requiredWood, requiredRock, requiredBrunch);
                Debug.Log($"{buildingType} 건물이 성공적으로 설치되었습니다.");
            }
            else
            {
                Debug.LogWarning($"{buildingType} 건물을 설치할 수 없습니다. 자원이 부족합니다.");
            }
        }
}
