using UnityEngine;
using System.Collections;

public class LeftImagePositioningScript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GetComponent<RectTransform> ().position = new Vector3 (-Screen.width*0.5f, GetComponent<RectTransform> ().position.y, GetComponent<RectTransform> ().position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
