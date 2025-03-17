using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingSystem : MonoBehaviour
{
    public GameObject buildingPrefab;  // 배치할 건물의 프리팹
    public Transform buildingPlacementPoint;  // 건물 배치할 위치
    public ReSourceManager resourceManager;  // 자원 관리 시스템
    public int requiredWood = 10;  // 건물에 필요한 목재 수
    public int requiredStone = 10;  // 건물에 필요한 돌 수

    private PlacementManager placementManager;  // 배치 가능한지 확인하는 매니저
    private bool canBuild = false;  // 배치 가능 여부

    void Start()
    {
        // PlacementManager를 찾고, 초기화
        placementManager = GetComponent<PlacementManager>();
    }

    void Update()
    {
        // 마우스 좌클릭으로 건물 배치
        if (Input.GetMouseButtonDown(0) && canBuild)
        {
            Build();
        }

        // 배치 가능한지 체크
        canBuild = placementManager.IsValidPlacement(buildingPlacementPoint.position) &&
                   resourceManager.CanBuild(requiredWood, requiredStone);
    }

    void Build()
    {
        if (resourceManager.CanBuild(requiredWood, requiredStone))
        {
            // 건물 배치
            Instantiate(buildingPrefab, buildingPlacementPoint.position, Quaternion.identity);
            // 자원 차감
            resourceManager.UseResources(requiredWood, requiredStone);
        }
    }
}