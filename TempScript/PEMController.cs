#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PEMController : MonoBehaviour {	
	public float lightIntensity;
	public GameObject light;
	[SerializeField]
	private InputActionReference toggleLight;
	public int tapCount;
	
	private void Awake() {
		MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 108, "exit");
		MaxyGames.UNode.GraphDebug.Value(lightIntensity = MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(light, this, 1909372234, 154, "input", false).GetComponent<UnityEngine.Rendering.Universal.Light2D>(), this, 1909372234, 151, "-instance", false).intensity, this, 1909372234, 150, "value", false), this, 1909372234, 150, "target", true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 150, true);
	}
	
	private void Start() {
		MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 156, "exit");
		MaxyGames.UNode.GraphDebug.Value(light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0F, this, 1909372234, 158, "target", true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 158, true);
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
			MaxyGames.UNode.GraphDebug.Value(light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = lightIntensity, this, 1909372234, 172, "target", true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 172, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 146, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 146, false);
		}
	}
	
	public void ToggleOff() {
		MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 119, "exit");
		if(MaxyGames.UNode.GraphDebug.Value((tapCount == 0), this, 1909372234, 123, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 123, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0F, this, 1909372234, 171, "target", true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 171, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 123, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 123, false);
		}
	}
}

