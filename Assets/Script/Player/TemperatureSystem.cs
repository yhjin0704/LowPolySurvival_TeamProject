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
    //�µ��� �� �� ������ �� �÷��̾ ���ظ� �Ա� �����մϴ�. ����� 0�� ���Ϸ� �����Ǿ� �ֽ��ϴ�.
    public float coldDamage = 5f;
    //�µ��� coldThreshold ���Ϸ� �������� �� �÷��̾ �޴� ���ط��Դϴ�.
    public float damageInterval = 120f;
    //���ذ� �־����� ������ �����ϴ� �����Դϴ�. 3�ʸ��� ���ظ� �ݴϴ�.
    private float damageTimer;
    //���ظ� �ִ� Ÿ�̸ӷ�, ���� �ð����� ���ظ� �ֱ� ���� Ÿ�̸��Դϴ�.

    [Header("�� ��ó �µ� ��� ó��")]
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
        // playerCondition�� �������� �ʾҴٸ� GetComponent�� ��������
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

        // �� ��ó üũ
        Collider[] fireSources = Physics.OverlapSphere(player.position, fireWarmthRange, fireLayer);
        if (fireSources.Length > 0)
        {
            targetTemp += fireHeatBonus;
        }

        // �µ� �ε巴�� ����
        currentTemperature = Mathf.Lerp(currentTemperature, targetTemp, Time.deltaTime * temperatureChangeSpeed);

        //Debug.Log("Current Temperature: " + currentTemperature);

        // ������ ó��
        if (currentTemperature <= coldThreshold)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                //TakeDamage ȣ��
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