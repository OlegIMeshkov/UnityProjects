using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBlock : MonoBehaviour {

    public Text taskDescription, taskPriority, taskTimeEstimation;

    [SerializeField]
    private int taskID;

    public void Display(ItemEntry item)
    {
        taskID = item.itemID;
        taskDescription.text = item.itemDescription;
        taskPriority.text = item.itemPriority.ToString();
        taskTimeEstimation.text = item.itemTimeEstimation.ToString();

    }

}
