#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class lightPEMController : MonoBehaviour {	
	[SerializeField]
	private InputActionReference toggleLight;
	public bool _On;
	public GameObject light;
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
	
	void Start() {
		MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 51, "flow:0");
		coroutine1.Run();
	}
	
	void Update() {
		if(coroutine1.IsRunning) {
			coroutine5.Run();
		}
		if(coroutine2.IsRunning) {
			coroutine8.Run();
		}
		if(coroutine3.IsRunning) {
			coroutine11.Run();
		}
	}
	
	public void Toggle() {
	}
	
	void _ActivateTransition(string name) {
		switch(name) {
			case "Onn52": {
				if(coroutine1.IsRunning) {
					coroutine1.Stop(true);
					MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 62, "exit");
					coroutine2.Run();
				}
			}
			break;
			case "Off63": {
				if(coroutine2.IsRunning) {
					coroutine2.Stop(true);
					MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 74, "exit");
					coroutine3.Run();
				}
			}
			break;
			case "cycle77": {
				if(coroutine3.IsRunning) {
					coroutine3.Stop(true);
					MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 89, "exit");
					coroutine2.Run();
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
			coroutine4.Stop();
		});
		coroutine1.Debug(1909372234, 52, null);
		coroutine2.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 65, "flow:0");
			coroutine6.Run();
		}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine2.IsRunning;
		})), () => {
			coroutine6.Stop();
			coroutine7.Stop();
		});
		coroutine2.Debug(1909372234, 63, null);
		coroutine3.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 87, "flow:0");
			coroutine9.Run();
		}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine3.IsRunning;
		})), () => {
			coroutine9.Stop();
			coroutine10.Stop();
		});
		coroutine3.Debug(1909372234, 77, null);
		coroutine4.Setup(this, new MaxyGames.Runtime.Conditional(() => MaxyGames.UNode.GraphDebug.Value(toggleLight.action.triggered, this, 1909372234, 59, "condition", false), onTrue: MaxyGames.Runtime.EventCoroutine.Create(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 59, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(_On = true, this, 1909372234, 60, "target", true);
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 60, "exit");
			_ActivateTransition("Onn52");
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 60, true);
		}))));
		coroutine4.Debug(1909372234, 59, null);
		coroutine5.Setup(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 56, "flow:0");
			coroutine4.Run();
		}));
		coroutine6.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				light.SetActive(true);
		}));
		coroutine6.Debug(1909372234, 66, null);
		coroutine7.Setup(this, new MaxyGames.Runtime.Conditional(() => MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(toggleLight.action.triggered, this, 1909372234, 69, "-instance", false).Equals(_On), this, 1909372234, 67, "condition", false), onTrue: MaxyGames.Runtime.EventCoroutine.Create(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 67, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(_On = false, this, 1909372234, 72, "target", true);
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 72, "exit");
			_ActivateTransition("Off63");
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 72, true);
		}))));
		coroutine7.Debug(1909372234, 67, null);
		coroutine8.Setup(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 71, "flow:0");
			coroutine7.Run();
		}));
		coroutine9.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				light.SetActive(false);
		}));
		coroutine9.Debug(1909372234, 80, null);
		coroutine10.Setup(this, new MaxyGames.Runtime.Conditional(() => MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(toggleLight.action.triggered, this, 1909372234, 83, "-instance", false).Equals(MaxyGames.UNode.GraphDebug.Value(_On.Equals(false), this, 1909372234, 83, "-0-0", false)), this, 1909372234, 81, "condition", false), onTrue: MaxyGames.Runtime.EventCoroutine.Create(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 81, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(_On = true, this, 1909372234, 85, "target", true);
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 85, "exit");
			_ActivateTransition("cycle77");
			MaxyGames.UNode.GraphDebug.FlowNode(this, 1909372234, 85, true);
		}))));
		coroutine10.Debug(1909372234, 81, null);
		coroutine11.Setup(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 1909372234, 86, "flow:0");
			coroutine10.Run();
		}));
	}
}

