using UnityEngine;
using System.Collections;

public class Reposition : MonoBehaviour {

	Vector3 objRectTransformPos;

	// Use this for initialization
	void Start () 
	{
		objRectTransformPos = this.gameObject.GetComponent<RectTransform> ().position;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		Debug.Log (objRectTransformPos);

		if (objRectTransformPos.x <= -540f) 
		{
			objRectTransformPos = new Vector3 (1080f, objRectTransformPos.y, objRectTransformPos.z);
		}
	
	}
}
