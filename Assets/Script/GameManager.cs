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
            // ���� �ִ� ��� Tree ������Ʈ�� ã���ϴ�.
            Tree[] trees = FindObjectsOfType<Tree>();
            foreach (Tree tree in trees)
            {
                // �� Tree�� Break() �޼��带 ȣ���մϴ�.
                tree.TakeDamage(200f);
            }
            Debug.Log("F1");
        }
    }
}
