using UnityEngine;
using UnityEngine.UI;

public class PollutionManager : Singleton<PollutionManager>
{
    public PlayState playState = PlayState.Normal;

    [Header("Score")]
    [SerializeField] private int pollutionScore;
    [SerializeField] private Image pollutionIndicator;

    [Header("Popup")]
    [SerializeField] private GameObject minusPopup;
    [SerializeField] private GameObject plusPopup;

    [Header("Line")]
    [SerializeField] private LineSpawner[] lineSpawners;

    [Header("ButtonAction")]
    [SerializeField] private ButtonAction[] buttonActions;

    [Header("Boss")]
    [SerializeField] private Animator buttonActionAnimator;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject bossScene;

    [Header("Timer")]
    [SerializeField] private float startTime = 60f;
    [SerializeField] private float elapsedTime = 0;

    private void Start()
    {
        playState = PlayState.Normal;
        elapsedTime = startTime;
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

        pollutionIndicator.fillAmount = pollutionScore / 100f;

        if (popupSpawnPoint != null)
        {
            GameObject newPopup = Instantiate(minusPopup, popupSpawnPoint.position, Quaternion.identity);
        }
    }

    public void ReducePollutionScore(int score, Transform popupSpawnPoint)
    {
        pollutionScore += score;

        if (pollutionScore > 100)
        {
            pollutionScore = 100;
        }

        pollutionIndicator.fillAmount = pollutionScore / 100f;

        if (popupSpawnPoint != null)
        {
            GameObject newPopup = Instantiate(plusPopup, popupSpawnPoint.position, Quaternion.identity);
        }

        if (pollutionScore >= 100)
        {
            BossScene();
        }
    }

    public void BossScene()
    {
        playState = PlayState.Boss;
        buttonActionAnimator.enabled = true;

        for (int j = 0; j < lineSpawners.Length; j++)
        {
            lineSpawners[j].ClearAllPeople();
        }

        for (int i = 0; i < buttonActions.Length; i++)
        {
            buttonActions[i].ClearAction();
        }

        mainCamera.SetActive(false);
        bossScene.SetActive(true);
    }
}
public enum PlayState
{
    Normal,
    Boss
}
