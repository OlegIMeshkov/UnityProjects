﻿using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

	public int m_height = 30;
	public int m_width = 10;
	public Transform m_emptySprite;
	public int m_header = 8;
	Transform[,] m_grid;

	void Awake ()
	{
		m_grid = new Transform[m_width, m_height];
	}

	// Use this for initialization
	void Start () 
	{
		DrawEmptyCells ();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void DrawEmptyCells ()
	{
		if (m_emptySprite != null) {
			for (int y = 0; y < m_height - m_header; y++) {
				for (int x = 0; x < m_width; x++) {
					Transform clone;
					clone = Instantiate (m_emptySprite, new Vector3 (x, y, 0f), Quaternion.identity) as Transform;
					clone.name = "Board Space ( x = " + x.ToString () + " , y = " + y.ToString () + ")";
					clone.transform.parent = transform;
				}
			}
		} else 
		{
			Debug.Log ("Warning! No sprite");
		}
	}
}
