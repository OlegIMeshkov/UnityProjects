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
	void Awake ()
    {
		if (ins == null) 
		ins = this;
		else if (ins != this) 
		Destroy (gameObject);
	}

    public void SaveItems()
    {
        //open new XML file
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Create);
        serializer.Serialize(stream, itemDB);
        stream.Close();




    }


    public void LoadItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Open);
        itemDB = serializer.Deserialize(stream) as ItemDatabase;
        stream.Close();
    }



}

	[System.Serializable]
	public class ItemEntry
	{
		public string itemDescription;
		public int itemPriority;
		public int itemTimeEstimation;
	}

    [System.Serializable]
    public class ItemDatabase
	{

        [XmlArray("TaskBox")]
		public List<ItemEntry> list = new List<ItemEntry>();

	}
	

