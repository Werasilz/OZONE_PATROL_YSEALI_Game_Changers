#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://forum.unity.com/threads/how-to-manually-reload-domain.1263077/
public class EnterPlayModeOptionsWindow : EditorWindow
{
    [MenuItem("Tools/Enter Play Mode Options")]
    public static void ShowWindow()
    {
        var window = GetWindow<EnterPlayModeOptionsWindow>();
        window.titleContent = new GUIContent("Enter Play Mode Options");
        window.Show();
    }

    private bool isDomainReloadDisabled => EditorApplication.isCompiling;
    private bool isSceneReloadDisabled => !EditorApplication.isPlaying;

    private void OnGUI()
    {
        Disableable(DomainReloadButton, isDomainReloadDisabled);
        Disableable(SceneReloadButton, isSceneReloadDisabled);
    }

    private void DomainReloadButton()
    {
        if (GUILayout.Button("Reload Domain"))
        {
            EditorUtility.RequestScriptReload();
        }
    }

    private void SceneReloadButton()
    {
        if (GUILayout.Button("Reload Scene"))
        {
            var scene = SceneManager.GetActiveScene();
            if (scene != null)
            {
                var opts = new LoadSceneParameters { };
                EditorSceneManager.LoadSceneInPlayMode(scene.path, opts);
            }
        }
    }

    private void Disableable(Action renderer, bool disabled)
    {
        EditorGUI.BeginDisabledGroup(disabled);
        renderer();
        EditorGUI.EndDisabledGroup();
    }
}
#endif