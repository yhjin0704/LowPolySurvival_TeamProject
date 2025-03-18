using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject player;
    public ReSourceManager resourceManager;
    public GameObject buildingPrefab; // ��ġ�� �ǹ� ������
    public LayerMask targetlayer;
    public BuildingSystem buildingSystem;

    //private Vector3 placementPosition;

    public bool IsValidPlacement(Vector3 position)
    {
        RaycastHit hit;
        // Raycast�� �Ʒ��� ��Ƽ�, ��ġ�Ϸ��� ��ġ�� �´� 'Ground'�� �ִ��� Ȯ��
        if (Physics.Raycast(position, Vector3.down, out hit, 1f))
        {
            // �±װ� "Ground"�� �����ϴ��� Ȯ��
            if (hit.collider.CompareTag("Ground") || hit.collider.tag.StartsWith("Ground"))
            {
                return true; // ��ġ ����
            }
        }
        return false; // ��ġ�� �� ���� ��
    }
    void Update()
    {
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
       


    }
}