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

    [Header("�� ��ó �µ� ��� ó��")]
    public Transform player;
    public float fireWarmthRange = 5f;
    public float fireHeatBonus = 10f;
    public LayerMask fireLayer;

    void Start()
    {
        // playerCondition�� �������� �ʾҴٸ� GetComponent�� ��������
        if (playerCondition == null)
        {
            playerCondition = GetComponent<PlayerCondition>();
        }
    }

    void Update()
    {
        float targetTemp = dayNightCycle.isNightTime ? nightTemperature : dayTemperature;

        // �� ��ó üũ
        Collider[] fireSources = Physics.OverlapSphere(player.position, fireWarmthRange, fireLayer);
        if (fireSources.Length > 0)
        {
            targetTemp += fireHeatBonus;
        }

        // �µ� �ε巴�� ����
        currentTemperature = Mathf.Lerp(currentTemperature, targetTemp, Time.deltaTime * temperatureChangeSpeed);

        // ������ ó��
        if (currentTemperature <= coldThreshold)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                // playerCondition�� Condition ������Ʈ�� ���� TakeDamage ȣ��
                playerCondition.condition.TakeDamage(coldDamage);  // coldDamage��ŭ ü�� ����
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