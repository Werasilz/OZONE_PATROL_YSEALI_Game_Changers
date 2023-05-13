using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : Singleton<RequestManager>
{
    [Header("Requester")]
    [SerializeField] private int requesterAmount;
    [SerializeField] private GameObject requesterPrefab;

    [Header("Grid")]
    [SerializeField] private CellData[] cellDatas;

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

            // Clear timer
            elapsedTime = 0;

            // Find available cell
            int index = FindAvailableCellIndex();

            if (index != -1)
            {
                CellData cellData = cellDatas[index];
                GameObject newRequester = Instantiate(requesterPrefab, cellData.cellTransform.position, cellData.cellTransform.rotation);
                cellDatas[index].requester = newRequester;
            }
            else
            {
                Debug.Log("No available cell found.");
            }
        }
    }

    int FindAvailableCellIndex()
    {
        List<int> availableIndices = new List<int>();

        for (int i = 0; i < cellDatas.Length; i++)
        {
            if (cellDatas[i].requester == null)
            {
                availableIndices.Add(i);
            }
        }

        if (availableIndices.Count > 0)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            return availableIndices[randomIndex];
        }

        // No available cell found
        return -1;
    }


    [System.Serializable]
    struct CellData
    {
        public Transform cellTransform;
        public GameObject requester;
    }
}
