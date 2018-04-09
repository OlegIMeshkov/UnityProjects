﻿using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {


	public float speed;
	float zPos;
	float xPos;
	float yPos;
	public float startZPos;
	public float amplitude;
	public bool jump = false;

	// Use this for initialization
	void Start () {
		zPos = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		zPos += speed * Time.fixedDeltaTime;
		xPos += Input.GetAxis ("Horizontal") * speed * Time.fixedDeltaTime;






		if (jump) {
			yPos = 0.36f + Mathf.Sin ((transform.position.z - startZPos) / 2f * Mathf.PI / 2f) * amplitude;

		} else {
			yPos = transform.position.y;
			gameObject.GetComponent<Rigidbody> ().useGravity = true;
		}
		transform.position = new Vector3 (xPos, yPos, zPos);
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.CompareTag ("GrowingObstacle")) {
			
			Destroy (gameObject);

		} else if (other.gameObject.CompareTag ("Floor")) {

			jump = false;
		}
	}
}