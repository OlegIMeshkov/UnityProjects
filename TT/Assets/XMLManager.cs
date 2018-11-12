using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour {


	public static XMLManager ins;
	public ItemDatabase itemDB;



	// Use this for initialization
	void Awake () {
		if (ins == null) 
		ins = this;
		else if (ins != this) 
		Destroy (gameObject);


	}
}

	[System.Serializable]
	public class ItemEntry
	{
		public string itemDescription;
		public int priority;
		public int timeEstimation;
	}

	public class ItemDatabase
	{
		public List<ItemEntry> list = new List<ItemEntry>();

	}
	

