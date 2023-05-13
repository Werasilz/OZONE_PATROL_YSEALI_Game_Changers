using System.Collections.Generic;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] peoplePrefabs;
    [SerializeField] private Transform[] standingPoints;
    [SerializeField] private List<GameObject> spawnedPeople;

    void Start()
    {
        spawnedPeople = new List<GameObject>();

        for (int i = 0; i < 5; i++)
        {
            GameObject newPeople = Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Length)], standingPoints[i].position, Quaternion.identity);
            spawnedPeople.Add(newPeople);
        }
    }

    [ContextMenu("Remove First Point")]
    public void RemoveFirstPoint()
    {
        // Remove the first point
        Destroy(spawnedPeople[0]);
        spawnedPeople.RemoveAt(0);

        // Move one step
        for (int i = 0; i < spawnedPeople.Count; i++)
        {
            spawnedPeople[i].transform.position = standingPoints[i].position;
        }

        // Spawn new people
        GameObject newPeople = Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Length)], standingPoints[standingPoints.Length - 1].position, Quaternion.identity);
        spawnedPeople.Add(newPeople);
    }
}
