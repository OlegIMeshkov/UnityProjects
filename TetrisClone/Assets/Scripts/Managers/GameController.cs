using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	Board m_gameBoard;

	Spawner m_spawner;

	Shape m_activeShape;

	float m_dropIntreval = 1f;
	float m_timeToDrop;



	// Use this for initialization
	void Start () 
	{
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
		if (Time.time > m_timeToDrop) 
		{
			m_timeToDrop = Time.time + m_dropIntreval;
			if (m_activeShape)
			{
				m_activeShape.MoveDown ();
				if (!m_gameBoard.IsValidPosition (m_activeShape))
				{
					m_activeShape.MoveUp ();
					if (m_spawner) 
					{
						m_activeShape = m_spawner.SpawnShape ();

					}
				}
			}

		}

	}


}
