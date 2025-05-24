#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class start : MonoBehaviour {	
	public InputActionReference interaction;
	
	private void Update() {
	}
	
	public void NewGame() {
		MaxyGames.UNode.GraphDebug.Flow(this, 1989447350, 17, "exit");
		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		MaxyGames.UNode.GraphDebug.FlowNode(this, 1989447350, 19, true);
	}
	
	public void QuitGame() {
		MaxyGames.UNode.GraphDebug.Flow(this, 1989447350, 24, "exit");
		Application.Quit();
		MaxyGames.UNode.GraphDebug.FlowNode(this, 1989447350, 27, true);
	}
}

