using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject player;
	private Vector3 offset;
	private float fixedPosY;
	// Use this for initialization
	void Start ()
	{
		player = FindObjectOfType<TurnDetection> ().gameObject;
		offset = transform.position - player.transform.position;
		fixedPosY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + offset;
		transform.position = new Vector3 (transform.position.x, fixedPosY, transform.position.z);
	}
}
