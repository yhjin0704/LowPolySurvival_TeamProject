using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSourceManager : MonoBehaviour
{
    public int wood = 0;
    public int rock = 0;
    public int brunch = 0;

    void Start()
    {
        AddWood(20);
        AddRock(20);
        Debug.Log($"[초기 자원] wood: {wood}, rock: {rock}");
    }



        public void AddWood(int amount)
        {
            wood += amount;
        }

        public void AddRock(int amount)
        {
            rock += amount;
        }

        public void AddBrunch(int amount) // brunch 자원 추가
        {
            brunch += amount;
        }

        public bool CanBuild(int requiredWood, int requiredRock, int requiredBrunch) // brunch 추가
        {
            return wood >= requiredWood && rock >= requiredRock && brunch >= requiredBrunch;
        }

        public void UseResources(int requiredWood, int requiredRock, int requiredBrunch) // brunch 추가
        {
            if (CanBuild(requiredWood, requiredRock, requiredBrunch))
            {
                wood -= requiredWood;
                rock -= requiredRock;
                brunch -= requiredBrunch;
            }
        }
    }