using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Udar.SceneManager;

public class SceneLoaderManager : MonoBehaviour
{
    [Header("User Interface")]
    [SerializeField] private Slider loadingBar;
    [SerializeField] private TextMeshProUGUI loadingText;

    [Header("Loading Screen Object")]
    [SerializeField] private GameObject loadingCanvas;

    [Space]
    [SerializeField] private SceneField[] scenes;

    [Header("Active Scene")]
    [SerializeField] private SceneIndexes currentActiveScene = SceneIndexes.LoadingScene;

    [Header("Load Progress")]
    [SerializeField] private float progression;

    private bool debugSessionTime;
    private DateTime m_sessionStartTime;
    private DateTime m_sessionEndTime;

    private void Awake()
    {
        m_sessionStartTime = DateTime.Now;
        if (debugSessionTime) Debug.Log("Game Session Start @: " + m_sessionStartTime);

        // First time loading from loading scene to main menu
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndexes.LoadingScene)
        {
            LoadMainMenu();
        }
        // First loading from other scene (possible in editor only)
        else
        {
            // Update Active Scene
            int sceneIndex = (int)SceneManager.GetActiveScene().buildIndex;
            currentActiveScene = (SceneIndexes)sceneIndex;

            // Disable loading screen
            loadingCanvas.SetActive(false);
            progression = 0;
            loadingBar.value = 0;
        }
    }

    private void OnApplicationQuit()
    {
        m_sessionEndTime = DateTime.Now;

        TimeSpan timeDifference = m_sessionEndTime.Subtract(m_sessionStartTime);

        if (debugSessionTime) Debug.Log("Game Session Ended @: " + m_sessionEndTime);
        if (debugSessionTime) Debug.Log("Game Session Lasted " + timeDifference);
    }

    private string GetSceneName(SceneIndexes activeScene)
    {
        return SceneManager.GetSceneByBuildIndex((int)currentActiveScene).name;
    }

    #region Load Scene Shortcut
    [ContextMenu("Load Main Menu")]
    public void LoadMainMenu()
    {
        LoadScene(GetSceneName(currentActiveScene), scenes[(int)SceneIndexes.MainMenu].Name);
    }

    [ContextMenu("Load Level Select")]
    public void LoadLevelSelect()
    {
        LoadScene(GetSceneName(currentActiveScene), scenes[(int)SceneIndexes.LevelSelect].Name);
    }

    [ContextMenu("Load City Map")]
    public void LoadCityMap()
    {
        LoadScene(GetSceneName(currentActiveScene), scenes[(int)SceneIndexes.CityMap].Name);
    }

    [ContextMenu("Load Town Map")]
    public void LoadTownMap()
    {
        LoadScene(GetSceneName(currentActiveScene), scenes[(int)SceneIndexes.TownMap].Name);
    }

    [ContextMenu("Load Forest Map")]
    public void LoadForestMap()
    {
        LoadScene(GetSceneName(currentActiveScene), scenes[(int)SceneIndexes.ForestMap].Name);
    }
    #endregion

    public void LoadScene(string currentSceneName, string nextSceneName)
    {
        StartCoroutine(LoadSceneAsync(currentSceneName, nextSceneName));
    }

    IEnumerator LoadSceneAsync(string currentSceneName, string nextSceneName)
    {
        print(string.Format("[Scene Loader] Load from {0} to {1}", currentSceneName, nextSceneName));

        // Set loading text
        loadingText.text = "Loading... 0%";

        // Enable loading screen
        progression = 0;
        loadingBar.value = 0;
        loadingCanvas.SetActive(true);

        // Unload current scene
        if (currentActiveScene != SceneIndexes.LoadingScene)
        {
            SceneManager.UnloadSceneAsync(currentSceneName);
        }

        // Load next scene
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);

        // Update Active Scene
        int sceneIndex = (int)SceneManager.GetSceneByName(nextSceneName).buildIndex;
        currentActiveScene = (SceneIndexes)sceneIndex;

        // Not allow next scene to active
        asyncOperation.allowSceneActivation = false;

        // Scene load not finished
        while (asyncOperation.isDone == false)
        {
            // Set loading bar progress
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingBar.value = progress;

            // Set loading text progress
            float hundredProgress = progress * 100;
            progression = hundredProgress;
            loadingText.text = "Loading... " + (int)hundredProgress + "%";

            // Load finished
            if (asyncOperation.progress >= 0.9f && !asyncOperation.allowSceneActivation)
            {
                yield return new WaitForSeconds(1);
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        // Disable loading screen
        loadingCanvas.SetActive(false);
        progression = 0;
        loadingBar.value = 0;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextSceneName));
    }
}

public enum SceneIndexes
{
    LoadingScene = 0,
    MainMenu = 1,
    LevelSelect = 2,
    CityMap = 3,
    TownMap = 4,
    ForestMap = 5,
    Summary = 6,
}
