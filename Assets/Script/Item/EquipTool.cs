using DropResource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;

    [Header("Combat")]
    public int damage;
    public LayerMask hitLayer;

    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    public override void OnAttackInput()
    {
      
    }

    void OnCanAttack()
    {
        attacking = false;
    }

    public void OnHit()
    {
       
    }
}
