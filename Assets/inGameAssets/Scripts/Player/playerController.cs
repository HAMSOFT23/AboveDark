#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class playerController : MonoBehaviour {	
	[SerializeField]
	[Header("Inputs")]
	private InputActionReference xMovement;
	[SerializeField]
	private InputActionReference yJump;
	private AudioSource _AS;
	private Rigidbody2D _rb;
	private Animator _animator;
	private BoxCollider2D playerBox;
	[SerializeField]
	[Header("Input Forces ")]
	[Tooltip("The Velocity of the Player in the X axis")]
	[Range(3F, 10F)]
	private float movementForce;
	[SerializeField]
	[Tooltip("The Jump Force of the Player in the Y axis")]
	[Range(4F, 8F)]
	private float jumpForce;
	[SerializeField]
	[Header("Checks")]
	[Tooltip("Check if the Player is Gorunded")]
	public bool _isGrounded;
	[SerializeField]
	[Tooltip("The initial position of the player before his jump is true (Sort of)")]
	private bool _initialAirTime;
	private float positionY;
	[SerializeField]
	[Tooltip("Don't know")]
	private float _airTime;
	[SerializeField]
	[Tooltip("The time the landing animation will last")]
	[Range(0F, 1.5F)]
	private float landingWaitTime;
	public bool _keyPicked;
	[SerializeField]
	[Header("Boxcast")]
	private float yOffset = 0.6F;
	[SerializeField]
	private float boxCastMaxCheckDistance;
	[SerializeField]
	private LayerMask whatIsGround;
	private Vector2 rayCastHitPoint;
	public float currentDistanceFromGround;
	[SerializeField]
	private float landDistance = 1F;
	[SerializeField]
	[Header("SFX")]
	private List<AudioClip> metalFootstepsSFX = new List<AudioClip>() { null, null, null, null, null, null, null };
	public AudioClip landingSFX;
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
	private MaxyGames.Runtime.EventCoroutine coroutine16 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine17 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine18 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine19 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine20 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine21 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine22 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine23 = new MaxyGames.Runtime.EventCoroutine();
	private MaxyGames.Runtime.EventCoroutine coroutine24 = new MaxyGames.Runtime.EventCoroutine();
	
	public Rigidbody2D RB {
		get {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 186, "exit");
			return MaxyGames.UNode.GraphDebug.Value(_rb, this, 866747430, 185, "value", false);
		}
	}
	public Animator ANIM {
		get {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 198, "exit");
			return MaxyGames.UNode.GraphDebug.Value(_animator, this, 866747430, 197, "value", false);
		}
	}
	public bool GROUNDED {
		get {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 232, "exit");
			return MaxyGames.UNode.GraphDebug.Value(_isGrounded, this, 866747430, 231, "value", false);
		}
	}
	
	private void Start() {
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 177, "flow:0");
		coroutine1.Run();
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 8, "exit");
		MaxyGames.UNode.GraphDebug.Value(_rb = MaxyGames.UNode.GraphDebug.Value(base.GetComponent<UnityEngine.Rigidbody2D>(), this, 866747430, 29, "value", false), this, 866747430, 29, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 29, "exit");
		MaxyGames.UNode.GraphDebug.Value(_animator = MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(base.GetComponent<UnityEngine.Animator>(), this, 866747430, 194, "input", false) as Animator), this, 866747430, 192, "value", false), this, 866747430, 192, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 192, "exit");
		MaxyGames.UNode.GraphDebug.Value(playerBox = MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(base.GetComponent<UnityEngine.BoxCollider2D>(), this, 866747430, 265, "input", false) as BoxCollider2D), this, 866747430, 263, "value", false), this, 866747430, 263, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 263, "exit");
		MaxyGames.UNode.GraphDebug.Value(_AS = MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(base.GetComponent<UnityEngine.AudioSource>(), this, 866747430, 337, "input", false) as AudioSource), this, 866747430, 335, "value", false), this, 866747430, 335, "target", true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 335, true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 263, true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 192, true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 29, true);
	}
	
	void FixedUpdate() {
		if(coroutine1.IsRunning) {
			coroutine10.Run();
		}
		if(coroutine2.IsRunning) {
			coroutine13.Run();
		}
	}
	
	private void Update() {
		if(coroutine3.IsRunning) {
			coroutine16.Run();
		}
		if(coroutine4.IsRunning) {
			coroutine19.Run();
		}
		if(coroutine5.IsRunning) {
			coroutine24.Run();
		}
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 237, "exit");
		GroundedRaycast ();
		MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 245, true);
	}
	
	public void Move() {
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 18, "exit");
		MaxyGames.UNode.GraphDebug.Value(_rb.velocity = MaxyGames.UNode.GraphDebug.Value(new Vector2((xMovement.action.ReadValue<UnityEngine.Vector2>().x * movementForce), _rb.velocity.y), this, 866747430, 26, "value", false), this, 866747430, 26, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 26, "exit");
		if(MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(_rb.velocity.x, this, 866747430, 96, "inputA", false) > 0F), this, 866747430, 92, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 92, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(this.transform.localScale = new Vector3(-1f, 1f, 1f), this, 866747430, 101, "target", true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 101, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 92, true);
		} else {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 92, "onFalse");
			if(MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(_rb.velocity.x, this, 866747430, 104, "inputA", false) < 0F), this, 866747430, 106, "condition", false)) {
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 106, "onTrue");
				MaxyGames.UNode.GraphDebug.Value(this.transform.localScale = Vector3.one, this, 866747430, 108, "target", true);
				MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 108, true);
				MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 106, true);
			} else {
				MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 106, false);
			}
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 92, false);
		}
		MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 26, true);
	}
	
	public void Jump() {
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 21, "exit");
		if(MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(yJump.action.triggered, this, 866747430, 60, "de835dd666d55f2", false) && MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(_isGrounded, this, 866747430, 62, "inputA", false) == true), this, 866747430, 60, "1b707c38241446dc", false)), this, 866747430, 39, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 39, "onTrue");
			_rb.AddForce(MaxyGames.UNode.GraphDebug.Value(new Vector2(0F, jumpForce), this, 866747430, 41, "-0-0", false), ForceMode2D.Impulse);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 41, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 39, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 39, false);
		}
	}
	
	public void GroundedRaycast () {
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 243, "exit");
		MaxyGames.UNode.GraphDebug.Value(_isGrounded = MaxyGames.UNode.GraphDebug.Value(Physics2D.BoxCast(MaxyGames.UNode.GraphDebug.Value(new Vector2(this.transform.position.x, (this.transform.position.y - yOffset)), this, 866747430, 246, "-0-0", false), MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(playerBox, this, 866747430, 267, "-instance", false).size, this, 866747430, 246, "-0-1", false), 0F, MaxyGames.UNode.GraphDebug.Value(-MaxyGames.UNode.GraphDebug.Value(Vector2.up, this, 866747430, 269, "target", false), this, 866747430, 246, "-0-3", false), MaxyGames.UNode.GraphDebug.Value(boxCastMaxCheckDistance, this, 866747430, 246, "-0-4", false), MaxyGames.UNode.GraphDebug.Value(whatIsGround, this, 866747430, 246, "-0-5", false)), this, 866747430, 276, "value", false), this, 866747430, 276, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 276, "exit");
		MaxyGames.UNode.GraphDebug.Value(currentDistanceFromGround = MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(Physics2D.BoxCast(MaxyGames.UNode.GraphDebug.Value(new Vector2(this.transform.position.x, (this.transform.position.y - yOffset)), this, 866747430, 246, "-0-0", false), MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(playerBox, this, 866747430, 267, "-instance", false).size, this, 866747430, 246, "-0-1", false), 0F, MaxyGames.UNode.GraphDebug.Value(-MaxyGames.UNode.GraphDebug.Value(Vector2.up, this, 866747430, 269, "target", false), this, 866747430, 246, "-0-3", false), MaxyGames.UNode.GraphDebug.Value(boxCastMaxCheckDistance, this, 866747430, 246, "-0-4", false), MaxyGames.UNode.GraphDebug.Value(whatIsGround, this, 866747430, 246, "-0-5", false)), this, 866747430, 441, "-instance", false).distance, this, 866747430, 440, "value", false), this, 866747430, 440, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 440, "exit");
		MaxyGames.UNode.GraphDebug.Value(rayCastHitPoint = MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(Physics2D.BoxCast(MaxyGames.UNode.GraphDebug.Value(new Vector2(this.transform.position.x, (this.transform.position.y - yOffset)), this, 866747430, 246, "-0-0", false), MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(playerBox, this, 866747430, 267, "-instance", false).size, this, 866747430, 246, "-0-1", false), 0F, MaxyGames.UNode.GraphDebug.Value(-MaxyGames.UNode.GraphDebug.Value(Vector2.up, this, 866747430, 269, "target", false), this, 866747430, 246, "-0-3", false), MaxyGames.UNode.GraphDebug.Value(boxCastMaxCheckDistance, this, 866747430, 246, "-0-4", false), MaxyGames.UNode.GraphDebug.Value(whatIsGround, this, 866747430, 246, "-0-5", false)), this, 866747430, 307, "-instance", false).point, this, 866747430, 309, "value", false), this, 866747430, 309, "target", true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 309, true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 440, true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 276, true);
	}
	
	private void OnDrawGizmos() {
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 278, "exit");
		if(MaxyGames.UNode.GraphDebug.Value(Application.isPlaying, this, 866747430, 313, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 313, "onTrue");
			if(MaxyGames.UNode.GraphDebug.Value(_isGrounded, this, 866747430, 445, "condition", false)) {
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 445, "onTrue");
				MaxyGames.UNode.GraphDebug.Value(Gizmos.color = new Color() { r = 1F, g = 0.9175284F, a = 1F }, this, 866747430, 281, "target", true);
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 281, "exit");
				Gizmos.DrawRay(MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(new Vector3(this.transform.position.x, (this.transform.position.y - yOffset)), this, 866747430, 297, "517689f31e8d3035", false) - (Vector3)MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(Vector2.up, this, 866747430, 300, "5ac8a11f6cfeffd9", false) * MaxyGames.UNode.GraphDebug.Value(boxCastMaxCheckDistance, this, 866747430, 300, "2e7dd1a1b753d46", false)), this, 866747430, 297, "67713fded496bec", false)), this, 866747430, 282, "-0-0", false), MaxyGames.UNode.GraphDebug.Value(-MaxyGames.UNode.GraphDebug.Value(Vector2.up, this, 866747430, 305, "target", false), this, 866747430, 282, "-0-1", false));
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 282, "exit");
				Gizmos.DrawCube(MaxyGames.UNode.GraphDebug.Value(rayCastHitPoint, this, 866747430, 306, "-0-0", false), MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(playerBox, this, 866747430, 284, "-instance", false).size, this, 866747430, 306, "-0-1", false));
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 306, "exit");
				if(MaxyGames.UNode.GraphDebug.Value(_isGrounded, this, 866747430, 443, "condition", false)) {
					MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 443, true);
				} else {
					MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 443, false);
				}
				MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 306, true);
				MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 282, true);
				MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 281, true);
				MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 445, true);
			} else {
				MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 445, false);
			}
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 313, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 313, false);
		}
	}
	
	public void Footsteps() {
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 328, "exit");
		if(_isGrounded) {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 338, "onTrue");
			_AS.PlayOneShot(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(metalFootstepsSFX, this, 866747430, 355, "-instance", false)[MaxyGames.UNode.GraphDebug.Value(Random.Range(0, 7), this, 866747430, 355, "-0-0", false)], this, 866747430, 330, "-0-0", false), 0.6F);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 330, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 338, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 338, false);
		}
	}
	
	public void landing() {
		MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 529, "exit");
		if(MaxyGames.UNode.GraphDebug.Value(_isGrounded, this, 866747430, 532, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 532, "onTrue");
			_AS.PlayOneShot(MaxyGames.UNode.GraphDebug.Value(landingSFX, this, 866747430, 531, "-0-0", false), 0.8F);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 531, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 532, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 532, false);
		}
	}
	
	System.Collections.IEnumerable _ExecuteCoroutineEvent(int uid) {
		switch(uid) {
			case 0: {
				Move();
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 226, "exit");
				Jump();
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 227, "exit");
				if(MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(_rb, this, 866747430, 211, "-instance", false).velocity.x, this, 866747430, 212, "inputA", false) != 0F), this, 866747430, 209, "condition", false)) {
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 209, "onTrue");
					_ActivateTransition("Will Walk178");
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 209, "exit");
					if(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(yJump, this, 866747430, 465, "-instance", false).action.triggered, this, 866747430, 467, "condition", false)) {
						MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 467, "onTrue");
						_ActivateTransition("Will Jump178");
						MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 467, true);
						yield return true;
					} else {
						MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 467, false);
						yield return false;
					}
					MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 209, true);
					yield return true;
				} else {
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 209, "exit");
					if(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(yJump, this, 866747430, 465, "-instance", false).action.triggered, this, 866747430, 467, "condition", false)) {
						MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 467, "onTrue");
						_ActivateTransition("Will Jump178");
						MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 467, true);
						yield return true;
					} else {
						MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 467, false);
						yield return false;
					}
					MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 209, false);
					yield return false;
				}
				MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 227, true);
			}
			break;
			case 1: {
				Move();
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 175, "exit");
				Jump();
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 228, "exit");
				if(MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(_rb, this, 866747430, 219, "-instance", false).velocity.x, this, 866747430, 217, "inputA", false) == 0F), this, 866747430, 216, "condition", false)) {
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 216, "onTrue");
					_ActivateTransition("ToIdle173");
					MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 216, true);
					yield return true;
				} else {
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 216, "onFalse");
					if(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(yJump, this, 866747430, 470, "-instance", false).action.triggered, this, 866747430, 471, "condition", false)) {
						MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 471, "onTrue");
						_ActivateTransition("Will Jump173");
						MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 471, true);
						yield return true;
					} else {
						MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 471, false);
						yield return false;
					}
					MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 216, false);
					yield return false;
				}
				MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 228, true);
			}
			break;
			case 2: {
				MaxyGames.UNode.GraphDebug.Value(_initialAirTime = MaxyGames.UNode.GraphDebug.Value(Physics2D.BoxCast(MaxyGames.UNode.GraphDebug.Value(new Vector2(this.transform.position.x, (this.transform.position.y - yOffset)), this, 866747430, 485, "-0-0", false), MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(playerBox, this, 866747430, 498, "-instance", false).size, this, 866747430, 485, "-0-1", false), 0F, MaxyGames.UNode.GraphDebug.Value(-MaxyGames.UNode.GraphDebug.Value(Vector2.up, this, 866747430, 500, "target", false), this, 866747430, 485, "-0-3", false), _airTime, MaxyGames.UNode.GraphDebug.Value(whatIsGround, this, 866747430, 485, "-0-5", false)), this, 866747430, 503, "value", false), this, 866747430, 503, "target", true);
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 503, "exit");
				MaxyGames.UNode.GraphDebug.Value(currentDistanceFromGround = MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(Physics2D.BoxCast(MaxyGames.UNode.GraphDebug.Value(new Vector2(this.transform.position.x, (this.transform.position.y - yOffset)), this, 866747430, 485, "-0-0", false), MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(playerBox, this, 866747430, 498, "-instance", false).size, this, 866747430, 485, "-0-1", false), 0F, MaxyGames.UNode.GraphDebug.Value(-MaxyGames.UNode.GraphDebug.Value(Vector2.up, this, 866747430, 500, "target", false), this, 866747430, 485, "-0-3", false), _airTime, MaxyGames.UNode.GraphDebug.Value(whatIsGround, this, 866747430, 485, "-0-5", false)), this, 866747430, 507, "-instance", false).distance, this, 866747430, 506, "value", false), this, 866747430, 506, "target", true);
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 506, "exit");
				if(_initialAirTime) {
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 508, "onTrue");
					_ActivateTransition("ToLanding403");
					MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 508, true);
					yield return true;
				} else {
					MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 508, false);
					yield return false;
				}
				MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 506, true);
			}
			break;
		}
		yield break;
	}
	
	void _ActivateTransition(string name) {
		switch(name) {
			case "Will Walk178": {
				if(coroutine1.IsRunning) {
					coroutine1.Stop(true);
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 214, "exit");
					coroutine2.Run();
				}
			}
			break;
			case "Will Jump178": {
				if(coroutine1.IsRunning) {
					coroutine1.Stop(true);
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 394, "exit");
					coroutine9.Run();
				}
			}
			break;
			case "ToIdle173": {
				if(coroutine2.IsRunning) {
					coroutine2.Stop(true);
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 221, "exit");
					coroutine1.Run();
				}
			}
			break;
			case "Will Jump173": {
				if(coroutine2.IsRunning) {
					coroutine2.Stop(true);
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 393, "exit");
					coroutine9.Run();
				}
			}
			break;
			case "ToFall366": {
				if(coroutine3.IsRunning) {
					coroutine3.Stop(true);
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 405, "exit");
					coroutine4.Run();
				}
			}
			break;
			case "ToLanding403": {
				if(coroutine4.IsRunning) {
					coroutine4.Stop(true);
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 422, "exit");
					coroutine5.Run();
				}
			}
			break;
			case "ToIdle423": {
				if(coroutine5.IsRunning) {
					coroutine5.Stop(true);
					MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 434, "exit");
					coroutine1.Run();
				}
			}
			break;
		}
	}
	
	void Awake() {
		coroutine1.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 205, "flow:0");
			coroutine7.Run();
		}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine1.IsRunning;
		})), () => {
			coroutine7.Stop();
			coroutine8.Stop();
		});
		coroutine1.Debug(866747430, 178, null);
		coroutine2.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 176, "flow:0");
			coroutine12.Run();
		}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine2.IsRunning;
		})), () => {
			coroutine11.Stop();
			coroutine12.Stop();
		});
		coroutine2.Debug(866747430, 173, null);
		coroutine3.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 372, "flow:0");
			coroutine14.Run();
		}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine3.IsRunning;
		})), () => {
			coroutine14.Stop();
			coroutine15.Stop();
		});
		coroutine3.Debug(866747430, 366, null);
		coroutine4.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 411, "flow:0");
			coroutine17.Run();
		}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine4.IsRunning;
		})), () => {
			coroutine17.Stop();
			coroutine18.Stop();
		});
		coroutine4.Debug(866747430, 403, null);
		coroutine5.Setup(this, 
		MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 425, "flow:0");
			coroutine20.Run();
		}), MaxyGames.Runtime.Routine.WaitWhile(() => {
			return coroutine5.IsRunning;
		})), () => {
			coroutine20.Stop();
			coroutine6.Stop();
			coroutine21.Stop();
			coroutine22.Stop();
			coroutine23.Stop();
		});
		coroutine5.Debug(866747430, 423, null);
		coroutine6.Setup(this, MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.Routine.Wait(() => MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(this, this, 866747430, 521, "input", false).GetComponent<Animator>(), this, 866747430, 519, "-instance", false).GetCurrentAnimatorStateInfo(0), this, 866747430, 516, "-instance", false).length, this, 866747430, 432, "waitTime", false)), MaxyGames.Runtime.Routine.New(MaxyGames.Runtime.EventCoroutine.Create(this, MaxyGames.Runtime.Routine.Event(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 432, "exit");
			return coroutine21;
		})))));
		coroutine6.Debug(866747430, 432, null);
		coroutine7.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(this, this, 866747430, 182, "input", false).GetComponent<Animator>(), this, 866747430, 180, "-instance", false).Play("Player_idle");
		}));
		coroutine7.Debug(866747430, 180, null);
		coroutine8.Setup(this, _ExecuteCoroutineEvent(0));
		coroutine8.Debug(866747430, 226, null);
		coroutine9.Setup(this, new MaxyGames.Runtime.Conditional(() => _isGrounded, onTrue: MaxyGames.Runtime.EventCoroutine.Create(this, MaxyGames.Runtime.Routine.Event(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 463, "onTrue");
			return coroutine3;
		})), onFalse: MaxyGames.Runtime.EventCoroutine.Create(this, MaxyGames.Runtime.Routine.Event(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 463, "onFalse");
			return coroutine4;
		}))));
		coroutine9.Debug(866747430, 463, null);
		coroutine10.Setup(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 207, "flow:0");
			coroutine8.Run();
		}));
		coroutine11.Setup(this, _ExecuteCoroutineEvent(1));
		coroutine11.Debug(866747430, 175, null);
		coroutine12.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				MaxyGames.UNode.GraphDebug.Value(_animator, this, 866747430, 224, "-instance", false).Play("Player_walking");
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 224, "exit");
				Footsteps();
				MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 333, true);
		}));
		coroutine12.Debug(866747430, 224, null);
		coroutine13.Setup(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 215, "flow:0");
			coroutine11.Run();
		}));
		coroutine14.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(this, this, 866747430, 377, "input", false).GetComponent<Animator>(), this, 866747430, 375, "-instance", false).Play("Player_JumpStart");
		}));
		coroutine14.Debug(866747430, 375, null);
		coroutine15.Setup(this, new MaxyGames.Runtime.Conditional(() => _isGrounded, onTrue: MaxyGames.Runtime.EventCoroutine.Create(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 378, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(positionY = MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(_rb, this, 866747430, 408, "input", false).transform, this, 866747430, 406, "-instance", false).position.y, this, 866747430, 410, "value", false), this, 866747430, 410, "target", true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 410, true);
		})), onFalse: MaxyGames.Runtime.EventCoroutine.Create(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 378, "onFalse");
			_ActivateTransition("ToFall366");
		}))));
		coroutine15.Debug(866747430, 378, null);
		coroutine16.Setup(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 392, "flow:0");
			coroutine15.Run();
		}));
		coroutine17.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				_animator.Play("Player_JumpFalling");
		}));
		coroutine17.Debug(866747430, 412, null);
		coroutine18.Setup(this, _ExecuteCoroutineEvent(2));
		coroutine18.Debug(866747430, 503, null);
		coroutine19.Setup(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 482, "flow:0");
			coroutine18.Run();
		}));
		coroutine20.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				_animator.Play("Player_JumpLanding");
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 426, "exit");
				return coroutine23.Run();
		}));
		coroutine20.Debug(866747430, 426, null);
		coroutine21.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				_ActivateTransition("ToIdle423");
		}));
		coroutine21.Debug(866747430, 433, null);
		coroutine22.Setup(this, new MaxyGames.Runtime.Conditional(() => _isGrounded, onTrue: MaxyGames.Runtime.EventCoroutine.Create(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 523, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(_rb.velocity = Vector2.zero, this, 866747430, 527, "target", true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 866747430, 527, true);
		}))));
		coroutine22.Debug(866747430, 523, null);
		coroutine23.Setup(this, MaxyGames.Runtime.Routine.New(() => {
				landing();
				MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 536, "exit");
				return coroutine6.Run();
		}));
		coroutine23.Debug(866747430, 536, null);
		coroutine24.Setup(this, MaxyGames.Runtime.Routine.New(() => {
			MaxyGames.UNode.GraphDebug.Flow(this, 866747430, 522, "flow:0");
			coroutine22.Run();
		}));
	}
}

