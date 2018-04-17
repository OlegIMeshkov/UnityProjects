using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;


public class GameController : MonoBehaviour {



	public Text m_responseText;
	public Button m_sendButton;
	public RectTransform m_content;
	int m_contentItemsCount;


	public RectTransform[] screensArray;



	// Use this for initialization

		public string url = "http://yandex.ru";



	void Awake ()
	{
		
		ResetScreenPosition ();
		CameraToScreen (0);

	}

	IEnumerator GetRequestText()
		{
			using (WWW www = new WWW(url))
			{
				yield return www;

			if (www.responseHeaders.ContainsKey("Date"))
				{
				string dateString = www.responseHeaders ["Date"];
				m_responseText.text = dateString;

				}
			}
		}

	public void GetResponse()
	{
		StartCoroutine ("GetRequestText");
	}

	void ResetScreenPosition()
	{
		foreach (var r in screensArray) {
			r.localPosition = new Vector3 (0f, 0f);
		}
	}

	public void CameraToScreen (int screenNumber)
	{
		foreach (var r in screensArray) {
			r.gameObject.SetActive (false);
		}
		screensArray [screenNumber].gameObject.SetActive (true);

	}


}




