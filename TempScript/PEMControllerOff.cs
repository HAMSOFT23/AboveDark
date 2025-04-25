#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PEMControllerOff : MonoBehaviour {	
	[SerializeField]
	private InputActionReference toggleLight;
	public bool _Off;
	public GameObject light;
	
	private void Update() {
		MaxyGames.UNode.GraphDebug.Flow(this, -1688373313, 92, "exit");
		if(MaxyGames.UNode.GraphDebug.Value(toggleLight.action.triggered, this, -1688373313, 94, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, -1688373313, 94, "onTrue");
			Toggle();
			MaxyGames.UNode.GraphDebug.FlowNode(this, -1688373313, 98, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -1688373313, 94, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, -1688373313, 94, false);
		}
	}
	
	public void Toggle() {
		MaxyGames.UNode.GraphDebug.Flow(this, -1688373313, 24, "exit");
		light.SetActive(true);
		MaxyGames.UNode.GraphDebug.Flow(this, -1688373313, 95, "exit");
		MaxyGames.UNode.GraphDebug.Value(this.GetComponent<PEMControllerOn>()._On = true, this, -1688373313, 106, "target", true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, -1688373313, 106, true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, -1688373313, 95, true);
	}
}

