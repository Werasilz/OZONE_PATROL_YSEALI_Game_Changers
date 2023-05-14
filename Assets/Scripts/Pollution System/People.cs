using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class People : MonoBehaviour
{
    [HideInInspector] public LineSpawner lineSpawner;
    private Coroutine countingCoroutine;

    [Header("Pollution")]
    [SerializeField] private int pollutionReduceScore;
    [SerializeField] private GameObject pollutionMonsterPrefab;

    [Header("Cooldown")]
    private bool m_isStartWaiting;
    public bool IsStartWaiting => m_isStartWaiting;
    [SerializeField] private float minStartTime;
    [SerializeField] private float maxStartTime;
    [SerializeField] private float timeElapsed = 0f;

    [Header("User Interface")]
    [SerializeField] private Image timeIndicatorBG;
    [SerializeField] private Image timeIndicator;

    [Header("Popup")]
    [SerializeField] private Transform popupSpawnPoint;
    [SerializeField] private GameObject popup;

    public void Init(LineSpawner lineSpawner, Vector3 position)
    {
        this.lineSpawner = lineSpawner;

        timeIndicatorBG.enabled = false;
        timeIndicator.enabled = false;

        StartCounting();
        StartMove(position);
    }

    public void StartMove(Vector3 position)
    {
        StartCoroutine(Move(position));
    }

    IEnumerator Move(Vector3 position)
    {
        while (Vector2.Distance(transform.position, position) > 0.1f)
        {
            transform.Translate(Vector3.right * 2f * Time.deltaTime);
            yield return null;
        }

        m_isStartWaiting = true;
    }

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
            if (m_isStartWaiting && lineSpawner.GetSpawnedPeople[0].GetInstanceID() == this.GetInstanceID())
            {
                if (timeIndicator.enabled == false)
                {
                    yield return new WaitForSeconds(0.1f);
                    timeIndicatorBG.enabled = true;
                    timeIndicator.enabled = true;
                }

                timeElapsed -= Time.deltaTime;
                float value = timeElapsed / startingTime;
                timeIndicator.fillAmount = value;
            }

            yield return null;
        }

        timeElapsed = 0;
        lineSpawner.RemoveFirstPeople();
        timeIndicatorBG.enabled = false;
        timeIndicator.enabled = false;

        // Reduce ozone
        PollutionManager.Instance.AddPollutionScore(pollutionReduceScore, popupSpawnPoint);
        int random = Random.Range(0, 3);
        if (random == 0)
            CarSpawner.Instance.Spawn();

        while (transform.localPosition.x < 3)
        {
            transform.Translate(Vector3.right * 4f * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
