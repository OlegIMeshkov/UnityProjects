using UnityEngine;
using System.Collections;

public class EndEffector : MonoBehaviour {
	private GameObject newLevel;
	public Transform instPos;


	void OnTriggerExit ()
	{
		GameController.instance.CreateNextLevel (instPos);
	}

	void OnTriggerEnter ()
	{
		GameController.instance.newStartPos = transform.position;
		GameController.instance.SavePlayerProgress ();
		GameController.instance.currentLevel++;
		GameController.instance.changeColor = true;
	}

}
