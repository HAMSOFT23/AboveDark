#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class interactableDoor : MonoBehaviour {	
	public bool _playerIn;
	public GameObject player;
	public AudioClip lockedSfx;
	private AudioSource audiSou;
	
	private void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		audiSou = base.GetComponent<UnityEngine.AudioSource>();
	}
	
	private void OnTriggerEnter2D(Collider2D colliderInfo) {
		if(colliderInfo.gameObject.CompareTag("Player")) {
			_playerIn = true;
			player.GetComponent<interactionManager>()._isInteractable = true;
		}
	}
	
	private void OnTriggerExit2D(Collider2D colliderInfo) {
		if(colliderInfo.gameObject.CompareTag("Player")) {
			_playerIn = false;
			player.GetComponent<interactionManager>()._isInteractable = false;
		}
	}
	
	private void Update() {
		Interaction();
	}
	
	public void Interaction() {
		if(((_playerIn == true) && player.GetComponent<interactionManager>().interactionKey.action.triggered)) {
			audiSou.pitch = Random.Range(0.8F, 1F);
			audiSou.PlayOneShot(lockedSfx, 1F);
		}
	}
}

