using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject buildingPrefab; // 설치할 건물 프리팹
    public LayerMask targetlayer;

    private Vector3 placementPosition;

    void Update()
    {
        // 마우스 클릭 위치로 레이 쏘기
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f, targetlayer))
            {
                Vector3 buildPosition = hit.point;

                // 건물 설치
                if (buildingPrefab != null)
                {
                    Instantiate(buildingPrefab, buildPosition, Quaternion.identity);
                    Debug.Log("건물이 설치되었습니다.");
                }
                else
                {
                    Debug.LogWarning("buildingPrefab이 설정되지 않았습니다!");
                }
            }
        }
    }
}