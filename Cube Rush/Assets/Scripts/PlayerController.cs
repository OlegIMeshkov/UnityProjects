using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	Rigidbody rb;
	public float speed = 50f;
	public float rotationSpeed = 100f;



	// Use this for initialization
	void Start () {
	
		rb = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		PlayerMovement ();
	}
		
	void PlayerMovement()
	{
		if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f))

		{
			transform.position += transform.right * Input.GetAxis ("Horizontal") * speed * Time.deltaTime +transform.forward * Input.GetAxis ("Vertical") * speed * Time.deltaTime;
		}

		if (Input.GetButton("RotateLeft"))
		{
			rb.transform.eulerAngles += new Vector3 (0f,
				rotationSpeed * Time.deltaTime, 0f);

		}
		else if (Input.GetButton("RotateRight")) {
			rb.transform.eulerAngles += new Vector3 (0f,
				-rotationSpeed * Time.deltaTime, 0f);
		}

	}
		
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag("TimeCollectible")) {

			StartCoroutine (GameController.instance.SpawnTimeCollectible ());
			Destroy (other.gameObject);

			GameController.instance.UpdateTimeUI ();


		} else if (other.gameObject.CompareTag("PointCollectible")) {
			StartCoroutine (GameController.instance.SpawnPointCollectible ());

			Destroy (other.gameObject);
			GameController.instance.UpdatePointUI ();
		}
	}


}

