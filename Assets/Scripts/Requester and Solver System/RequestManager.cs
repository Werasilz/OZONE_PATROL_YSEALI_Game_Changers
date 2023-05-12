using System.Collections;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    [Header("Requester")]
    [SerializeField] private int requesterAmount;
    [SerializeField] private GameObject requesterPrefab;

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
        for (int i = 0; i < requesterAmount; i++)
        {
            elapsedTime = Random.Range(minSpawnTime, maxSpawnTime + 1f);

            while (elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;

                yield return null;
            }

            elapsedTime = 0;
        }
    }
}
