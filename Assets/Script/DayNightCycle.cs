using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength = 120f;
    public float startTime = 0.4f; //0.5f �϶� ����
    private float timeRate;
    public Vector3 noon; //Vector 90 0 0

    public bool isNightTime;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;//��� ��ȭ

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;//��� ��ȭ

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;//������ �
    public AnimationCurve reflectionIntensityMultiplier;//�ݻ籤 ���� ��ȭ �
    void Start()
    {
        timeRate = 0.5f / fullDayLength;
        time = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;


        // ������ ������ üũ
        isNightTime = time >= 0.75f && time <= 1.0f;

        // isNightTime ���¸� Ȯ��
        if (isNightTime)
        {
            Debug.Log("�� �ð��� ���۵�!"); // ���� �� �����
        }
        else
        {
            Debug.Log("�� �ð�!"); // ���� �� �����
        }

        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    void UpdateLighting(Light lightSource, Gradient gradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);

        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4f;
        lightSource.color = gradient.Evaluate(time);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject;
        if(lightSource.intensity == 0 && go.activeInHierarchy)
        {
            go.SetActive(false);
        }
        else if(lightSource.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
        }
    }
}
