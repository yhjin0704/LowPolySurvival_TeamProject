using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength = 120f;
    public float startTime = 0.4f; //0.5f ÀÏ¶§ Á¤¿À
    private float timeRate;
    public Vector3 noon; //Vector 90 0 0

    public bool isNightTime;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;//¹à±â º¯È­

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;//¹à±â º¯È­

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;//º¸Á¤¿ë °î¼±
    public AnimationCurve reflectionIntensityMultiplier;//¹Ý»ç±¤ °­µµ º¯È­ °î¼±
    void Start()
    {
        timeRate = 0.5f / fullDayLength;
        time = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;


        // ¹ãÀÎÁö ³·ÀÎÁö Ã¼Å©
        isNightTime = time >= 0.75f && time <= 1.0f;

        // isNightTime »óÅÂ¸¦ È®ÀÎ
        if (isNightTime)
        {
            Debug.Log("¹ã ½Ã°£ÀÌ ½ÃÀÛµÊ!"); // ¹ãÀÏ ¶§ µð¹ö±×
        }
        else
        {
            Debug.Log("³· ½Ã°£!"); // ³·ÀÏ ¶§ µð¹ö±×
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
