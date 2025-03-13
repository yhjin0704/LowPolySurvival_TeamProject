using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public ItemData itemData;
    public Action addItem;
    public PlayerCondition condition;

    private void Awake()
    {
        PlayerManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        //condition = GetComponent<PlayerCondition>();
    }
}
