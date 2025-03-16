using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingSystem : MonoBehaviour
{
    public GameObject buildingPrefab;  // ��ġ�� �ǹ��� ������
    public Transform buildingPlacementPoint;  // �ǹ� ��ġ�� ��ġ
    public ReSourceManager resourceManager;  // �ڿ� ���� �ý���
    public int requiredWood = 10;  // �ǹ��� �ʿ��� ���� ��
    public int requiredStone = 10;  // �ǹ��� �ʿ��� �� ��

    private PlacementManager placementManager;  // ��ġ �������� Ȯ���ϴ� �Ŵ���
    private bool canBuild = false;  // ��ġ ���� ����

    void Start()
    {
        // PlacementManager�� ã��, �ʱ�ȭ
        placementManager = GetComponent<PlacementManager>();
    }

    void Update()
    {
        // ���콺 ��Ŭ������ �ǹ� ��ġ
        if (Input.GetMouseButtonDown(0) && canBuild)
        {
            Build();
        }

        // ��ġ �������� üũ
        canBuild = placementManager.IsValidPlacement(buildingPlacementPoint.position) &&
                   resourceManager.CanBuild(requiredWood, requiredStone);
    }

    void Build()
    {
        if (resourceManager.CanBuild(requiredWood, requiredStone))
        {
            // �ǹ� ��ġ
            Instantiate(buildingPrefab, buildingPlacementPoint.position, Quaternion.identity);
            // �ڿ� ����
            resourceManager.UseResources(requiredWood, requiredStone);
        }
    }
}