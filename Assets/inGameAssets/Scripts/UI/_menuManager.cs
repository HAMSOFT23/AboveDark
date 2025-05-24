#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Experimental.VFX;

public class _pauseMenuManager : MonoBehaviour {	
	[SerializeField]
	private InputActionReference exitKey;
	[SerializeField]
	private CanvasGroup myUIGroup;
	[SerializeField]
	private GameObject options;
	public Animator vig;
	[SerializeField]
	private bool fadeIn;
	[SerializeField]
	private bool fadeOut;
	private bool buttonPressed;
	
	private void Update() {
		MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 41, "exit");
		if(MaxyGames.UNode.GraphDebug.Value(fadeIn, this, -337483660, 13, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 13, "onTrue");
			if(MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(myUIGroup, this, -337483660, 17, "-instance", false).alpha, this, -337483660, 18, "inputA", false) < 1), this, -337483660, 15, "condition", false)) {
				MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 15, "onTrue");
				MaxyGames.UNode.GraphDebug.Value(myUIGroup.alpha += Time.deltaTime, this, -337483660, 21, "target", true);
				MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 21, "exit");
				if(MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(myUIGroup, this, -337483660, 24, "-instance", false).alpha, this, -337483660, 25, "inputA", false) >= 1), this, -337483660, 22, "condition", false)) {
					MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 22, "onTrue");
					MaxyGames.UNode.GraphDebug.Value(fadeIn = false, this, -337483660, 26, "target", true);
					MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 26, true);
					MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 22, true);
				} else {
					MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 22, false);
				}
				MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 21, true);
				MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 15, true);
			} else {
				MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 15, false);
			}
			MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 13, "exit");
			if(MaxyGames.UNode.GraphDebug.Value(fadeOut, this, -337483660, 27, "condition", false)) {
				MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 27, "onTrue");
				if(MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(myUIGroup, this, -337483660, 31, "-instance", false).alpha, this, -337483660, 32, "inputA", false) >= 0), this, -337483660, 29, "condition", false)) {
					MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 29, "onTrue");
					MaxyGames.UNode.GraphDebug.Value(myUIGroup.alpha -= Time.deltaTime, this, -337483660, 35, "target", true);
					MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 35, "exit");
					if(MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(myUIGroup, this, -337483660, 38, "-instance", false).alpha, this, -337483660, 39, "inputA", false) == 0), this, -337483660, 36, "condition", false)) {
						MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 36, "onTrue");
						MaxyGames.UNode.GraphDebug.Value(fadeOut = false, this, -337483660, 40, "target", true);
						MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 40, "exit");
						options.SetActive(false);
						MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 67, true);
						MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 40, true);
						MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 36, true);
					} else {
						MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 36, false);
					}
					MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 35, true);
					MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 29, true);
				} else {
					MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 29, false);
				}
				MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 27, true);
			} else {
				MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 27, false);
			}
			MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 13, true);
		} else {
			MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 13, "exit");
			if(MaxyGames.UNode.GraphDebug.Value(fadeOut, this, -337483660, 27, "condition", false)) {
				MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 27, "onTrue");
				if(MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(myUIGroup, this, -337483660, 31, "-instance", false).alpha, this, -337483660, 32, "inputA", false) >= 0), this, -337483660, 29, "condition", false)) {
					MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 29, "onTrue");
					MaxyGames.UNode.GraphDebug.Value(myUIGroup.alpha -= Time.deltaTime, this, -337483660, 35, "target", true);
					MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 35, "exit");
					if(MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(myUIGroup, this, -337483660, 38, "-instance", false).alpha, this, -337483660, 39, "inputA", false) == 0), this, -337483660, 36, "condition", false)) {
						MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 36, "onTrue");
						MaxyGames.UNode.GraphDebug.Value(fadeOut = false, this, -337483660, 40, "target", true);
						MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 40, "exit");
						options.SetActive(false);
						MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 67, true);
						MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 40, true);
						MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 36, true);
					} else {
						MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 36, false);
					}
					MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 35, true);
					MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 29, true);
				} else {
					MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 29, false);
				}
				MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 27, true);
			} else {
				MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 27, false);
			}
			MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 13, false);
		}
	}
	
	private void FixedUpdate() {
		MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 86, "exit");
		if(MaxyGames.UNode.GraphDebug.Value(exitKey.action.triggered, this, -337483660, 89, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 89, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(buttonPressed = true, this, -337483660, 88, "target", true);
			MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 88, "exit");
			vig.Play("vgFXfadeIn");
			MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 96, "exit");
			PauseAction();
			MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 91, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 96, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 88, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 89, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 89, false);
		}
	}
	
	public void PauseAction() {
		MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 10, "exit");
		if(buttonPressed) {
			MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 57, "onTrue");
			options.SetActive(true);
			MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 76, "exit");
			MaxyGames.UNode.GraphDebug.Value(fadeIn = true, this, -337483660, 9, "target", true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 9, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 76, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 57, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 57, false);
		}
	}
	
	public void ResumeGame() {
		MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 12, "exit");
		MaxyGames.UNode.GraphDebug.Value(fadeOut = true, this, -337483660, 11, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 11, "exit");
		MaxyGames.UNode.GraphDebug.Value(buttonPressed = false, this, -337483660, 75, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 75, "exit");
		vig.Play("vgFXfadeOut");
		MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 101, true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 75, true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 11, true);
	}
	
	public void QuitGame() {
		MaxyGames.UNode.GraphDebug.Flow(this, -337483660, 63, "exit");
		Application.Quit();
		MaxyGames.UNode.GraphDebug.FlowNode(this, -337483660, 65, true);
	}
}

