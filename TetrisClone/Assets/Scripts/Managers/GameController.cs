﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	Board m_gameBoard;

	Spawner m_spawner;

	Shape m_activeShape;

	SoundManager m_soundManager;

	float m_dropIntreval = 0.9f;
	float m_timeToDrop;

	float m_timeToNextKeyLeftRight;
	[Range(0.02f,1f)]
	public float m_keyRepeatRateLeftRight = 0.25f;

	float m_timeToNextKeyDown;
	[Range(0.02f,1f)]
	public float m_keyRepeatRateDown = 0.25f;

	float m_timeToNextKeyRotate;
	[Range(0.02f,1f)]
	public float m_keyRepeatRateRotate = 0.25f;

	bool m_gameOver = false;

	public GameObject m_gameOverPanel;

	public IconToggle m_rotIconToggle;
	bool m_clockwise = true;


	// Use this for initialization
	void Start () 
	{

		m_gameOverPanel.SetActive (false);

		m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;
		m_timeToNextKeyDown = Time.time + m_keyRepeatRateDown;
		m_timeToNextKeyRotate = Time.time + m_keyRepeatRateRotate;

		m_gameBoard = GameObject.FindObjectOfType<Board> ();
		m_spawner = GameObject.FindObjectOfType<Spawner> ();
		m_soundManager = GameObject.FindObjectOfType<SoundManager> ();

		if (!m_gameBoard) 
		{
			Debug.LogWarning("Warining! There is no game board defined!");
		}
		if (!m_soundManager) 
		{
			Debug.LogWarning("Warining! There is no Sound Manager defined!");
		}

		if (!m_spawner) 
		{
			Debug.LogWarning ("Warining! There is no spawner defined!");
		} else 
		{
			m_spawner.transform.position = Vectorf.Round (m_spawner.transform.position);
			if (m_activeShape == null) 
			{
				m_activeShape = m_spawner.SpawnShape ();
			}
		}

		if (m_gameOverPanel) {
			m_gameOverPanel.SetActive (false);
		}

	}

	void Update () 
	{
		if (!m_gameBoard || !m_spawner || !m_activeShape || m_gameOver ||!m_soundManager) 
		{
			return;
		}

		PlayerInput ();


	}

	void LandShape ()
	{
		m_timeToNextKeyLeftRight = Time.time;
		m_timeToNextKeyDown = Time.time;
		m_timeToNextKeyRotate = Time.time;

		m_activeShape.MoveUp ();
		m_gameBoard.StoreShapeInGrid (m_activeShape);

		PlaySound (m_soundManager.m_dropSound, 0.75f);
		m_activeShape = m_spawner.SpawnShape ();   

		m_gameBoard.ClearAllRows ();

		if (m_gameBoard.m_completedRows >0)
		{
			if (m_gameBoard.m_completedRows>1) {
				AudioClip randomVocal = m_soundManager.GetRandomClip (m_soundManager.m_vocalClips);
				PlaySound (randomVocal, 3f);
			}
			PlaySound (m_soundManager.m_clearRowSound, 3f);
		}
	}

	void GameOver ()
	{
		m_activeShape.MoveUp ();
		m_gameOver = true;
		Debug.LogWarning (m_activeShape.name + " is over the limit");

		PlaySound (m_soundManager.m_gameOverSound, 5f);
		PlaySound (m_soundManager.m_gameOverVocalClip, 5f);

		if (m_gameOverPanel) {
			m_gameOverPanel.SetActive (true);
		}
	}

	void PlaySound (AudioClip clip, float volMultiplier)
	{
		if (m_soundManager.m_fxEnabled && clip) {
			AudioSource.PlayClipAtPoint (clip, Camera.main.transform.position, Mathf.Clamp(m_soundManager.m_fxVolume * volMultiplier,0.05f, 1f));
		}
	}

	void PlayerInput ()
	{
		if (Input.GetButton ("MoveRight") && Time.time > m_timeToNextKeyLeftRight || Input.GetButtonDown ("MoveRight"))
		{
			m_activeShape.MoveRight ();
			m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;
			if (!m_gameBoard.IsValidPosition (m_activeShape)) {
				m_activeShape.MoveLeft ();
				PlaySound (m_soundManager.m_errorSound, 0.7f); 
			} else 
			{
				PlaySound (m_soundManager.m_moveSound, 0.5f); 
			}
		} 
		else if (Input.GetButton ("MoveLeft") && Time.time > m_timeToNextKeyLeftRight || Input.GetButtonDown ("MoveLeft"))
		{
			m_activeShape.MoveLeft ();
			m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;
			  

			if (!m_gameBoard.IsValidPosition (m_activeShape))
			{
				m_activeShape.MoveRight ();
				PlaySound (m_soundManager.m_errorSound, 0.7f); 
			}else 
			{
				PlaySound (m_soundManager.m_moveSound, 0.5f); 
			}
		}
		else if (Input.GetButtonDown ("Rotate") && Time.time > m_timeToNextKeyRotate) 
		{
			//m_activeShape.RotateRight ();
			m_activeShape.RotateClockwise (m_clockwise);
			m_timeToNextKeyRotate = Time.time + m_keyRepeatRateRotate;

			if (!m_gameBoard.IsValidPosition (m_activeShape)) {
				//m_activeShape.RotateLeft ();
				m_activeShape.RotateClockwise (!m_clockwise);
				PlaySound (m_soundManager.m_errorSound, 0.7f); 
			}else 
			{
				PlaySound (m_soundManager.m_moveSound, 0.5f); 
			}
		} 
		else if ((Input.GetButton("MoveDown") && Time.time > m_timeToNextKeyDown) || Time.time > m_timeToDrop) 
		{
			m_timeToDrop = Time.time + m_dropIntreval;
			m_timeToNextKeyDown = Time.time + m_keyRepeatRateDown;
			m_activeShape.MoveDown ();

			if (!m_gameBoard.IsValidPosition (m_activeShape))
			{
				if (m_gameBoard.IsOverLimit (m_activeShape))
				{
					GameOver ();
				} else {
					LandShape ();
				}
			}
		} 
		else if (Input.GetButtonDown("ToggleRot")) {
			ToggleRotDirection ();
		}
	}

	public void Restart ()
	{
		Debug.Log ("Restarted");
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}


	public void ToggleRotDirection ()
	{
		m_clockwise = !m_clockwise;
		if (m_rotIconToggle) {
			m_rotIconToggle.ToggleIcon (m_clockwise);
		}
	}

}
