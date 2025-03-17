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
    public float coldDamage = 5f;
    public float damageInterval = 3f;
    private float damageTimer;

    [Header("불 근처 온도 상승 처리")]
    public Transform player;
    public float fireWarmthRange = 5f;
    public float fireHeatBonus = 10f;
    public LayerMask fireLayer;

    void Start()
    {
        // playerCondition이 설정되지 않았다면 GetComponent로 가져오기
        if (playerCondition == null)
        {
            playerCondition = GetComponent<PlayerCondition>();
        }
    }

    void Update()
    {
        float targetTemp = dayNightCycle.isNightTime ? nightTemperature : dayTemperature;

        // 불 근처 체크
        Collider[] fireSources = Physics.OverlapSphere(player.position, fireWarmthRange, fireLayer);
        if (fireSources.Length > 0)
        {
            targetTemp += fireHeatBonus;
        }

        // 온도 부드럽게 변경
        currentTemperature = Mathf.Lerp(currentTemperature, targetTemp, Time.deltaTime * temperatureChangeSpeed);

        // 데미지 처리
        if (currentTemperature <= coldThreshold)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                // playerCondition의 Condition 컴포넌트를 통해 TakeDamage 호출
                playerCondition.condition.TakeDamage(coldDamage);  // coldDamage만큼 체력 감소
                damageTimer = 0f;
            }
        }
        else
        {
            damageTimer = 0f;
        }
    }
}
}