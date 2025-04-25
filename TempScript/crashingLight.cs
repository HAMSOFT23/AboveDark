#pragma warning disable
using UnityEngine;
using System.Collections.Generic;

public class crashingLight : MonoBehaviour {	
	public AudioSource audioSource;
	public float volume;
	public AudioClip lightCollisionSFX;
	public CapsuleCollider2D collider;
	
	private void Start() {
		collider = base.GetComponent<UnityEngine.CapsuleCollider2D>();
		audioSource = base.GetComponent<UnityEngine.AudioSource>();
	}
	
	private void OnCollisionEnter2D(Collision2D collisionInfo) {
		if(collisionInfo.collider.gameObject.CompareTag("Ground")) {
			audioSource.PlayOneShot(lightCollisionSFX, volume);
		}
	}
}

