using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

//public class BuildingPlacer : MonoBehaviour
//{
//    public GameObject player;
//    public ReSourceManager resourceManager;
//    public GameObject buildingPrefab; // 설치할 건물 프리팹
//    public LayerMask targetlayer;
//    public BuildingSystem buildingSystem;

//    //private Vector3 placementPosition;

//    public bool IsValidPlacement(Vector3 position)
//    {
//        RaycastHit hit;
//        // Raycast를 아래로 쏘아서, 배치하려는 위치에 맞는 'Ground'가 있는지 확인
//        if (Physics.Raycast(position, Vector3.down, out hit, 1f))
//        {
//            // 태그가 "Ground"로 시작하는지 확인
//            if (hit.collider.CompareTag("Ground") || hit.collider.tag.StartsWith("Ground"))
//            {
//                return true; // 배치 가능
//            }
//        }
//        return false; // 배치할 수 없는 곳
//    }

    //void Update()
    //    {
    //                if (Input.GetKeyDown(KeyCode.Alpha1)) // 텐트
    //                {
    //                    buildingSystem.buildingType = BuildingSystem.BuildingType.Tent;
    //                    buildingSystem.Build();
    //                }
    //                if (Input.GetKeyDown(KeyCode.Alpha2)) // 모닥불
    //                {
    //                    buildingSystem.buildingType = BuildingSystem.BuildingType.Campfire;
    //                    buildingSystem.Build();
    //                }
    //                if (Input.GetKeyDown(KeyCode.Alpha3)) // 울타리
    //                {
    //                    buildingSystem.buildingType = BuildingSystem.BuildingType.Fence;
    //                    buildingSystem.Build();
    //                }
    //    }
//}
