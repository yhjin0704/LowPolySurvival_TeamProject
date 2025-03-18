using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject player;
    public GameObject buildingPrefab; // 설치할 건물 프리팹
    public LayerMask targetlayer;
    public BuildingSystem buildingSystem;

    //private Vector3 placementPosition;

    void Update()
    {
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

    }
}