using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject player;
    public Transform buildingPlacementPoint;  // 건물 배치할 위치
    public ReSourceManager resourceManager;   // 자원 관리 시스템
    private Vector3 placementPosition;
    private int selectedBuildingIndex = 0; // 선택된 건물 인덱스

    // 건물 데이터 리스트 (프리팹 + 자원 요구 사항)
    public List<BuildingData> buildingDataList;

    private void Start()
    {
        player = PlayerManager.Instance.Player.gameObject;
    }

    void Update()
    {
        // 건물 선택
        if (Input.GetKeyDown(KeyCode.Alpha1)) { selectedBuildingIndex = 0; Build(); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { selectedBuildingIndex = 1; Build(); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { selectedBuildingIndex = 2; Build(); }

        // 건물 배치 위치 업데이트
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

    // 건물 배치 함수
    public void Build()
    {
        // 선택된 건물 데이터 가져오기
        BuildingData selectedBuilding = buildingDataList[selectedBuildingIndex];

        // 배치할 위치 계산
        placementPosition = buildingPlacementPoint.position;

        // 위치가 유효한지 체크
        if (IsValidPlacement(placementPosition))
        {
            // 자원 요구량 확인
            
            if (resourceManager.CanBuild(selectedBuilding.wood, selectedBuilding.rock, selectedBuilding.branch))
            {
                // 건물 배치
                Instantiate(selectedBuilding.prefab, placementPosition, player.transform.rotation);

                // 자원 차감
                resourceManager.UseResources(selectedBuilding.wood, selectedBuilding.rock, selectedBuilding.branch);
                Debug.Log($"{selectedBuilding.prefab.name} 건물이 성공적으로 설치되었습니다.");
            }
            else
            {
                Debug.LogWarning($"{selectedBuilding.prefab.name} 건물을 설치할 수 없습니다. 자원이 부족합니다.");
            }
        }
        else
        {
            Debug.LogWarning("배치할 수 없는 위치입니다.");
        }
    }

    // 배치 가능한 위치인지 확인하는 함수
    public bool IsValidPlacement(Vector3 position)
    {
        RaycastHit hit;
        // Raycast를 아래로 쏘기
        if (Physics.Raycast(position, Vector3.down, out hit, 1f))
        {
            // 'Ground' 레이어인지 확인
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                return true; // 배치 가능
            }
        }

        return false; // 배치할 수 없는 곳
    }

    public void UpdateBuildingPlacementPosition()
    {
        // 플레이어 앞 5 유닛 위치로 배치할 위치 업데이트
        buildingPlacementPoint.position = player.transform.position + player.transform.forward * 5f;
    }
}