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

    public float dayTemperature = 50f;
    public float nightTemperature = 25f;
    public float temperatureChangeSpeed = 2f;

    [HideInInspector]
    public float targetTemp;

    public float hotThreshold = 75f;
    public float coldThreshold = 30f;

    bool isNearFire = false;
    bool isColdArea = false;
    bool isHotArea = false;

    private void Awake()
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

        // playerCondition이 설정되지 않았다면 GetComponent로 가져오기
        if (playerCondition == null)
        {
            playerCondition = player.condition;
        }
    }

    void Update()
    {
        if (dayNightCycle == null)
        {
            return;
        }

        SetTargetTemp();

        // 온도 부드럽게 변경
        currentTemperature = Mathf.Lerp(currentTemperature, targetTemp, Time.deltaTime * temperatureChangeSpeed);

        Debug.Log("Current Temperature: " + currentTemperature);

        if (currentTemperature <= 30f || currentTemperature >= 75f)
        {
            playerCondition.TakeDamage(playerCondition.healthDecay * Time.deltaTime);
        }
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