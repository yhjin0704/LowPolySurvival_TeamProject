using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingSystem : MonoBehaviour
{
    public GameObject buildingPrefab;  // 배치할 건물의 프리팹
    public Transform buildingPlacementPoint;  // 건물 배치할 위치
    public ReSourceManager resourceManager;  // 자원 관리 시스템
    public int requiredWood = 10;  // 건물에 필요한 목재 수
    public int requiredRock = 10;  // 건물에 필요한 돌 수
    public UIInventory uiinventory;

    private PlacementManager placementManager;  // 배치 가능한지 확인하는 매니저
    // private bool canBuild = false;  // 배치 가능 여부

    void Start()
    {
        // PlacementManager를 찾고, 초기화
        placementManager = GetComponent<PlacementManager>();
    }

    public void Build()
    {
        if (resourceManager.CanBuild(requiredWood, requiredRock))
        {
            // 건물 배치
            Instantiate(buildingPrefab, buildingPlacementPoint.position, Quaternion.identity);
            // 자원 차감
            resourceManager.UseResources(requiredWood, requiredRock);
            Debug.Log("건물이 성공적으로 설치되었습니다.");


        }
        else
        {
            Debug.LogWarning("자원이 부족하여 건물을 설치할 수 없습니다. (필요: Wood 10, Rock 10)");
        }
    }
}