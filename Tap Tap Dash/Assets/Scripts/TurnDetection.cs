using UnityEngine;
using System.Collections;

public class TurnDetection : MonoBehaviour {
	private float maxDistance = 30f;
	bool toLeft = false;
	bool toRight = false;
	bool toJump = false;


	// Use this for initialization
	void Start () 
	{
		RayShoot ();
	}
	
	void RayShoot ()
	{		
		RaycastHit hit;
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		Debug.DrawRay (transform.position, forward* 20f, Color.cyan);
		if (Physics.Raycast (GetComponent<Rigidbody> ().transform.position, forward, out hit, maxDistance, 1)) 
		{
		GameObject hitObject = hit.transform.gameObject;
			string name1 = hitObject.name;
			Debug.Log (name1);
		TurnLeft turnLeft = hitObject.GetComponent<TurnLeft> ();
		TurnRight turnRight = hitObject.GetComponent<TurnRight> ();
			Jump jump = hitObject.GetComponent<Jump> ();

			if (turnLeft != null) 
			{
				toLeft = true;
			} else if (turnRight != null)
			{
				toRight = true;
			} else if (jump != null)
				toJump = true;
		}
	}

	void Update()
	{
		OnClickTurn ();
	}

	void OnClickTurn ()
	{
		if (Input.GetMouseButtonDown (0)) 
		{

			if (toLeft)
			{
				gameObject.transform.Rotate (0f, -90f, 0f);
				toLeft = false;
				RayShoot ();

			}
			else if (toRight)
			{
				gameObject.transform.Rotate (0f, 90f, 0f);
				toRight = false;
				RayShoot ();
			} 
			else if (toJump)
			{
				
				gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up*170f);
				toJump = false;
				RayShoot ();
			}
		}
	}
}