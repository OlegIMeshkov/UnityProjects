using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour {

	public GameObject m_gamePanel;

	void Start()
	{
		m_gamePanel.SetActive (false);
	}

	public void StartGame()
	{
		m_gamePanel.SetActive (true);
	}

}
