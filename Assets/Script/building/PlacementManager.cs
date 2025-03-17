using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
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
}
