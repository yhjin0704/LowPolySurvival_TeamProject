using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    public DayNightCycle dayNightCycle; //CYS추가코드

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition thirst { get { return uiCondition.thirst; } }
    Condition stamina { get { return uiCondition.stamina; } }
    Condition temperature { get { return uiCondition.temperature; } }

    public float HealthDecay;
    public event Action onTakeDamage;
    public TemperatureSystem temperatureSystem;
    

    private void Awake()
    {
        if (temperatureSystem == null)
        {
            temperatureSystem = PlayerManager.Instance.Player.GetComponent<TemperatureSystem>();
        }
        
    }

    private void Update()
    {
        if (uiCondition == null)
        {
            return;
        }


        
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        thirst.Subtract(thirst.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0f)
        {
            health.Subtract(HealthDecay * Time.deltaTime);
        }

        if (thirst.curValue <= 0f)
        {
            health.Subtract(HealthDecay * Time.deltaTime);
        }

        if (temperatureSystem.currentTemperature <= 0f)
        {
            health.Subtract(HealthDecay * Time.deltaTime);
        }

        if (temperatureSystem.currentTemperature >= 30f)
        {
            health.Subtract(HealthDecay * Time.deltaTime);
        }

        if (health.curValue <= 0f)
        {
            Die();
        }

 


    }

    public void TakeDamage(float amount)
    {
        health.Subtract(amount);
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

    public void ConsumeStamina(float amount)
    {
        stamina.Subtract(amount);
    }

    public bool IsStaminaZero()
    {
        return stamina.GetPercentage() == 0;
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }

}
