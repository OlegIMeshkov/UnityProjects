using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBlock : MonoBehaviour {

    public Text taskDescription, taskPriority, taskTimeEstimation;

    
    public int taskID;

    public void Display(ItemEntry item)
    {
        taskID = item.itemID;
        taskDescription.text = item.itemDescription;
        taskPriority.text = item.itemPriority.ToString();
        taskTimeEstimation.text = (Mathf.FloorToInt(item.itemTimeEstimationInSeconds/3600)).ToString() + "h " 
            + (Mathf.FloorToInt(item.itemTimeEstimationInSeconds % 3600)/60).ToString() + "m "
            + (Mathf.FloorToInt(item.itemTimeEstimationInSeconds % 60)).ToString() + "s ";

    }

}
