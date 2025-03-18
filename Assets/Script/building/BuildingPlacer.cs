using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject player;
    public GameObject buildingPrefab; // ��ġ�� �ǹ� ������
    public LayerMask targetlayer;
    public BuildingSystem buildingSystem;

    //private Vector3 placementPosition;

    void Update()
    {
        // ���콺 Ŭ�� ��ġ�� ���� ���
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
            Debug.LogError("BuildingSystem�� ������� �ʾҽ��ϴ�!");
            return;
        }
        if (buildingSystem.buildingPlacementPoint == null)
        {
            Debug.LogError("buildingPlacementPoint�� �������� �ʾҽ��ϴ�!");
            return;
        }

    }
}