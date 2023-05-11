using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script must have to place in first scene of game
public class LoadPersistentScene : MonoBehaviour
{
    [Header("Scene to load")]
    [SerializeField] private SceneField persistentScene;

    private void Awake()
    {
        // If persistent scene never load before
        if (!SceneManager.GetSceneByName(persistentScene.Name).isLoaded)
        {
            // Load persistent scene
            SceneManager.LoadSceneAsync(persistentScene.Name, LoadSceneMode.Additive);
        }
    }
}
