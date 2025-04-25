#pragma warning disable
using UnityEngine;

public class CrossRaycast : MonoBehaviour {	
	[SerializeField]
	[Range(0F, 1F)]
	[Header("Raycast Distance")]
	[Tooltip("The optimal distance is 0.2 or less")]
	private float raycastDistance = 1F;
	[Header("Layer Mask")]
	[Tooltip("The Layer Mask to detect")]
	[SerializeField]
	private LayerMask raycastLayerMask = ((LayerMask)64);
	public GameObject player;
	
	private void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	private void Update() {
		//Start position of raycasts
		var origin = this.transform.position;
		//Cast rays in four directions: up, down, left, right
		var hitUp = Physics2D.Raycast(origin, Vector2.up, raycastDistance, raycastLayerMask);
		var hitDown = Physics2D.Raycast(origin, Vector2.down, raycastDistance, raycastLayerMask);
		var hitLeft = Physics2D.Raycast(origin, Vector2.left, raycastDistance, raycastLayerMask);
		var hitRight = Physics2D.Raycast(origin, Vector2.right, raycastDistance, raycastLayerMask);
		//Debugging rays: draw rays in the Scene view
		Debug.DrawRay(origin, (Vector2.up * raycastDistance), Color.green);
		Debug.DrawRay(origin, (Vector2.down * raycastDistance), Color.green);
		Debug.DrawRay(origin, (Vector2.left * raycastDistance), Color.green);
		Debug.DrawRay(origin, (Vector2.right * raycastDistance), Color.green);
		//Check if rays hit something
		if((hitUp.collider != null)) {
			Debug.Log(("Hit Up: " + hitUp.collider.name));
			Effector();
		}
		if((hitDown.collider != null)) {
			Debug.Log(("Hit Down: " + hitDown.collider.name));
			Effector();
		}
		if((hitLeft.collider != null)) {
			Debug.Log(("Hit Left: " + hitLeft.collider.name));
			Effector();
		}
		if((hitRight.collider != null)) {
			Debug.Log(("Hit Right: " + hitRight.collider.name));
			Effector();
		}
	}
	
	public void Effector() {
		player.GetComponent<Rigidbody2D>().freezeRotation = (player.GetComponent<Rigidbody2D>().freezeRotation == false);
	}
}

