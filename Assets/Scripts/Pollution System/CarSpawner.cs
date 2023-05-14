using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : Singleton<CarSpawner>
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private Sprite[] carSprites;
    [SerializeField] private Transform spawnPoint;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        GameObject newCar = Instantiate(carPrefab, spawnPoint.position, Quaternion.identity);
        newCar.GetComponent<SpriteRenderer>().sprite = carSprites[Random.Range(0, carSprites.Length)];
    }
}
