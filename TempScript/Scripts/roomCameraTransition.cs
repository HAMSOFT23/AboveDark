#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;

public class roomCameraTransition : MaxyGames.UNode.RuntimeBehaviour {	
	public UnityEngine.GameObject player;
	public UnityEngine.GameObject mainCam;
	public UnityEngine.GameObject roomCam;
	public Cinemachine.CinemachineVirtualCamera newVariable;
	
	private void Start() {
		player = UnityEngine.GameObject.FindGameObjectWithTag("Player");
	}
	
	private void Update() {
	}
	
	public void Transition() {
		mainCam.SetActive(false);
		roomCam.SetActive(true);
		if(roomCam) {
			roomCam.GetComponent<Cinemachine.CinemachineVirtualCameraBase>().Follow = player.transform;
		}
	}
}

