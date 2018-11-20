using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using System.Linq;


[System.Serializable]
public class ItemDatabase
{
 

    [XmlArray("TaskBox")]
    public List<ItemEntry> list = new List<ItemEntry>();



}

