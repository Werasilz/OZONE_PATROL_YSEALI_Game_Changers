using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    [Header("People")]
    [SerializeField] private GameObject[] peoplePrefabs;
    [SerializeField] private Transform[] standingPoints;
    [SerializeField] private List<People> spawnedPeople;

    [Header("Spawn")]
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    void Start()
    {
        spawnedPeople = new List<People>();

        StartCoroutine(SpawnPeople());

        IEnumerator SpawnPeople()
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject newPeople = Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Length)], standingPoints[i].position, Quaternion.identity);
                People people = newPeople.GetComponent<People>();
                people.lineSpawner = this;
                spawnedPeople.Add(people);

                // Start for first index
                if (i == 0)
                {
                    spawnedPeople[i].StartCounting();
                }

                yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime + 1));
            }
        }
    }

    [ContextMenu("Remove First People")]
    public void RemoveFirstPeople()
    {
        if (spawnedPeople.Count > 0)
        {
            StartCoroutine(RemoveOrder());

            IEnumerator RemoveOrder()
            {
                spawnedPeople[0].StartCounting();

                // Remove the first point
                Destroy(spawnedPeople[0].gameObject);
                spawnedPeople.RemoveAt(0);

                // Move one step
                for (int i = 0; i < spawnedPeople.Count; i++)
                {
                    spawnedPeople[i].transform.position = standingPoints[i].position;
                }

                yield return new WaitForSeconds(0.5f);

                // Spawn new people
                GameObject newPeople = Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Length)], standingPoints[standingPoints.Length - 1].position, Quaternion.identity);
                People people = newPeople.GetComponent<People>();
                people.lineSpawner = this;
                spawnedPeople.Add(people);
            }
        }
    }
}
