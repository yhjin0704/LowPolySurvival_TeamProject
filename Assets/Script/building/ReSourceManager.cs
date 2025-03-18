using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSourceManager : MonoBehaviour
{
    public int wood = 0;
    public int rock = 0;
    public int branch = 0;

    void Start()
    {
        Debug.Log("ReSourceManager Start �����"); // Ȯ�ο� �α�
        AddWood(20);
        AddRock(20);
        AddBranch(10);
        Debug.Log($"[�ʱ� �ڿ�] wood: {wood}, rock: {rock}, branch: {branch}");
    }


        public void AddWood(int amount)
        {
            wood += amount;
        }

        public void AddRock(int amount)
        {
            rock += amount;
        }

        public void AddBranch(int amount)
        {
            branch += amount;
        }

        public bool CanBuild(int requiredWood, int requiredRock, int requiredBranch)
        {
            return wood >= requiredWood && rock >= requiredRock && branch >= requiredBranch;
        }

        public void UseResources(int requiredWood, int requiredRock, int requiredBranch) 
        {
            if (CanBuild(requiredWood, requiredRock, requiredBranch))
            {
                wood -= requiredWood;
                rock -= requiredRock;
                branch -= requiredBranch;
            }
        }
    }