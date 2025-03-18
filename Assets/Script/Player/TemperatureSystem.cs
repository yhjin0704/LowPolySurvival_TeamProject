using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureSystem : MonoBehaviour
{
    Player player;
    public PlayerCondition playerCondition;
    public UICondition uiCondition;
    public DayNightCycle dayNightCycle;

    Condition temp { get { return uiCondition.temp; } }

    [HideInInspector]
    public float currentTemperature = 50f;

    float dayTemperature = 50f;
    float nightTemperature = 25f;

    [HideInInspector]
    public float targetTemp;

    bool isNearFire = false;
    bool isColdArea = false;
    bool isHotArea = false;

    private void Start()
    {
        player = PlayerManager.Instance.Player;

        if (uiCondition == null)
        {
            uiCondition = FindObjectOfType<UICondition>();
        }

        if (dayNightCycle == null)
        {
            dayNightCycle = FindObjectOfType<DayNightCycle>();
        }

        if (playerCondition == null)
        {
            playerCondition = PlayerManager.Instance.Player.condition;
        }
    }

    void Update()
    {
        if (dayNightCycle == null)
        {
            return;
        }

        SetTargetTemp();

        // �µ� �ε巴�� ����
        currentTemperature = Mathf.Lerp(currentTemperature, targetTemp, Time.deltaTime * 0.5f);

        Debug.Log("Current Temperature: " + currentTemperature);

        if (currentTemperature <= 30f)
        {
            playerCondition.TakeDamage(playerCondition.healthDecay * Time.deltaTime);
            playerCondition.HungerValuePlus(5f);
        }
        else if (currentTemperature >= 75f)
        {
            playerCondition.TakeDamage(playerCondition.healthDecay * Time.deltaTime);
            playerCondition.ThirstValuePlus(5f);
        }
        else
        {
            playerCondition.ResetHungerValuePlus();
            playerCondition.ResetThirstValuePlus();
        }

            temp.curValue = currentTemperature;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Desert"))
        {
            isHotArea = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Snow"))
        {
            isColdArea = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            isNearFire = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Desert"))
        {
            isHotArea = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Snow"))
        {
            isColdArea = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            isNearFire = false;
        }
    }

    void SetTargetTemp()
    {
        targetTemp = dayNightCycle.isNightTime ? nightTemperature : dayTemperature;

        if (isHotArea)
        {
            targetTemp += 30f;
        }

        if (isColdArea)
        {
            targetTemp -= 25f;
        }

        if (isNearFire)
        {
            targetTemp += 15f;
        }
    }
}