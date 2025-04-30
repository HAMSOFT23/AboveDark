#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class exitController : MonoBehaviour {	
	private bool timerOn;
	private float elapsed;
	private bool paused;
	private float duration;
	public bool _playerOut;
	public GameObject _player;
	public GameObject parentDoor;
	public GameObject defaultCamera;
	public GameObject roomCamera;
	public GameObject roomCameraManager;
	[Range(0F, 2F)]
	public float waitTime;
	
	private void Update() {
		MaxyGames.UNode.GraphDebug.Flow(this, -646259854, 24, "exit");
		if(_playerOut) {
			MaxyGames.UNode.GraphDebug.Flow(this, -646259854, 76, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(roomCameraManager, this, -646259854, 173, "input", false).GetComponent<roomCameraTransition>(), this, -646259854, 169, "-instance", false).ExitTranstion();
			MaxyGames.UNode.GraphDebug.FlowNode(this, -646259854, 169, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -646259854, 76, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, -646259854, 76, false);
		}
		if(timerOn && !paused) {
			elapsed += Time.deltaTime;
			if(elapsed >= duration) {
				elapsed = 0;
				timerOn = false;
				MaxyGames.UNode.GraphDebug.Flow(this, -646259854, 150, "onFinished");
				Debug.Log("Outside The Room");
				MaxyGames.UNode.GraphDebug.Flow(this, -646259854, 32, "exit");
				MaxyGames.UNode.GraphDebug.Value(_player.transform.position = MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(parentDoor, this, -646259854, 45, "input", false).GetComponent<teleportingDoor>(), this, -646259854, 42, "-instance", false).defautlPositon, this, -646259854, 37, "-instance", false).transform.position, this, -646259854, 36, "value", false), this, -646259854, 36, "target", true);
				MaxyGames.UNode.GraphDebug.FlowNode(this, -646259854, 36, true);
				MaxyGames.UNode.GraphDebug.FlowNode(this, -646259854, 32, true);
			}
		}
	}
	
	private void Awake() {
		MaxyGames.UNode.GraphDebug.Flow(this, -646259854, 47, "exit");
		MaxyGames.UNode.GraphDebug.Value(parentDoor = MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(this, this, -646259854, 56, "input", false).transform, this, -646259854, 53, "-instance", false).parent, this, -646259854, 54, "input", false).gameObject, this, -646259854, 49, "value", false), this, -646259854, 49, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, -646259854, 49, "exit");
		MaxyGames.UNode.GraphDebug.Value(_player = MaxyGames.UNode.GraphDebug.Value(GameObject.FindGameObjectWithTag("Player"), this, -646259854, 57, "value", false), this, -646259854, 57, "target", true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, -646259854, 57, true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, -646259854, 49, true);
	}
	
	private void OnTriggerEnter2D(Collider2D colliderInfo) {
		MaxyGames.UNode.GraphDebug.Flow(this, -646259854, 11, "exit");
		if(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(colliderInfo, this, -646259854, 22, "input", false).gameObject, this, -646259854, 15, "-instance", false).CompareTag("Player"), this, -646259854, 14, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, -646259854, 14, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(_playerOut = true, this, -646259854, 16, "target", true);
			MaxyGames.UNode.GraphDebug.Flow(this, -646259854, 16, "exit");
			MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(roomCameraManager, this, -646259854, 186, "input", false).GetComponent<SpriteFader>(), this, -646259854, 185, "-instance", false).PerformFullTransition();
			MaxyGames.UNode.GraphDebug.Flow(this, -646259854, 185, "exit");
			PlayerOut();
			MaxyGames.UNode.GraphDebug.FlowNode(this, -646259854, 151, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -646259854, 185, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -646259854, 16, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -646259854, 14, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, -646259854, 14, false);
		}
	}
	
	public void PlayerOut() {
		MaxyGames.UNode.GraphDebug.Flow(this, -646259854, 27, "exit");
		if(!timerOn) {
			timerOn = true;
			elapsed = 0;
			paused = false;
			duration = 0.8F;
		}
	}
}

