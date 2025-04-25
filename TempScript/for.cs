#pragma warning disable
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneList : MonoBehaviour {	
	private void Start() {
		var sceneNames = new List<string>();
		var sceneCount = SceneManager.sceneCountInBuildSettings;
		for(int index = 0; index < sceneCount; index += 1) {
			var scenePath = SceneUtility.GetScenePathByBuildIndex(index);
			var sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
			sceneNames.Add(sceneName);
		}
		//Print the list of scene names
		foreach(string loopValue in sceneNames) {
			Debug.Log(loopValue);
		}
	}
}

