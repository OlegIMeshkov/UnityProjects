using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	[Range(0.5f,5f)]
	public float scrollSpeed;

	public const float height = 32f;

	// Use this for initialization
	void Start () {
		this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, height, this.gameObject.transform.position.z);
		Camera.main.orthographic = true;

	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate (new Vector3(0f, 0f, Input.GetAxis ("Mouse ScrollWheel"))* scrollSpeed);
		if (Input.GetMouseButton(2)) {
			float xDirection = Input.GetAxis ("Mouse X");
			float yDirection = Input.GetAxis ("Mouse Y");
			transform.Translate (new Vector3 (xDirection, yDirection, 0f) * scrollSpeed);
		} 




	}
}
