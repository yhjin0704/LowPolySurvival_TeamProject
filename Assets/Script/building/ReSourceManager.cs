using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ReSourceManager : MonoBehaviour
{
    public int wood = 0;
    public int stone = 0;

    public void AddWood(int amount)
    {
        wood += amount;
    }

    public void AddStone(int amount)
    {
        stone += amount;
    }

    public bool CanBuild(int requiredWood, int requiredStone)
    {
        return wood >= requiredWood && stone >= requiredStone;
    }

    public void UseResources(int requiredWood, int requiredStone)
    {
        if (CanBuild(requiredWood, requiredStone))
        {
            wood -= requiredWood;
            stone -= requiredStone;
        }
    }
}
