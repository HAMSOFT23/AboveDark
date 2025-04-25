#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Feel;
using MoreMountains.FeedbacksForThirdParty;
using MoreMountains.Feedbacks;

public class breakableLight : MonoBehaviour {	
	private BoxCollider2D boxCo;
	private AudioSource audioSo;
	public GameObject anchor;
	public bool _playerIn;
	public GameObject sparks1;
	public GameObject sparksEx1;
	public GameObject sparksEx2;
	public List<AudioClip> sparksSFX = new List<AudioClip>() { null, null };
	public AudioClip sparksExplo;
	[Range(0F, 1F)]
	public float sFXVolume = 0.3F;
	public MMF_Player triggerEffect;
	
	private void Start() {
		boxCo = base.GetComponent<UnityEngine.BoxCollider2D>();
		audioSo = base.GetComponent<UnityEngine.AudioSource>();
	}
	
	private void Update() {
		Sparks();
	}
	
	private void OnTriggerEnter2D(Collider2D colliderInfo) {
		if(colliderInfo.gameObject.CompareTag("Player")) {
			_playerIn = true;
			audioSo.PlayOneShot(sparksExplo, 1F);
			triggerEffect.PlayFeedbacks();
			anchor.SetActive(false);
			Object.Destroy(sparksEx1);
			sparks1.SetActive(true);
		}
	}
	
	public void Sparks() {
		this.GetComponent<MMF_Player>().PlayFeedbacks();
	}
}

