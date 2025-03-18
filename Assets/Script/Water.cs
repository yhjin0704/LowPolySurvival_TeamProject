using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour, IInteractable
{
    public ItemData data;
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}\n'E'키를 눌러 상호작용";
        return str;
    }

    public void OnInteract()
    {
        PlayerManager.Instance.Player.controller.WaterInteraction();
    }
}
