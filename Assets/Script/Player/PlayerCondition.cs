using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    public TemperatureSystem temperatureSystem;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition thirst { get { return uiCondition.thirst; } }
    Condition stamina { get { return uiCondition.stamina; } }
    Condition temp { get { return uiCondition.temp; } }

    public float healthDecay = 1f;
    public event Action onTakeDamage;

    private void Awake()
    {
        if (uiCondition == null)
        {
            uiCondition = FindObjectOfType<UICondition>();
        }

        if (temperatureSystem == null)
        {
            temperatureSystem = FindObjectOfType<TemperatureSystem>();
        }
    }

    private void Update()
    {
        if (uiCondition == null)
        {
            return;
        }

        hunger.Subtract((hunger.passiveValue + hunger.plusValue) * Time.deltaTime);
        thirst.Subtract((thirst.passiveValue + thirst.plusValue) * Time.deltaTime);
        stamina.Add((stamina.passiveValue + stamina.plusValue) * Time.deltaTime);

        if (hunger.curValue <= 0f)
        {
            TakeDamage(healthDecay * Time.deltaTime);
        }

        if (thirst.curValue <= 0f)
        {
            TakeDamage(healthDecay * Time.deltaTime);
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

    public void HungerValuePlus(float amount)
    {
        hunger.plusValue = amount;
    }

    public void ResetHungerValuePlus()
    {
        hunger.plusValue = 0;
    }

    public void ThirstValuePlus(float amount)
    {
        thirst.plusValue = amount;
    }

    public void ResetThirstValuePlus()
    {
        thirst.plusValue = 0;
    }

    public void Die()
    {
        PlayerManager.Instance.Player.controller.Die();
    }

}
