using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = player.transform.position - gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.gameObject.GetComponent<Mover>().jump) {
			gameObject.transform.position = new Vector3 (0.7f*(player.transform.position.x - offset.x),
				2f,
				(player.transform.position.z - offset.z));
			Debug.Log("++");
		} else
			gameObject.transform.position = new Vector3 (0.7f*(player.transform.position.x - offset.x),
				player.transform.position.y - offset.y,
				(player.transform.position.z - offset.z));
	}
}
