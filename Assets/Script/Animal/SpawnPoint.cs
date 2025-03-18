using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPoint : MonoBehaviour
{
    public GameObject AnimalPrefab;
    public float minSpawnRate = 40f;
    public float maxSpawnRate = 80f;
    private float spawnRate;
    public int startSpawnCount = 1;

    List<GameObject> LSpawnAnimalList;

    SpawnManager spawnManager;

    private void Awake()
    {
        spawnManager = SpawnManager.Instance;
    }

    void Start()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 10f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} : 주어진 범위 내에 NavMesh 위치를 찾지 못했습니다.");
        }

        if (AnimalPrefab.name.Contains("Bear"))
        {
            LSpawnAnimalList = spawnManager.LBear;
        }
        else if (AnimalPrefab.name.Contains("Chicken"))
        {
            LSpawnAnimalList = spawnManager.LChicken;
        }
        else if (AnimalPrefab.name.Contains("Deer"))
        {
            LSpawnAnimalList = spawnManager.LDeer;
        }
        else if (AnimalPrefab.name.Contains("Tiger"))
        {
            LSpawnAnimalList = spawnManager.LTiger;
        }

        for (int i = 0; i < startSpawnCount; i++)
        {
            if (LSpawnAnimalList != null)
            {
                GameObject newAnimal = Instantiate(AnimalPrefab, transform.position, Quaternion.identity);
                LSpawnAnimalList.Add(newAnimal);
            }
        }

        SetSpawnRate();
    }

    void Update()
    {
        spawnRate -= Time.deltaTime;
        if (spawnRate < 0f)
        {
            SpawnAnimal();

            SetSpawnRate();
        }
    }

    void SetSpawnRate()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }

    void SpawnAnimal()
    {
        if (LSpawnAnimalList != null)
        {
            GameObject checkActivatedAnimal = LSpawnAnimalList.Find(animal => !animal.gameObject.activeInHierarchy);

            if (checkActivatedAnimal != null)
            {
                checkActivatedAnimal.gameObject.SetActive(true);
                checkActivatedAnimal.transform.position = transform.position;
            }
            else
            {
                GameObject newAnimal = Instantiate(AnimalPrefab, transform.position, Quaternion.identity);
                LSpawnAnimalList.Add(newAnimal);
            }
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} : Animal List가 설정되지 않았습니다.");
        }
    }
}
