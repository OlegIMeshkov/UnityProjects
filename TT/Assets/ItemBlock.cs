using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBlock : MonoBehaviour {

	public Text taskDescription, taskPriority, taskTimeEstimation;

    public void Display(ItemEntry item)
    {
        taskDescription.text = item.itemDescription;
        taskPriority.text = item.itemPriority.ToString();
        taskTimeEstimation.text = item.itemTimeEstimation.ToString();

    }

}
