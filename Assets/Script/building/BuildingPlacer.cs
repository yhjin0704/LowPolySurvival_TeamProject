using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

//public class BuildingPlacer : MonoBehaviour
//{
//    public GameObject player;
//    public ReSourceManager resourceManager;
//    public GameObject buildingPrefab; // ��ġ�� �ǹ� ������
//    public LayerMask targetlayer;
//    public BuildingSystem buildingSystem;

//    //private Vector3 placementPosition;

//    public bool IsValidPlacement(Vector3 position)
//    {
//        RaycastHit hit;
//        // Raycast�� �Ʒ��� ��Ƽ�, ��ġ�Ϸ��� ��ġ�� �´� 'Ground'�� �ִ��� Ȯ��
//        if (Physics.Raycast(position, Vector3.down, out hit, 1f))
//        {
//            // �±װ� "Ground"�� �����ϴ��� Ȯ��
//            if (hit.collider.CompareTag("Ground") || hit.collider.tag.StartsWith("Ground"))
//            {
//                return true; // ��ġ ����
//            }
//        }
//        return false; // ��ġ�� �� ���� ��
//    }

    //void Update()
    //    {
    //                if (Input.GetKeyDown(KeyCode.Alpha1)) // ��Ʈ
    //                {
    //                    buildingSystem.buildingType = BuildingSystem.BuildingType.Tent;
    //                    buildingSystem.Build();
    //                }
    //                if (Input.GetKeyDown(KeyCode.Alpha2)) // ��ں�
    //                {
    //                    buildingSystem.buildingType = BuildingSystem.BuildingType.Campfire;
    //                    buildingSystem.Build();
    //                }
    //                if (Input.GetKeyDown(KeyCode.Alpha3)) // ��Ÿ��
    //                {
    //                    buildingSystem.buildingType = BuildingSystem.BuildingType.Fence;
    //                    buildingSystem.Build();
    //                }
    //    }
//}
