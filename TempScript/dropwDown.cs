#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class NaughtyComponent : MonoBehaviour {	
	[Dropdown("intValues")]
	public int intValue;
	[Dropdown("StringValues")]
	public string stringValue;
	[Dropdown("GetVectorValues")]
	public Vector3 vectorValue;
	private int[] intValues = new int[] { 1, 2, 3, 4, 5 };
	
	private List<string> StringValues {
		get {
			return new List<string>() { "A",
			"B",
			"C",
			"D",
			"E",} ;
		}
	}
	
	private DropdownList<Vector3> GetVectorValues() {
		return new DropdownList<Vector3>();
	}
}

