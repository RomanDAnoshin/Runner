using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ScenesMenu : MonoBehaviour
{
    private static void OpenScene(string scene)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
            EditorSceneManager.OpenScene(scene);
        }
    }

    [MenuItem("Scenes/MainMenu")]
    public static void LoadSceneMainMenu()
    {
        OpenScene("Assets/Scenes/MainMenu.unity");
    }

    [MenuItem("Scenes/Road")]
    public static void LoadSceneRoad()
    {
        OpenScene("Assets/Scenes/Road.unity");
    }
}
