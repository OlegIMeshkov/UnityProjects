using UnityEngine;
using System.Collections;

public class CubeGrow : MonoBehaviour {


	public bool toGrow = false;
	public float growSpeed = 1f;
	bool gameOver = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (toGrow && transform.position.y <0.1f) {
			transform.Translate (Vector3.up * Time.fixedDeltaTime * growSpeed);

		}
	
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag("Screen")) {
			toGrow = true;
		}

	} 


}
