using System.Collections;
using UnityEngine;

public class PollutionSpawner : Singleton<PollutionSpawner>
{
    [Header("Pollution")]
    [SerializeField] private GameObject pollutionMonsterPrefab;

    [Header("Spawn Point")]
    [SerializeField] private Transform bottomLeftSpawnPoint;
    [SerializeField] private Transform bottomRightSpawnPoint;
    [SerializeField] private Transform topLeftSpawnPoint;
    [SerializeField] private Transform topRightSpawnPoint;

    [Header("Spawner")]
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private float elapsedTime;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    public void Spawn()
    {
        // Spawn the pollution
        Vector3 spawnPosition = new Vector3(Random.Range(bottomLeftSpawnPoint.position.x, bottomRightSpawnPoint.position.x), Random.Range(bottomLeftSpawnPoint.position.y, topLeftSpawnPoint.position.y));
        GameObject newPollutionMonster = Instantiate(pollutionMonsterPrefab, spawnPosition, Quaternion.identity);
    }

    IEnumerator Spawner()
    {
        for (int i = 0; i < 200; i++)
        {
            if (PollutionManager.Instance.playState == PlayState.Boss)
            {
                break;
            }

            elapsedTime = Random.Range(minSpawnTime, maxSpawnTime + 1f);

            while (elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;

                yield return null;
            }

            // Clear timer
            elapsedTime = 0;

            // Spawn the pollution
            Vector3 spawnPosition = new Vector3(Random.Range(bottomLeftSpawnPoint.position.x, bottomRightSpawnPoint.position.x), Random.Range(bottomLeftSpawnPoint.position.y, topLeftSpawnPoint.position.y));
            GameObject newPollutionMonster = Instantiate(pollutionMonsterPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
