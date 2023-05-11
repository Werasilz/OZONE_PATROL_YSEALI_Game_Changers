using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveLoadingSceneManually : MonoBehaviour
{
    [SerializeField] private SceneField loadingScene;

    private void Awake()
    {
        // Find scene loader manager from loading scene
        var sceneLoaderManager = FindObjectOfType<SceneLoaderManager>();

        // Can't find means this enter play directly to gameplay not from loading scene
        if (sceneLoaderManager == null)
        {
            // Load the loading scene
            SceneManager.LoadSceneAsync(loadingScene.Name, LoadSceneMode.Additive);
        }
    }
}
