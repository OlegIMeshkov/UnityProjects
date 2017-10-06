using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	Board m_gameBoard;

	Spawner m_spawner;


	// Use this for initialization
	void Start () 
	{
		m_gameBoard = GameObject.FindObjectOfType<Board> ();
		m_spawner = GameObject.FindObjectOfType<Spawner> ();

		if (!m_gameBoard) 
		{
			Debug.LogWarning("Warining! There is no game board defined!")
		}

		if (!m_spawner) 
		{
			Debug.LogWarning("Warining! There is no spawner defined!")
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
