#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class teleportingDoor : MonoBehaviour {	
	private bool timerOn;
	private float elapsed;
	private bool paused;
	private float duration;
	public GameObject player;
	[SerializeField]
	[Header("Player Checking")]
	private bool playerIn;
	[SerializeField]
	[Header("Teleport Transforms")]
	public Transform defautlPositon;
	[SerializeField]
	public Transform teleportingPosition;
	public GameObject childDoor;
	public GameObject roomCamManager;
	public float durationEntrance;
	
	private void Update() {
		if(((playerIn == true) && player.GetComponent<interactionManager>().interactionKey.action.triggered)) {
			roomCamManager.GetComponent<SpriteFader>().PerformFullTransition();
			Teleport();
		}
		if(timerOn && !paused) {
			elapsed += Time.deltaTime;
			if(elapsed >= duration) {
				elapsed = 0;
				timerOn = false;
				Debug.Log("Teleporting");
				player.transform.position = teleportingPosition.transform.position;
				roomCamManager.GetComponent<roomCameraTransition>().EntryTransition();
			}
		}
	}
	
	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		defautlPositon = this.transform.GetChild(0);
		teleportingPosition = this.transform.GetChild(1);
	}
	
	private void OnTriggerEnter2D(Collider2D colliderInfo) {
		if(colliderInfo.gameObject.CompareTag("Player")) {
			playerIn = true;
			player.GetComponent<interactionManager>()._isInteractable = true;
			childDoor.GetComponent<exitController>()._playerOut = false;
		}
	}
	
	private void OnTriggerExit2D(Collider2D colliderInfo) {
		if(colliderInfo.gameObject.CompareTag("Player")) {
			playerIn = false;
			player.GetComponent<interactionManager>()._isInteractable = false;
		}
	}
	
	public void Teleport() {
		if(!timerOn) {
			timerOn = true;
			elapsed = 0;
			paused = false;
			duration = durationEntrance;
		}
	}
}

