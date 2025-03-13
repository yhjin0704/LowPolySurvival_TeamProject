using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AddComponentToTagObject<Tree>("Tree");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddComponentToTagObject<T>(string _tagName) where T : Component
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(_tagName);
        foreach (GameObject obj in objs)
        {
            if (obj.GetComponent<T>() == null)
            {
                obj.AddComponent<T>();
            }
        }
    }
}
