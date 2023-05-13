using UnityEngine;
using UnityEngine.UI;

public class PollutionManager : Singleton<PollutionManager>
{
    [SerializeField] private int pollutionScore;
    [SerializeField] private Image pollutionIndicator;

    public void AddPollutionScore(int score)
    {
        pollutionScore += score;
        pollutionIndicator.fillAmount = pollutionScore / 100f;
    }
}
