using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemEntry
{
    public string itemDescription;
    public int itemPriority;
    public float itemTimeEstimationInSeconds;
    public int itemID;
    public bool isActive = false;
    public bool isCompleted = false;


}
