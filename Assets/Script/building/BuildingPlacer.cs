using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject player;
    public ReSourceManager resourceManager;
    public GameObject buildingPrefab; // 설치할 건물 프리팹
    public LayerMask targetlayer;
    public BuildingSystem buildingSystem;

    //private Vector3 placementPosition;

    public bool IsValidPlacement(Vector3 position)
    {
        RaycastHit hit;
        // Raycast를 아래로 쏘아서, 배치하려는 위치에 맞는 'Ground'가 있는지 확인
        if (Physics.Raycast(position, Vector3.down, out hit, 1f))
        {
            // 태그가 "Ground"로 시작하는지 확인
            if (hit.collider.CompareTag("Ground") || hit.collider.tag.StartsWith("Ground"))
            {
                return true; // 배치 가능
            }
        }
        return false; // 배치할 수 없는 곳
    }
    void Update()
    {
        if (buildingSystem == null)
        {
            Debug.LogError("BuildingSystem이 연결되지 않았습니다!");
            return;
        }
        if (buildingSystem.buildingPlacementPoint == null)
        {
            Debug.LogError("buildingPlacementPoint가 설정되지 않았습니다!");
            return;
        }
        // 마우스 클릭 위치로 레이 쏘기
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f, targetlayer))
            {
                buildingSystem.buildingPlacementPoint.position = hit.point;
                buildingSystem.Build();
            }
        }
       


    }
}