using UnityEngine;
using TMPro;
using System.Collections;

[System.Serializable]
public class TimeCounter
{
    [SerializeField] private GameState gameState;

    [Header("Prepare State")]
    [SerializeField] private float prepareTime = 3f;
    [SerializeField] private float prepareTimeElapsed = 0f;
    [SerializeField] private TextMeshProUGUI prepareTimeText;

    [Header("Play State")]
    [SerializeField] private float startingTime = 180f;
    [SerializeField] private float timeElapsed = 0f;
    [SerializeField] private TextMeshProUGUI timeText;

    private bool m_isFinished = false;
    public bool isFinished => m_isFinished;
    private bool m_isPrepare = false;

    public IEnumerator StartPrepareTime()
    {
        gameState = GameState.Prepare;
        m_isPrepare = true;
        prepareTimeElapsed = prepareTime;

        while (prepareTimeElapsed > 0)
        {
            prepareTimeElapsed -= Time.deltaTime;
            prepareTimeText.text = prepareTimeElapsed.ToString("F0");
            yield return null;
        }

        gameState = GameState.Play;
        prepareTimeElapsed = 0;
        prepareTimeText.gameObject.SetActive(false);
        m_isPrepare = false;
    }

    public void SetCountingTimer()
    {
        timeElapsed = startingTime;
        m_isFinished = false;
    }

    public void Counting()
    {
        if (m_isPrepare) return;

        if (timeElapsed > 0)
        {
            timeElapsed -= Time.deltaTime;
            UpdateText(timeElapsed);
        }
        else
        {
            timeElapsed = 0;
            m_isFinished = true;
        }
    }

    private void UpdateText(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

public enum GameState
{
    Prepare,
    Play
}
