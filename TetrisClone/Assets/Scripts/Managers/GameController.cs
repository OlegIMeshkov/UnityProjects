using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	Board m_gameBoard;

	Spawner m_spawner;

	Shape m_activeShape;

	float m_dropIntreval = 0.2f;
	float m_timeToDrop;

	float m_timeToNextKey;

	[Range(0.02f,1f)]
	public float m_keyRepeatRate = 0.25f;


	// Use this for initialization
	void Start () 
	{
		m_timeToNextKey = Time.time;
		m_gameBoard = GameObject.FindObjectOfType<Board> ();
		m_spawner = GameObject.FindObjectOfType<Spawner> ();

		if (!m_gameBoard) 
		{
			Debug.LogWarning("Warining! There is no game board defined!");
		}

		if (!m_spawner) 
		{
			Debug.LogWarning ("Warining! There is no spawner defined!");
		}

		if (m_activeShape == null) 
		{
			m_activeShape = m_spawner.SpawnShape ();
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton ("MoveRight") && Time.time > m_timeToNextKey || Input.GetButtonDown ("MoveRight"))
		{
			m_activeShape.MoveRight ();

			m_timeToNextKey = Time.time + m_keyRepeatRate;



			if (m_gameBoard.IsValidPosition (m_activeShape)) {
				Debug.Log ("Move Right");
			} else
				m_activeShape.MoveLeft ();
			Debug.Log ("Hit the right boundary");
		}

		if (Time.time > m_timeToDrop) 
		{
			m_timeToDrop = Time.time + m_dropIntreval;
			if (m_activeShape)
			{
				m_activeShape.MoveDown ();
				if (!m_gameBoard.IsValidPosition (m_activeShape))
				{
					m_activeShape.MoveUp ();
					m_gameBoard.StoreShapeInGrid (m_activeShape);


					if (m_spawner) 
					{
						m_activeShape = m_spawner.SpawnShape ();

					}
				}
			}

		}

	}




}
