using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroScript : MonoBehaviour {

	public PickingScript m_pickingScript;
	private Sprite m_heroImage;
	public bool m_usedHero;

	void Start()
	{
		m_heroImage = GetComponent<Image> ().sprite;
	}

	public void UseHero()
	{
		if (!m_usedHero) 
		{
			m_usedHero = true;
			GetComponent<Image> ().color = m_pickingScript.m_usedHeroColor;
			m_pickingScript.UseHero (m_heroImage);
		}
	}
}
