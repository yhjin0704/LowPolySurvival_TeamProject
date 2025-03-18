using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSourceManager : MonoBehaviour
{
    public UIInventory uiinventory;
    public ItemData[] datas;

    public bool CanBuild(int requiredWood, int requiredRock, int requiredBranch)
    {
        return uiinventory.GetItemQuantity(datas[0]) >= requiredWood && uiinventory.GetItemQuantity(datas[1]) >= requiredRock && uiinventory.GetItemQuantity(datas[2]) >= requiredBranch;
    }

    public void UseResources(int requiredWood, int requiredRock, int requiredBranch)
    {
        if (CanBuild(requiredWood, requiredRock, requiredBranch))
        {
            uiinventory.SetItemQunatity(datas[0], requiredWood);
            uiinventory.SetItemQunatity(datas[1], requiredRock);
            uiinventory.SetItemQunatity(datas[2], requiredBranch);
        }
    }
}