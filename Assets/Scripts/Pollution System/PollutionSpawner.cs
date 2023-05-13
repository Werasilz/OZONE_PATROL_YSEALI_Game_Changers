using System.Collections;
using UnityEngine;

public class PollutionSpawner : Singleton<PollutionSpawner>
{
    [Header("Requester")]
    [SerializeField] private GameObject pollutionMonsterPrefab;

    [Header("Spawn Point")]
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;

    [Header("Spawner")]
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private float elapsedTime;

    public void Start()
    {
        // Start spawner
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        for (int i = 0; i < 100; i++)
        {
            elapsedTime = Random.Range(minSpawnTime, maxSpawnTime + 1f);

            while (elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;

                yield return null;
            }

            // Clear timer
            elapsedTime = 0;

            // Spawn the pollution
            Vector3 spawnPosition = new Vector3(Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.x), leftSpawnPoint.position.y);
            GameObject newPollutionMonster = Instantiate(pollutionMonsterPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
