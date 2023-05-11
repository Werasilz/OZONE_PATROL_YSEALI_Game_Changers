using UnityEngine;

public class SceneNavigation : MonoBehaviour
{
    public void LoadMainMenu()
    {
        FindObjectOfType<SceneLoaderManager>().LoadMainMenu();
    }

    public void LoadGameplay()
    {
        FindObjectOfType<SceneLoaderManager>().LoadGameplay();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
