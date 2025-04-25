#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {	
	public float speed = 10F;
	public float lifeTime = 2F;
	private Vector2 targetDirection;
	
	private void Start() {
		//Destroy the bullet after a certain amount of time to avoid memory leaks
		Object.Destroy(this.gameObject, lifeTime);
	}
	
	public void SetDirection(Vector2 direction) {
		targetDirection = direction.normalized;
	}
	
	private void Update() {
		//Move the bullet in the set direction
		this.transform.Translate(((targetDirection * speed) * Time.deltaTime), Space.World);
	}
	
	private void OnCollisionEnter2D(Collision2D collision) {
		//Optionally, destroy the bullet upon collision
		Object.Destroy(this.gameObject);
	}
}

