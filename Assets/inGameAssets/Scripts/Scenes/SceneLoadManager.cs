#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour {	
	[SerializeField]
	[Dropdown("actualList")]
	public int SceneIndex;
	private List<int> actualList = new List<int>() { 0, 1, 2 };
	public int sceneCount;
	[Range(0F, 3F)]
	public int ceneIndex;
	
	private void OnTriggerEnter2D(Collider2D colliderInfo) {
		if(colliderInfo.gameObject.gameObject.CompareTag("Player")) {
			SceneManager.LoadSceneAsync(ceneIndex);
		}
	}
	
	private void Start() {
	}
}

