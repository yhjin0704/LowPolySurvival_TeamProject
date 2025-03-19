using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSourceManager : MonoBehaviour
{
    public UIInventory uiinventory;
    public ItemData[] datas;

    public bool CanBuild(int requiredTent, int requiredFire, int requiredFence)
    {
        return uiinventory.GetItemQuantity(datas[0]) >= requiredTent && uiinventory.GetItemQuantity(datas[1]) >= requiredFire && uiinventory.GetItemQuantity(datas[2]) >= requiredFence;
    }

    public void UseResources(int requiredTent, int requiredFire, int requiredFence)
    {
        if (CanBuild(requiredTent, requiredFire, requiredFence))
        {
            uiinventory.SetItemQunatity(datas[0], requiredTent);
            uiinventory.SetItemQunatity(datas[1], requiredFire);
            uiinventory.SetItemQunatity(datas[2], requiredFence);
        }
    }
}