using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneDisplay : Editor
{
    //Load Menu Scene 1
    [MenuItem("SceneDisplay/Scenes/ Open Main Menu Scene")]

    static void LoadScene1()
    {
        //Load Scene
        EditorSceneManager.OpenScene("Assets/inGameAssets/Scene/Main Menu.unity");
    }

    //Load Zone #1
    [MenuItem("SceneDisplay/Scenes/ Open Bunker Scene")]

    static void LoadScene2()
    {
        //Load Scene
        EditorSceneManager.OpenScene("Assets/inGameAssets/Scene/Zone #1/Búnker.unity");
    }
}
