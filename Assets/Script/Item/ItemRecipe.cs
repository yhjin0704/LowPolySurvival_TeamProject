using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RequiredItems
{
    public ItemData materials;
    public int amount;
}

[CreateAssetMenu(fileName = "ItemRecipe", menuName = "New ItemRecipe")]
public class ItemRecipe : ScriptableObject
{
    [Header("Info")]
    public ItemData desiredItem;
    public int quantities;

    [Header("Recipe")]
    public RequiredItems[] requiredItems;
}
