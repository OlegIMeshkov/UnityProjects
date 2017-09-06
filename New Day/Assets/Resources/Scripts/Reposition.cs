using UnityEngine;
using System.Collections;

public class Reposition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (this.gameObject.GetComponent<RectTransform> ().position);

		if (this.gameObject.GetComponent<RectTransform> ().position.x <= -540f) 
		{
			this.gameObject.GetComponent<RectTransform> ().position = new Vector3 (1080f, this.gameObject.GetComponent<RectTransform> ().position.y, this.gameObject.GetComponent<RectTransform> ().position.z);
		}
	
	}
}
