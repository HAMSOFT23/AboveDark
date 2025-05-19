#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class interactionManager : MonoBehaviour {	
	public InputActionReference interactionKey;
	public InputActionReference escapeKey;
	public bool _isInteractable;
	
	private void Update() {
		InteractionEvent();
		if(escapeKey.action.triggered) {
			Application.Quit();
		}
	}
	
	public void InteractionEvent() {
		if(((_isInteractable == true) && (this.GetComponent<playerController>()._isGrounded == true))) {
			if(interactionKey.action.triggered) {
				Debug.Log("Interaction Key Pressed");
			}
		}
	}
}

