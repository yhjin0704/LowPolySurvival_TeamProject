using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureSystem : MonoBehaviour
{
    public DayNightCycle dayNightCycle; 
    public PlayerCondition playerCondition;

    public float currentTemperature = 20f;
    public float dayTemperature = 20f;
    public float nightTemperature = -5f;
    public float temperatureChangeSpeed = 5f;

    public float coldThreshold = 0f;
    //온도가 이 값 이하일 때 플레이어가 피해를 입기 시작합니다. 현재는 0도 이하로 설정되어 있습니다.
    public float coldDamage = 5f;
    //온도가 coldThreshold 이하로 내려갔을 때 플레이어가 받는 피해량입니다.
    public float damageInterval = 120f;
    //피해가 주어지는 간격을 설정하는 변수입니다. 3초마다 피해를 줍니다.
    private float damageTimer;
    //피해를 주는 타이머로, 일정 시간마다 피해를 주기 위한 타이머입니다.

    [Header("불 근처 온도 상승 처리")]
    public Transform player;
    public float fireWarmthRange = 5f;
    public float fireHeatBonus = 10f;
    public LayerMask fireLayer;

    private void Awake()
    {
        if (dayNightCycle == null)
        {
            dayNightCycle = FindObjectOfType<DayNightCycle>();
        }
    }

    void Start()
    {
        // playerCondition이 설정되지 않았다면 GetComponent로 가져오기
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

        float targetTemp = dayNightCycle.isNightTime ? nightTemperature : dayTemperature;

        // 불 근처 체크
        Collider[] fireSources = Physics.OverlapSphere(player.position, fireWarmthRange, fireLayer);
        if (fireSources.Length > 0)
        {
            targetTemp += fireHeatBonus;
        }

        // 온도 부드럽게 변경
        currentTemperature = Mathf.Lerp(currentTemperature, targetTemp, Time.deltaTime * temperatureChangeSpeed);

        //Debug.Log("Current Temperature: " + currentTemperature);

        // 데미지 처리
        if (currentTemperature <= coldThreshold)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                //TakeDamage 호출
                //Debug.Log("Taking cold damage! Current Health: " + playerCondition.health);
                //playerCondition.TakeDamage(coldDamage);
                //damageTimer = 0f;
            }
        }
        else
        {
            //damageTimer = 0f;
        }
    }
}