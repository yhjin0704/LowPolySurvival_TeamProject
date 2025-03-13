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

        // 밤일 때만 체력 감소
        if (dayNightCycle.isNightTime)
        {
            health.Subtract(1f * Time.deltaTime); // 밤마다 1씩 감소
            Debug.Log("밤 시간! 체력 감소 중... 현재 체력: " + health.curValue); // 디버그로그 추가
        }

        if (health.curValue <= 0f)
        {
            Debug.Log("플레이어 체력이 0 이하로 떨어짐!"); // 체력이 0 이하로 떨어지면 로그 추가
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
        Debug.Log("플레이어가 죽었다.");
    }
}
