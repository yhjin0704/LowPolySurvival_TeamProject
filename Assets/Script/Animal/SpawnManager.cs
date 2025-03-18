using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;

    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject().AddComponent<SpawnManager>();
            }
            return _instance;
        }
    }

    public List<GameObject> LBear;
    public List<GameObject> LChicken;
    public List<GameObject> LDeer;
    public List<GameObject> LTiger;

    private void Awake()
    {
        LBear = new List<GameObject>();
        LChicken = new List<GameObject>();
        LDeer = new List<GameObject>();
        LTiger = new List<GameObject>();
    }

    private void Start()
    {
    }
}
