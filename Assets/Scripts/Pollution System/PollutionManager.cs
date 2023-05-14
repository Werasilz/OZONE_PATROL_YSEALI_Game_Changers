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

        SoundManager.Instance.PlayMusic(0);
    }

    private void Update()
    {
        elapsedTime -= Time.deltaTime;

        if (elapsedTime < 0)
        {
            elapsedTime = 0;
            BossScene();
        }
    }

    [ContextMenu("Boss Scene")]
    public void GoToBossScene()
    {
        BossScene();
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

        if (pollutionScore >= maxPollutionIndicator)
        {
            BossScene();
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

    public void BossScene()
    {
        playState = PlayState.Boss;
        buttonActionAnimator.enabled = true;
        mainCameraAnimator.enabled = true;

        for (int j = 0; j < lineSpawners.Length; j++)
        {
            lineSpawners[j].ClearAllPeople();
        }

        for (int i = 0; i < buttonActions.Length; i++)
        {
            buttonActions[i].ClearAction();
        }

        bossScene.SetActive(true);
    }
}
public enum PlayState
{
    Normal,
    Boss
}
