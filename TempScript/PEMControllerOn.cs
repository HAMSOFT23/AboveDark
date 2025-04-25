#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using NaughtyAttributes;
using Rewired;

public class PEMControllerOn : MonoBehaviour {	
	public bool _On;
	public GameObject light;
	private int rewiredPlayerID;
	[Tooltip("The Rewired Player")]
	public Player player;
	[SerializeField]
	private InputActionReference toggleLight;
	public bool toggle;
	public int tapCount;
	
	private void Awake() {
		MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 108, "exit");
		MaxyGames.UNode.GraphDebug.Value(player = MaxyGames.UNode.GraphDebug.Value(ReInput.players.GetPlayer(rewiredPlayerID), this, 1909372234, 110, "value", false), this, 1909372234, 110, "target", true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 110, true);
	}
	
	private void Update() {
		MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 92, "exit");
		if(MaxyGames.UNode.GraphDebug.Value(toggleLight.action.triggered, this, 1909372234, 94, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 94, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(tapCount += 1, this, 1909372234, 133, "target", true);
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 133, "exit");
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 144, "Flow-0");
			if(MaxyGames.UNode.GraphDebug.Value((tapCount == 2), this, 1909372234, 139, "condition", false)) {
				MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 139, "onTrue");
				MaxyGames.UNode.GraphDebug.Value(tapCount = 0, this, 1909372234, 143, "target", true);
				MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 143, true);
				MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 139, true);
			} else {
				MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 139, false);
			}
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 144, "Flow-1");
			ToggleOn();
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 98, "exit");
			ToggleOff();
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 145, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 98, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 133, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 94, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 94, false);
		}
	}
	
	public void ToggleOn() {
		MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 24, "exit");
		if(MaxyGames.UNode.GraphDebug.Value((tapCount == 1), this, 1909372234, 146, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 146, "onTrue");
			light.SetActive(true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 95, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 146, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 146, false);
		}
	}
	
	public void ToggleOff() {
		MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 119, "exit");
		if(MaxyGames.UNode.GraphDebug.Value((tapCount == 0), this, 1909372234, 123, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 123, "onTrue");
			light.SetActive(false);
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 121, "exit");
			MaxyGames.UNode.GraphDebug.Value(_On = false, this, 1909372234, 122, "target", true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 122, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 121, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 123, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 123, false);
		}
	}
}

