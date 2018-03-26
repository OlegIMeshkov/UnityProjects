using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PickingScript : MonoBehaviour {

	public Transform m_playerPicks;
	public Transform m_playerBans;
	public Transform m_enemyPicks;
	public Transform m_enemyBans;

	public GameObject m_playerTurnSign;
	public GameObject m_enemyTurnSign;

	public Text m_mainTimeText;
	public Text m_playerExtraTimeText;
	public Text m_enemyExtraTimeText;

	public Color m_usedHeroColor;

	private Transform m_slotToUse;

	public int m_playerPickBanIndex = 1;
	public int m_enemyPickBanIndex = 1;

	public bool m_playersTurn;
	private bool m_pickTurn;
	private bool m_mainTimeRanOut;

	private float m_mainTime = 30f;
	private float m_playersExtraTime = 110f;
	private float m_enemyExtraTime = 110f;

	void Start () 
	{
		if (Random.value > 0.5f)
			m_playersTurn = true;
		else
			m_playersTurn = false;
		PrepareGame ();
	}
	
	void Update () 
	{
		ShowTurnSign ();
		DecreaseTime ();
	}

	public void UseHero(Sprite heroImage)
	{
		GetComponent<Image> ().color = m_usedHeroColor;
		if (m_playersTurn)
		{
			switch (m_playerPickBanIndex) 
			{
			case 1:
				m_playerBans.GetChild (0).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_enemyBans.GetChild (0));
				}
				else 
				{
					SetSlotToUse (m_enemyBans.GetChild (1));
				}
				m_playerPickBanIndex++;
				SwitchTurn ();
				break;
			case 2:
				m_playerBans.GetChild (1).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_enemyBans.GetChild (1));
				}
				else 
				{
					SetSlotToUse (m_enemyPicks.GetChild (0));
				}
				m_playerPickBanIndex++;
				SwitchTurn ();
				break;
			case 3:
				m_playerPicks.GetChild (0).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_enemyPicks.GetChild (0));
				}
				else 
				{
					SetSlotToUse (m_enemyPicks.GetChild (1));
				}
				m_playerPickBanIndex++;
				SwitchTurn ();
				break;
			case 4:
				m_playerPicks.GetChild (1).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_enemyPicks.GetChild (1));
				}
				else 
				{
					SetSlotToUse (m_enemyBans.GetChild (2));
				}
				m_playerPickBanIndex++;
				SwitchTurn ();
				break;
			case 5:
				m_playerBans.GetChild (2).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_enemyBans.GetChild (2));
				}
				else 
				{
					SetSlotToUse (m_enemyBans.GetChild (3));
				}
				m_playerPickBanIndex++;
				SwitchTurn ();
				break;
			case 6:
				m_playerBans.GetChild (3).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_enemyBans.GetChild (3));
				}
				else 
				{
					SetSlotToUse (m_enemyPicks.GetChild (2));
				}
				m_playerPickBanIndex++;
				SwitchTurn ();
				break;
			case 7:
				m_playerPicks.GetChild (2).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_enemyPicks.GetChild (2));
				}
				else 
				{
					SetSlotToUse (m_enemyPicks.GetChild (3));
				}
				m_playerPickBanIndex++;
				SwitchTurn ();
				break;
			case 8:
				m_playerPicks.GetChild (3).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_enemyPicks.GetChild (3));
				}
				else 
				{
					SetSlotToUse (m_enemyBans.GetChild (4));
				}
				m_playerPickBanIndex++;
				SwitchTurn ();
				break;
			case 9:
				m_playerBans.GetChild (4).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_enemyBans.GetChild (4));
				}
				else 
				{
					SetSlotToUse (m_enemyPicks.GetChild (4));
				}
				m_playerPickBanIndex++;
				SwitchTurn ();
				break;
			case 10:
				m_playerPicks.GetChild (4).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_enemyPicks.GetChild (4));
				}
				else
				{
					m_playerPicks.GetChild (4).GetComponent<Image> ().color = Color.white;
					m_enemyTurnSign.SetActive (false);
					m_playerTurnSign.SetActive (false);
				}
				m_playerPickBanIndex++;
				SwitchTurn ();
				break;
			default:
				break;
			}
		}
		else
		{
			switch (m_enemyPickBanIndex) 
			{
			case 1:
				m_enemyBans.GetChild (0).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_playerBans.GetChild (0));
				}
				else 
				{
					SetSlotToUse (m_playerBans.GetChild (1));
				}
				m_enemyPickBanIndex++;
				SwitchTurn ();
				break;
			case 2:
				m_enemyBans.GetChild (1).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_playerBans.GetChild (1));
				}
				else 
				{
					SetSlotToUse (m_playerPicks.GetChild (0));
				}
				m_enemyPickBanIndex++;
				SwitchTurn ();
				break;
			case 3:
				m_enemyPicks.GetChild (0).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_playerPicks.GetChild (0));
				}
				else 
				{
					SetSlotToUse (m_playerPicks.GetChild (1));
				}
				m_enemyPickBanIndex++;
				SwitchTurn ();
				break;
			case 4:
				m_enemyPicks.GetChild (1).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_playerPicks.GetChild (1));
				}
				else 
				{
					SetSlotToUse (m_playerBans.GetChild (2));
				}
				m_enemyPickBanIndex++;
				SwitchTurn ();
				break;
			case 5:
				m_enemyBans.GetChild (2).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_playerBans.GetChild (2));
				}
				else 
				{
					SetSlotToUse (m_playerBans.GetChild (3));
				}
				m_enemyPickBanIndex++;
				SwitchTurn ();
				break;
			case 6:
				m_enemyBans.GetChild (3).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_playerBans.GetChild (3));
				}
				else 
				{
					SetSlotToUse (m_playerPicks.GetChild (2));
				}
				m_enemyPickBanIndex++;
				SwitchTurn ();
				break;
			case 7:
				m_enemyPicks.GetChild (2).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_playerPicks.GetChild (2));
				}
				else 
				{
					SetSlotToUse (m_playerPicks.GetChild (3));
				}
				m_enemyPickBanIndex++;
				SwitchTurn ();
				break;
			case 8:
				m_enemyPicks.GetChild (3).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_playerPicks.GetChild (3));
				}
				else 
				{
					SetSlotToUse (m_playerBans.GetChild (4));
				}
				m_enemyPickBanIndex++;
				SwitchTurn ();
				break;
			case 9:
				m_enemyBans.GetChild (4).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_playerBans.GetChild (4));
				}
				else 
				{
					SetSlotToUse (m_playerPicks.GetChild (4));
				}
				m_enemyPickBanIndex++;
				SwitchTurn ();
				break;
			case 10:
				m_enemyPicks.GetChild (4).GetComponent<Image> ().sprite = heroImage;
				if (m_playerPickBanIndex == m_enemyPickBanIndex) 
				{
					SetSlotToUse (m_playerPicks.GetChild (4));
				}
				else
				{
					m_enemyPicks.GetChild (4).GetComponent<Image> ().color = Color.white;
					m_enemyTurnSign.SetActive (false);
					m_playerTurnSign.SetActive (false);
				}
				m_enemyPickBanIndex++;
				SwitchTurn ();
				break;
			default:
				break;
			}
		}
	}

	void PrepareGame()
	{
		foreach (Transform child in m_playerPicks) 
		{
			child.GetComponent<Image> ().sprite = null;
		}
		foreach (Transform child in m_playerBans) 
		{
			child.GetComponent<Image> ().sprite = null;
		}
		foreach (Transform child in m_enemyPicks) 
		{
			child.GetComponent<Image> ().sprite = null;
		}
		foreach (Transform child in m_enemyBans) 
		{
			child.GetComponent<Image> ().sprite = null;
		}

		if (m_playersTurn) 
		{
			m_playerBans.GetChild (0).GetComponent<Image> ().color = Color.blue;
			m_slotToUse = m_playerBans.GetChild (0);
		}
		else
		{
			m_enemyBans.GetChild (0).GetComponent<Image> ().color = Color.blue;
			m_slotToUse = m_enemyBans.GetChild (0);
		}
		m_playersExtraTime = 110f;
		m_enemyExtraTime = 110f;
		m_enemyPickBanIndex = 1;
		m_playerPickBanIndex = 1;
		m_playerExtraTimeText.text = Mathf.FloorToInt(m_playersExtraTime/60).ToString() + ':' + Mathf.CeilToInt(m_playersExtraTime % 60).ToString();
		m_enemyExtraTimeText.text = Mathf.FloorToInt(m_enemyExtraTime/60).ToString() + ':' + Mathf.CeilToInt(m_enemyExtraTime % 60).ToString();
		m_pickTurn = false;
		m_mainTime = 30f;
	}

	void DecreaseTime ()
	{
		if (!m_mainTimeRanOut) 
		{
			m_mainTime -= Time.deltaTime;
			m_mainTimeText.text = Mathf.CeilToInt (m_mainTime).ToString ();
			if (m_mainTime <= 0f)
				m_mainTimeRanOut = true;
		}
		else if (m_playersTurn) 
		{
			m_playersExtraTime -= Time.deltaTime;
			m_playerExtraTimeText.text = Mathf.FloorToInt (m_playersExtraTime / 60).ToString () + ':' + Mathf.CeilToInt (m_playersExtraTime % 60).ToString ();
		}
		else 
		{
			m_enemyExtraTime -= Time.deltaTime;
			m_enemyExtraTimeText.text = Mathf.FloorToInt (m_enemyExtraTime / 60).ToString () + ':' + Mathf.CeilToInt (m_enemyExtraTime % 60).ToString ();
		}
	}

	void ShowTurnSign ()
	{
		if (m_playersTurn) 
		{
			m_playerTurnSign.SetActive (true);
			m_enemyTurnSign.SetActive (false);
		}
		else
		{
			m_enemyTurnSign.SetActive (true);
			m_playerTurnSign.SetActive (false);
		}
	}

	void SetSlotToUse(Transform slotToUse)
	{
		m_slotToUse.GetComponent<Image> ().color = Color.white;
		m_slotToUse = slotToUse;
		m_slotToUse.GetComponent<Image> ().color = Color.blue;
	}

	public void SwitchTurn()
	{
		m_playersTurn = !m_playersTurn;
		m_mainTime = 30f;
	}
}
