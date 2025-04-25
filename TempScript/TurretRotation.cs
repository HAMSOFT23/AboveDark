#pragma warning disable
using UnityEngine;

public class TurretController : MonoBehaviour {	
	public Transform player_;
	public float rotationSpeed = 20F;
	public float minRotation = -90F;
	public float maxRotation = 90F;
	public Quaternion targetRotation;
	public GameObject turret;
	
	private void Start() {
		turret = this.transform.GetChild(0).gameObject;
	}
	
	private void Update() {
		TrackPlayer();
	}
	
	private void TrackPlayer() {
		if((player_ == null)) {
			//Apply the rotation
			turret.GetComponent<Component>().transform.localRotation = Quaternion.Slerp(turret.GetComponent<Component>().transform.localRotation, Quaternion.Euler(0F, 0F, 90F), (rotationSpeed * Time.deltaTime));
		}
		 else {
			//Get direction to the player
			var direction = (player_.position - turret.GetComponent<Component>().transform.position);
			direction.z = 0;
			//Calculate the target angle
			var targetAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
			targetRotation = Quaternion.AngleAxis(ClampAngle(-targetAngle, minRotation, maxRotation), turret.transform.forward);
			//Apply the rotation
			turret.GetComponent<Component>().transform.localRotation = Quaternion.Slerp(turret.GetComponent<Component>().transform.localRotation, targetRotation, (rotationSpeed * Time.deltaTime));
		}
	}
	
	private float ClampAngle(float angle, float min, float max) {
		//Normalize the angle to -180 to 180 range
		if((angle > 180)) {
			angle -= 360;
		}
		if((angle < -180)) {
			angle += 360;
		}
		//Clamp the angle
		return Mathf.Clamp(angle, min, max);
	}
	
	private void OnTriggerEnter2D(Collider2D colliderInfo) {
		if(colliderInfo.gameObject.CompareTag("Player")) {
			player_ = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}
	
	private void OnTriggerExit2D(Collider2D colliderInfo) {
		if(colliderInfo.gameObject.CompareTag("Player")) {
			player_ = null;
		}
	}
}

