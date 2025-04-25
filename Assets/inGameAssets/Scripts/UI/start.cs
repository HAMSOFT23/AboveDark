#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class start : MonoBehaviour {	
	public InputActionReference interaction;
	
	private void Update() {
		MaxyGames.UNode.GraphDebug.Flow(this, 1989447350, 11, "exit");
		if(MaxyGames.UNode.GraphDebug.Value(interaction.action.triggered, this, 1989447350, 13, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, 1989447350, 13, "onTrue");
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1989447350, 15, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1989447350, 13, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1989447350, 13, false);
		}
	}
}

