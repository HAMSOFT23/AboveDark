#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;

public class roomCameraTransition : MonoBehaviour {	
	[Header("Cameras")]
	public GameObject playerCam;
	public GameObject roomCam;
	[Header("Objective")]
	[Tooltip("The Player Character Game Object. It attaches itself ")]
	public GameObject player;
	
	private void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	public void EntryTransition() {
		Debug.Log("Transition Activated!!!!!!!!!!!!!!!!!!!!!!!!");
		playerCam.SetActive(false);
		roomCam.SetActive(true);
		if(roomCam) {
			roomCam.GetComponent<CinemachineVirtualCameraBase>().Follow = player.transform;
		}
	}
	
	public void ExitTranstion() {
		playerCam.SetActive(true);
		roomCam.SetActive(false);
		if(roomCam) {
			roomCam.GetComponent<CinemachineVirtualCameraBase>().Follow = (null as Transform);
		}
	}
}

