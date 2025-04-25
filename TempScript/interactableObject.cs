#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;
using AllIn1SpriteShader;

public class interactableObject : MonoBehaviour {	
	public GameObject _player;
	public bool _onInteractiontRange;
	public Material mat;
	
	private void Start() {
		_player = GameObject.FindGameObjectWithTag("Player");
		mat = base.GetComponent<UnityEngine.Renderer>().material;
	}
	
	private void Update() {
		KeyPicked();
		ShineVFX();
	}
	
	private void OnTriggerEnter2D(Collider2D colliderInfo) {
		if(colliderInfo.gameObject.CompareTag("Player")) {
			_onInteractiontRange = true;
			_player.GetComponent<interactionManager>()._isInteractable = true;
		}
	}
	
	private void OnTriggerExit2D(Collider2D colliderInfo) {
		if(colliderInfo.gameObject.CompareTag("Player")) {
			_onInteractiontRange = false;
			_player.GetComponent<interactionManager>()._isInteractable = false;
		}
	}
	
	public void KeyPicked() {
		if(((_onInteractiontRange == true) && _player.GetComponent<interactionManager>().interactionKey.action.triggered)) {
			Debug.Log("Ha sido recogida");
			_player.GetComponent<playerController>()._keyPicked = true;
			Object.Destroy(this.gameObject.gameObject);
		}
	}
	
	public void ShineVFX() {
		mat.SetFloat("_ShineLocation", Mathf.Lerp(0F, 1F, Random.Range(1F, 3F)));
	}
}

