#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using NaughtyAttributes;
using UnityEngine.UI;

public class menuManager : MonoBehaviour {	
	public InputActionReference escapeKey;
	public bool _isInteractable;
	[SerializeField]
	private GameObject menuOptions;
	
	private void Update() {
		if(escapeKey.action.triggered) {
			menuOptions.SetActive(true);
			menuOptions.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0F, 1F, 1F);
		}
	}
	
	public void resumeGameOption() {
		menuOptions.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1F, 0F, 0F);
		menuOptions.SetActive(false);
	}
	
	public void quitGameOption() {
		Application.Quit();
	}
}

