#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class accessKey : MonoBehaviour {	
	public GameObject _player;
	public bool _onInteractiontRange;
	public Material mat;
	public Animator animator;
	private MaxyGames.Runtime.EventCoroutine coroutine1 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine2 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine3 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine4 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine5 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine6 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine7 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine8 = new MaxyGames.Runtime.EventCoroutine();
	
	void Start() {
		coroutine1.Run();
	}
	
	private void Awake() {
		coroutine1.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {
			coroutine5.Run();
		}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine1.IsRunning;
		})), () => {
			coroutine5.Stop();
			coroutine2.Stop();
			coroutine6.Stop();
		});
		coroutine2.Setup(this, MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.Wait(() => Random.Range(1, 3)), MaxyGames.Runtime.Routine.New(coroutine6)));
		coroutine3.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {
			coroutine7.Run();
		}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine3.IsRunning;
		})), () => {
			coroutine7.Stop();
			coroutine4.Stop();
			coroutine8.Stop();
		});
		coroutine4.Setup(this, MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.Wait(() => Random.Range(1, 3)), MaxyGames.Runtime.Routine.New(coroutine8)));
		coroutine5.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				animator.Play("defaultState", 0);
				return coroutine2.Run();
		}));
		coroutine6.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				_ActivateTransition("ToShine142");
		}));
		coroutine7.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				animator.Play("shining", 0);
				return coroutine4.Run();
		}));
		coroutine8.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				_ActivateTransition("ToDefault151");
		}));
		animator = base.GetComponent<UnityEngine.Animator>();
		_player = GameObject.FindGameObjectWithTag("Player");
		mat = base.GetComponent<UnityEngine.Renderer>().material;
	}
	
	private void Update() {
		KeyPicked();
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
		mat.SetFloat("_ShineLocation", Mathf.Lerp(1F, 0F, Random.Range(1F, 3F)));
	}
	
	public System.Collections.IEnumerator ShineFX() {
		yield return null;
	}
	
	void _ActivateTransition(string name) {
		switch(name) {
			case "ToShine142": {
				if(coroutine1.IsRunning) {
					coroutine1.Stop(true);
					coroutine3.Run();
				}
			}
			break;
			case "ToDefault151": {
				if(coroutine3.IsRunning) {
					coroutine3.Stop(true);
					coroutine1.Run();
				}
			}
			break;
		}
	}
}

