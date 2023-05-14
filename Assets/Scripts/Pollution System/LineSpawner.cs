using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    [Header("People")]
    [SerializeField] private GameObject[] peoplePrefabs;
    [SerializeField] private List<People> spawnedPeople;
    public List<People> GetSpawnedPeople => spawnedPeople;
    private Coroutine spawnPeopleCoroutine;

    [Header("Spawn Point")]
    [SerializeField] private Transform outsideStandingPoint;
    [SerializeField] private Transform[] standingPoints;

    [Header("Spawn Time")]
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    void Start()
    {
        spawnedPeople = new List<People>();

        spawnPeopleCoroutine = StartCoroutine(SpawnPeople());

        IEnumerator SpawnPeople()
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            for (int i = 0; i < 5; i++)
            {
                GameObject newPeople = Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Length)], outsideStandingPoint.position, Quaternion.identity);
                People people = newPeople.GetComponent<People>();
                people.Init(this, standingPoints[i].position);
                spawnedPeople.Add(people);

                yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime + 1));
            }
        }
    }

    [ContextMenu("Remove First People")]
    public void RemoveFirstPeople()
    {
        if (spawnedPeople.Count > 0)
        {
            // Remove the first point
            spawnedPeople.RemoveAt(0);

            // Move one step
            for (int i = 0; i < spawnedPeople.Count; i++)
            {
                spawnedPeople[i].StartMove(standingPoints[i].position);
            }

            // Spawn new people
            GameObject newPeople = Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Length)], outsideStandingPoint.position, Quaternion.identity);
            People people = newPeople.GetComponent<People>();
            people.Init(this, standingPoints[spawnedPeople.Count].position);
            spawnedPeople.Add(people);
        }
    }

    public void ClearAllPeople()
    {
        StopCoroutine(spawnPeopleCoroutine);

        for (int i = 0; i < spawnedPeople.Count; i++)
        {
            if (spawnedPeople[i].gameObject == null) continue;

            Destroy(spawnedPeople[i].gameObject);
        }
    }
}
