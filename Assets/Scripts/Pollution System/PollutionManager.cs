using UnityEngine;
using UnityEngine.UI;

public class PollutionManager : Singleton<PollutionManager>
{
    [SerializeField] private int pollutionScore;
    [SerializeField] private Image pollutionIndicator;

    [Header("Popup")]
    [SerializeField] private GameObject minusPopup;
    [SerializeField] private GameObject plusPopup;

    public void AddPollutionScore(int score, Transform popupSpawnPoint)
    {
        pollutionScore += score;

        if (pollutionScore > 100)
        {
            pollutionScore = 100;
        }

        pollutionIndicator.fillAmount = pollutionScore / 100f;

        if (popupSpawnPoint != null)
        {
            GameObject newPopup = Instantiate(minusPopup, popupSpawnPoint.position, Quaternion.identity);
        }
    }

    public void ReducePollutionScore(int score, Transform popupSpawnPoint)
    {
        pollutionScore -= score;

        if (pollutionScore < 0)
        {
            pollutionScore = 0;
        }

        pollutionIndicator.fillAmount = pollutionScore / 100f;

        if (popupSpawnPoint != null)
        {
            GameObject newPopup = Instantiate(plusPopup, popupSpawnPoint.position, Quaternion.identity);
        }
    }
}
