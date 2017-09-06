using UnityEngine;
using System.Collections;

public class RightImagePositioningScript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GetComponent<RectTransform> ().position = new Vector3 (Screen.width*1.5f, GetComponent<RectTransform> ().position.y, GetComponent<RectTransform> ().position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
