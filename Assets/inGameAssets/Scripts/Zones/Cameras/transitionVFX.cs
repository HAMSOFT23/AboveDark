#pragma warning disable
using UnityEngine;

public class RoomTransitionVFX : MonoBehaviour {	
	private SpriteRenderer vigniette;
	private float duration = 2F;
	private bool isFadingIn = true;
	private Animator anim_;
	[SerializeField]
	[Header("Timers")]
	[Range(0F, 2F)]
	public float entranceTime;
	[SerializeField]
	[Range(0F, 2F)]
	public float exitTime;
	
	private void Start() {
		MaxyGames.UNode.GraphDebug.Flow(this, 2047758190, 11, "exit");
		MaxyGames.UNode.GraphDebug.Value(anim_ = MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(base.GetComponent<UnityEngine.Animator>(), this, 2047758190, 116, "input", false) as Animator), this, 2047758190, 114, "value", false), this, 2047758190, 114, "target", true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, 2047758190, 114, true);
	}
	
	public void EntranceVFX() {
		MaxyGames.UNode.GraphDebug.Flow(this, 2047758190, 41, "exit");
		anim_.Play("Transition vigniette");
		MaxyGames.UNode.GraphDebug.FlowNode(this, 2047758190, 100, true);
	}
	
	public void ExitVFX() {
		MaxyGames.UNode.GraphDebug.Flow(this, 2047758190, 105, "exit");
		anim_.Play("Transition vigniette 2");
		MaxyGames.UNode.GraphDebug.FlowNode(this, 2047758190, 107, true);
	}
}

