using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingSystem : MonoBehaviour
{
    public GameObject buildingPrefab;  // ��ġ�� �ǹ��� ������
    public Transform buildingPlacementPoint;  // �ǹ� ��ġ�� ��ġ
    public ReSourceManager resourceManager;  // �ڿ� ���� �ý���
    public int requiredWood = 10;  // �ǹ��� �ʿ��� ���� ��
    public int requiredRock = 10;  // �ǹ��� �ʿ��� �� ��
    public UIInventory uiinventory;

    private PlacementManager placementManager;  // ��ġ �������� Ȯ���ϴ� �Ŵ���
    // private bool canBuild = false;  // ��ġ ���� ����

    void Start()
    {
        // PlacementManager�� ã��, �ʱ�ȭ
        placementManager = GetComponent<PlacementManager>();
    }

    public void Build()
    {
        if (resourceManager.CanBuild(requiredWood, requiredRock))
        {
            // �ǹ� ��ġ
            Instantiate(buildingPrefab, buildingPlacementPoint.position, Quaternion.identity);
            // �ڿ� ����
            resourceManager.UseResources(requiredWood, requiredRock);
            Debug.Log("�ǹ��� ���������� ��ġ�Ǿ����ϴ�.");


        }
        else
        {
            Debug.LogWarning("�ڿ��� �����Ͽ� �ǹ��� ��ġ�� �� �����ϴ�. (�ʿ�: Wood 10, Rock 10)");
        }
    }
}