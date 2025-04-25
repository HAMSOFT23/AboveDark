#pragma warning disable
using UnityEngine;
using System.Collections.Generic;

public class accessDoorController : MonoBehaviour {	
	private Animator _anim;
	private AudioSource _audioSou;
	[SerializeField]
	[Tooltip("The Player has entered the trigger")]
	private bool playerDetected;
	[Header("Audio Volume")]
	[Tooltip("The SFX that plays when the door opens")]
	[Range(0F, 1F)]
	public float openingVolume;
	[Tooltip("The SFX that plays when the door detects the player")]
	[Range(0F, 1F)]
	public float detectionVolume;
	[Tooltip("The SFX that plays when the door detects the player")]
	[Range(0F, 1F)]
	public float accessVolume;
	[SerializeField]
	[Header("SFX")]
	private List<AudioClip> openingSFX = new List<AudioClip>() { null, null };
	[SerializeField]
	private AudioClip _playerDetectedSFX = null;
	[SerializeField]
	private AudioClip _playerRejectedSFX;
	[SerializeField]
	private AudioClip _playerApprovedSFX;
	private GameObject player;
	private MaxyGames.Runtime.EventCoroutine coroutine1 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine2 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine3 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine4 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine5 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine6 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine7 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine8 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine9 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine10 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine11 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine12 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine13 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine14 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine15 = new MaxyGames.Runtime.EventCoroutine();
	
	private void Start() {
		coroutine1.Run();
		_anim = base.GetComponent<UnityEngine.Animator>();
		_audioSou = (base.GetComponent<UnityEngine.AudioSource>() as AudioSource);
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update() {
		if(coroutine1.IsRunning) {
			coroutine8.Run();
		}
	}
	
	private void OnTriggerEnter2D(Collider2D colliderInfo) {
		if(colliderInfo.gameObject.CompareTag("Player")) {
			playerDetected = true;
		}
	}
	
	void _ActivateTransition(string name) {
		switch(name) {
			case "DoorDetection25": {
				if(coroutine1.IsRunning) {
					coroutine1.Stop(true);
					coroutine2.Run();
				}
			}
			break;
			case "ToOpening33": {
				if(coroutine2.IsRunning) {
					coroutine2.Stop(true);
					coroutine4.Run();
				}
			}
			break;
			case "Negate33": {
				if(coroutine2.IsRunning) {
					coroutine2.Stop(true);
					coroutine1.Run();
				}
			}
			break;
			case "ToOpened52": {
				if(coroutine4.IsRunning) {
					coroutine4.Stop(true);
					coroutine6.Run();
				}
			}
			break;
		}
	}
	
	void Awake() {
		coroutine1.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine1.IsRunning;
		})), () => {
			coroutine7.Stop();
		});
		coroutine2.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {
			coroutine9.Run();
		}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine2.IsRunning;
		})), () => {
			coroutine9.Stop();
			coroutine3.Stop();
			coroutine10.Stop();
			coroutine11.Stop();
		});
		coroutine3.Setup(this, MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.Wait(() => 2F), MaxyGames.Runtime.Routine.New(coroutine11)));
		coroutine4.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {
			coroutine12.Run();
		}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine4.IsRunning;
		})), () => {
			coroutine12.Stop();
			coroutine5.Stop();
			coroutine13.Stop();
			coroutine14.Stop();
		});
		coroutine5.Setup(this, MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.Wait(() => _anim.GetCurrentAnimatorStateInfo(0).length), MaxyGames.Runtime.Routine.New(coroutine13)));
		coroutine6.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {
			coroutine15.Run();
		}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine6.IsRunning;
		})), () => {
			coroutine15.Stop();
		});
		coroutine7.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				_anim.Play("Door Closed", 0);
				if(playerDetected) {
					_ActivateTransition("DoorDetection25");
				}
		}));
		coroutine8.Setup(this, MaxyGames.Runtime.Routine.New(() => {
			coroutine7.Run();
		}));
		coroutine9.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				_anim.Play("Door Detection", 0);
				return coroutine10.Run();
		}));
		coroutine10.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				_audioSou.PlayOneShot(_playerDetectedSFX, detectionVolume);
				return coroutine3.Run();
		}));
		coroutine11.Setup(this, new MaxyGames.Runtime.Conditional(() => player.GetComponent<playerController>()._keyPicked, onTrue: MaxyGames.Runtime.EventCoroutine.Create(this, MaxyGames.Runtime.Routine.New(() => {
			_audioSou.PlayOneShot(_playerApprovedSFX, accessVolume);
			Debug.Log("Access Aproved");
			_ActivateTransition("ToOpening33");
		})), onFalse: MaxyGames.Runtime.EventCoroutine.Create(this, MaxyGames.Runtime.Routine.New(() => {
			_audioSou.PlayOneShot(_playerRejectedSFX, accessVolume);
			playerDetected = false;
			Debug.Log("Access Denied");
			_ActivateTransition("Negate33");
		}))));
		coroutine12.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				_anim.Play("Door Opening", 0);
				return coroutine14.Run();
		}));
		coroutine13.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				_ActivateTransition("ToOpened52");
		}));
		coroutine14.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				_audioSou.PlayOneShot(openingSFX[Random.Range(0, 1)], openingVolume);
				return coroutine5.Run();
		}));
		coroutine15.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				_anim.Play("Door opened", 0);
		}));
	}
}

