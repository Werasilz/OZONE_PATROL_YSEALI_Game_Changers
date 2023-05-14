using UnityEngine;

public class SceneNavigation : MonoBehaviour
{
    public void LoadMainMenu()
    {
        FindObjectOfType<SceneLoaderManager>().LoadMainMenu();
    }

    public void LoadLevelSelect()
    {
        FindObjectOfType<SceneLoaderManager>().LoadLevelSelect();
    }

    public void LoadCityMap()
    {
        FindObjectOfType<SceneLoaderManager>().LoadCityMap();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
