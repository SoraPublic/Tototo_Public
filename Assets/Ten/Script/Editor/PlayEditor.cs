using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayEditor
{
    private static string rememberPath;
    private static string FoldOutStateKey => $"{nameof(PlayEditor)}{nameof(rememberPath)}";

    [MenuItem("Tools/TestPlay")]
    static void Play()
    {
        if (!EditorApplication.isPlaying)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                rememberPath = EditorSceneManager.GetActiveScene().path;
                SessionState.SetString(FoldOutStateKey, rememberPath);
                EditorSceneManager.OpenScene("Assets/Takaaki/Scene/Title_Takaaki.unity");
                EditorApplication.ExecuteMenuItem("Edit/Play");
            }
        }
        else
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(WaitOff());
        }
    }

    static IEnumerator WaitOff()
    {
        EditorApplication.isPlaying = false;
        yield return new WaitUntil(() => !EditorApplication.isPlayingOrWillChangePlaymode);
        string path = SessionState.GetString(FoldOutStateKey, rememberPath);
        EditorSceneManager.OpenScene(path);
        yield break;
    }
}
