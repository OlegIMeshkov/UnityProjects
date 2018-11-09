using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public bool gameOver;

	public Text gameOverText;


	// Use this for initialization
	void Start () {
		gameOverText.text = "";

	}
	
	public void SetGameOverText (string text)
	{
		gameOverText.text = text;
	}
}
