#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class LightLerpIntensity : MonoBehaviour {	
	[SerializeField]
	[Header("Light Intensity")]
	[Tooltip("The Light Intensity of the game objects. Must not be modified.")]
	private float light1Intensity;
	[SerializeField]
	[Tooltip("The Light Intensity of the game objects. Must not be modified.")]
	private float light2Intensity;
	[SerializeField]
	[Header("Lerp Time ")]
	private float _NoiseSpeed;
	[SerializeField]
	[Header("Random Timer")]
	[Tooltip("The value from NoiseSpeed x Time.deltaTime")]
	private float t;
	[Header("Volume")]
	[Range(0F, 1F)]
	public float sfxVolume;
	[SerializeField]
	[Header("Light Game Objects")]
	private GameObject light1;
	[SerializeField]
	private GameObject light2;
	
	private void Awake() {
	}
	
	private void Start() {
		light1Intensity = light1.GetComponent<Light2D>().intensity;
		light2Intensity = light2.GetComponent<Light2D>().intensity;
	}
	
	private void Update() {
		t = Random.Range(0F, 30F);
		base.StartCoroutine(intensityLerp());
	}
	
	public System.Collections.IEnumerator intensityLerp() {
		yield return null;
		light1.GetComponent<Light2D>().intensity = Mathf.Lerp(0F, light1Intensity, Random.Range(0F, t));
		light2.GetComponent<Light2D>().intensity = Mathf.Lerp(0F, light2Intensity, Random.Range(0F, t));
	}
}

