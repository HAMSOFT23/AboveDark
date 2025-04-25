#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour {	
	public GameObject bulletPrefab;
	public Transform shootingPoint;
	private Transform player;
	public float fireRate = 1.5F;
	private float nextFireTime;
	public GameObject turretHead;
	private AudioSource audis_;
	public List<AudioClip> shootSFXs = new List<AudioClip>() { null, null, null };
	
	private void Start() {
		shootingPoint = turretHead.transform.GetChild(0);
		audis_ = base.GetComponent<UnityEngine.AudioSource>();
	}
	
	private void Update() {
	}
	
	private void Shoot(Vector2 direction) {
		//Normalize the direction to ensure it has a consistent length
		direction.Normalize();
		//Calculate the correct angle for the bullet to face the player
		var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
		//Instantiate the bullet at the shooting point with the correct rotation
		var bullet = Object.Instantiate<UnityEngine.GameObject>(bulletPrefab, shootingPoint.position, Quaternion.Euler(0, 0, angle));
		//Set the bullet's direction
		var bulletScript = bullet.GetComponent<Bullet>();
		if((bulletScript != null)) {
			bulletScript.SetDirection(direction);
			Shootin_SFX();
		}
	}
	
	public void Shootin_SFX() {
		audis_.PlayOneShot(shootSFXs[Random.Range(0, 2)]);
	}
}

