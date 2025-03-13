using DropResource;
using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        AddComponentToTagObject<Rock>("BreakableRock");
    }

    // Update is called once per frame
    void Update()
    {
        DebugTestKey();
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

    void DebugTestKey()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            IBreakableObject[] baobjs = FindObjectsOfType<MonoBehaviour>().OfType<IBreakableObject>().ToArray();
            foreach (IBreakableObject baobj in baobjs)
            {
                baobj.TakeDamage(200f);
            }
            Debug.Log("F1");
        }
    }
}
