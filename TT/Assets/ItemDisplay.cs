using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour {

	public ItemBlock blockPrefab;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i<20; i++) 
		{
		Display();
	}
	
	}
	
	public void Display ()
	{
		ItemBlock newBlock = Instantiate(blockPrefab) as ItemBlock;
		newBlock.transform.SetParent(transform, false);
	}
}
