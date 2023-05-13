using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class People : MonoBehaviour
{
    public LineSpawner lineSpawner;
    private Coroutine countingCoroutine;
    [SerializeField] private GameObject pollutionMonsterPrefab;
    [SerializeField] private float minStartTime;
    [SerializeField] private float maxStartTime;
    [SerializeField] private float timeElapsed = 0f;
    [SerializeField] private Image timeIndicator;

    public void StartCounting()
    {
        countingCoroutine = StartCoroutine(Counting());
    }

    public void StopCounting()
    {
        StopCoroutine(countingCoroutine);
    }

    private IEnumerator Counting()
    {
        float startingTime = Random.Range(minStartTime, maxStartTime + 1);
        timeElapsed = startingTime;

        while (timeElapsed > 0)
        {
            timeElapsed -= Time.deltaTime;
            float value = timeElapsed / startingTime;
            timeIndicator.fillAmount = value;
            yield return null;
        }

        timeElapsed = 0;

        lineSpawner.RemoveFirstPeople();
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y);
        GameObject newPollutionMonster = Instantiate(pollutionMonsterPrefab, spawnPosition, Quaternion.identity);
    }
}
