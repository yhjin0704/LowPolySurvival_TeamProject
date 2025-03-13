using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    public DayNightCycle dayNightCycle;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition thirst { get { return uiCondition.thirst; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;
    public float noThirstHealthDecay;
    public event Action onTakeDamage;

    

    private void Update()
    {

        // ���� ���� ü�� ����
        if (dayNightCycle.isNightTime)
        {
            health.Subtract(1f * Time.deltaTime); // �㸶�� 1�� ����
            Debug.Log("�� �ð�! ü�� ���� ��... ���� ü��: " + health.curValue); // ����׷α� �߰�
        }

        if (health.curValue <= 0f)
        {
            Debug.Log("�÷��̾� ü���� 0 ���Ϸ� ������!"); // ü���� 0 ���Ϸ� �������� �α� �߰�
            Die();
        }

        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        thirst.Subtract(thirst.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (thirst.curValue <= 0f)
        {
            health.Subtract(noThirstHealthDecay * Time.deltaTime);
        }

        if (health.curValue <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Drink(float amount)
    {
        thirst.Add(amount);
    }

    public void Die()
    {
        Debug.Log("�÷��̾ �׾���.");
    }
}
