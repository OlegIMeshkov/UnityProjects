using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;


public class PlayerSetup : NetworkBehaviour {


	[SyncVar(hook = "UpdateColor")]
	public Color m_playerColor;
	public string m_basename = "PLAYER";

	[SyncVar(hook = "UpdateName")]
	public int m_playerNum = 1;
	public Text m_playerNameText;



	public override void OnStartClient ()
	{
		base.OnStartClient ();
		if (m_playerNameText != null) {
			m_playerNameText.enabled = false;

		}
	}


	void UpdateColor(Color pColor)
	{
		MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer r in meshes) {
			r.material.color = pColor;
		}
	}

	void UpdateName (int pNum)
	{
		if (m_playerNameText != null) {
			m_playerNameText.enabled = true;
			m_playerNameText.text = m_basename + pNum.ToString ();
		}
	}

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		CmdSetupPlayer ();
		//UpdateColor();
		//UpdateName ();
	}

	[Command]
	void CmdSetupPlayer ()
	{
		GameManager.Instance.AddPlayer (this);
		GameManager.Instance.m_playersCount++;
	}

	void Start ()
	{
		if (!isLocalPlayer) {
			UpdateName (m_playerNum);
			UpdateColor (m_playerColor);
		}
	}
}
