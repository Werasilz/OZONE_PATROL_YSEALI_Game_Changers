using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    private bool isPaused;

    public void TogglePauseResume()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
}
