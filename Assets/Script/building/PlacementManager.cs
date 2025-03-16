using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
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
}
