using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSourceManager : MonoBehaviour
{
    public int wood = 0;
    public int rock = 0;  // stone ¡æ rock

    public void AddWood(int amount)
    {
        wood += amount;
    }

    public void AddRock(int amount) // AddStone ¡æ AddRock
    {
        rock += amount;
    }

    public bool CanBuild(int requiredWood, int requiredRock) // requiredStone ¡æ requiredRock
    {
        return wood >= requiredWood && rock >= requiredRock;
    }

    public void UseResources(int requiredWood, int requiredRock)
    {
        if (CanBuild(requiredWood, requiredRock))
        {
            wood -= requiredWood;
            rock -= requiredRock;
        }
    }
}
