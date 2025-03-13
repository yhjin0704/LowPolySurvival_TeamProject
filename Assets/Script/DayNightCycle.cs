using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f; //0.5f 일때 정오
    private float timeRate;
    public Vector3 moon; //Vector 90 0 0

    [Header('Sun')]

    [Header('Moon')]

    [Header('Other Lighting')]
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
