using UnityEngine;
using UnityEngine.UI;

public class PollutionManager : Singleton<PollutionManager>
{
    [SerializeField] private int pollutionScore;
    [SerializeField] private Slider pollutionIndicator;

    public void AddPollutionScore(int score)
    {
        pollutionScore += score;
        pollutionIndicator.value = pollutionScore;
    }
}
