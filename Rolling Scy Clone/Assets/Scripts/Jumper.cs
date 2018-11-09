using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

	public AudioSource jumpAudio;

	// Use this for initialization
	void Start () {
		jumpAudio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<Rigidbody> ().useGravity = false;
			other.gameObject.GetComponent<Mover> ().startZPos = other.gameObject.transform.position.z;
			other.gameObject.GetComponent<Mover> ().jump = true;
			jumpAudio.Play ();
		} 
	}
}
