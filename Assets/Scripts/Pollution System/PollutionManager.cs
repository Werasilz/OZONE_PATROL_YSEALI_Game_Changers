using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PollutionManager : Singleton<PollutionManager>
{
    public PlayState playState = PlayState.Normal;

    [Header("Happy")]
    [SerializeField] private int happyScore;
    [SerializeField] private TextMeshProUGUI happyScoreText;

    [Header("Score")]
    [SerializeField] private int pollutionScore;
    [SerializeField] private Image pollutionIndicator;
    [SerializeField] private int maxPollutionIndicator;

    [Header("Popup")]
    [SerializeField] private GameObject minusPopup;
    [SerializeField] private GameObject plusPopup;

    [Header("Notify")]
    [SerializeField] private GameObject warningLabel;
    [SerializeField] private GameObject successLabel;

    [Header("Line")]
    [SerializeField] private LineSpawner[] lineSpawners;

    [Header("ButtonAction")]
    [SerializeField] private ButtonAction[] buttonActions;

    [Header("Boss")]
    [SerializeField] private Animator buttonActionAnimator;
    [SerializeField] private Animator mainCameraAnimator;
    [SerializeField] private GameObject bossScene;

    [Header("Timer")]
    [SerializeField] private float startTime = 60f;
    [SerializeField] private float elapsedTime = 0;
    private Coroutine animateHappyScoreCoroutine;

    private void Start()
    {
        playState = PlayState.Normal;
        elapsedTime = startTime;
        pollutionScore = maxPollutionIndicator / 2;
        pollutionIndicator.fillAmount = (float)pollutionScore / (float)maxPollutionIndicator;
        happyScoreText.text = happyScore.ToString();
    }

    private void Update()
    {
        elapsedTime -= Time.deltaTime;

        if (elapsedTime < 0)
        {
            elapsedTime = 0;

            if (playState == PlayState.Normal)
            {
                playState = PlayState.Boss;
                StartCoroutine(BossScene());
            }

            return;
        }
    }

    [ContextMenu("Boss Scene")]
    public void GoToBossScene()
    {
        StartCoroutine(BossScene());
    }

    public void AddPollutionScore(int score, Transform popupSpawnPoint)
    {
        pollutionScore -= score;

        if (pollutionScore < 0)
        {
            pollutionScore = 0;
        }

        pollutionIndicator.fillAmount = (float)pollutionScore / (float)maxPollutionIndicator;

        if (popupSpawnPoint != null)
        {
            GameObject newPopup = Instantiate(minusPopup, popupSpawnPoint.position, Quaternion.identity);
            newPopup.GetComponent<Popup>().SetText("-" + score.ToString());
        }
    }

    public void ReducePollutionScore(int score, Vector3 popupSpawnPoint)
    {
        pollutionScore += score;
        int currentHappyScore = happyScore;
        happyScore += score;

        if (animateHappyScoreCoroutine != null)
        {
            StopCoroutine(animateHappyScoreCoroutine);
        }

        animateHappyScoreCoroutine = StartCoroutine(AnimateHappyScore(currentHappyScore, happyScore));

        if (pollutionScore > maxPollutionIndicator)
        {
            pollutionScore = maxPollutionIndicator;
        }

        pollutionIndicator.fillAmount = (float)pollutionScore / (float)maxPollutionIndicator;

        if (popupSpawnPoint != null)
        {
            GameObject newPopup = Instantiate(plusPopup, popupSpawnPoint, Quaternion.identity);
            newPopup.GetComponent<Popup>().SetText("+" + score.ToString());
        }
    }

    private IEnumerator AnimateHappyScore(int currentScore, int targetScore)
    {
        while (currentScore < targetScore)
        {
            currentScore++;
            happyScoreText.text = currentScore.ToString();

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator BossScene()
    {
        for (int j = 0; j < lineSpawners.Length; j++)
        {
            lineSpawners[j].ClearAllPeople();
        }

        for (int i = 0; i < buttonActions.Length; i++)
        {
            buttonActions[i].ClearAction();
        }

        buttonActionAnimator.enabled = true;

        if (warningLabel.activeInHierarchy == false)
            warningLabel.SetActive(true);
        yield return new WaitForSeconds(3f);
        warningLabel.SetActive(false);
        yield return new WaitForSeconds(1f);

        mainCameraAnimator.enabled = true;
        bossScene.SetActive(true);

        while (true)
        {
            if (pollutionScore > 0)
            {
                pollutionScore -= 10;
            }
            else
            {
                pollutionScore = 0;
            }

            pollutionIndicator.fillAmount = (float)pollutionScore / (float)maxPollutionIndicator;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
public enum PlayState
{
    Normal,
    Boss
}
