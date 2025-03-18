using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSourceManager : MonoBehaviour
{
    public int wood = 0;
    public int rock = 0;  // stone → rock

    //void Start()
    //{
    //    AddWood(20);
    //    AddRock(20);
    //    Debug.Log($"[초기 자원] wood: {wood}, rock: {rock}");
    //}

    public void AddWood(int amount)
    {
        wood += amount;
    }

    public void AddRock(int amount) // AddStone → AddRock
    {
        rock += amount;
    }

    public bool CanBuild(int requiredWood, int requiredRock) // requiredStone → requiredRock
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
