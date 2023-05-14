using UnityEngine;

public class SceneNavigation : MonoBehaviour
{
    public void LoadMainMenu()
    {
        FindObjectOfType<SceneLoaderManager>().LoadMainMenu();
    }

    public void LoadLevelSelect()
    {
        SoundManager.Instance.PlaySoundEffect(0);
        FindObjectOfType<SceneLoaderManager>().LoadLevelSelect();
    }

    public void LoadCityMap()
    {
        SoundManager.Instance.PlaySoundEffect(0);
        FindObjectOfType<SceneLoaderManager>().LoadCityMap();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
