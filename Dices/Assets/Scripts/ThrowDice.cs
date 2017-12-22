using UnityEngine;
using System.Collections;

public class ThrowDice : MonoBehaviour {

	Rigidbody rb;
	public bool isActive = false;
	public bool isThrown = false;
	public bool isStanding = false;
	public bool diceCounted =false;
	public bool buttonPressed = false;

	float throwTime;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (isActive) 
		{
			Throw ();
			throwTime = Time.time;
			buttonPressed = !buttonPressed;
		}

		if (!diceCounted && isThrown && rb.angularVelocity == Vector3.zero && (Time.time - throwTime) > 0.1f ) {
			isStanding = true;
			CheckOrientation ();
			GameController.instance.isCounted++;
			diceCounted = true;

		} else
			isStanding = false;
	
	}


	void Throw()
	{
		Vector3 force = new Vector3 (Random.Range (0f, 300f), Random.Range (0f, 300f), Random.Range (0f, 300f));
		rb.useGravity = true;
		rb.isKinematic = false;
		rb.AddTorque (force);
		isThrown = true;
		isActive = false;
	}

	void CheckOrientation ()
	{
		if (Mathf.RoundToInt (gameObject.transform.up.y) == 1) {
			GameController.instance.counter += 1;
		} else if (Mathf.RoundToInt (gameObject.transform.forward.y) == -1) {
			GameController.instance.counter += 2;
		} else if (Mathf.RoundToInt (gameObject.transform.right.y) == 1) {
			GameController.instance.counter += 3;
		} else if (Mathf.RoundToInt (gameObject.transform.right.y) == -1) {
			GameController.instance.counter += 4;
		} else if (Mathf.RoundToInt (gameObject.transform.forward.y) == 1) {
			GameController.instance.counter += 5;
		} else if (Mathf.RoundToInt (gameObject.transform.up.y) == -1) {
			GameController.instance.counter += 6;
		} 
	}


	public void ChangeState()
	{
		buttonPressed = true;
	}

}
