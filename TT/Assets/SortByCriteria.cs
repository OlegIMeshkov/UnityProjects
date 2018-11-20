using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class SortByCriteria : MonoBehaviour {

    public Text labelText;


    public void CheckLabelText()
    {
        switch (labelText.text)
        {
            case "Priority":
                Debug.Log("Ok, you chose Priority");

                ItemDisplay.itemDisplayInstance.DisplayFilteredByPriority();
                break;

            case "Scheduled time":
                Debug.Log("Ok, you chose Scheduled time");

                ItemDisplay.itemDisplayInstance.DisplayFilteredByScheduledTime();
                break;

            case "Date created":
                Debug.Log("Ok, you chose Date created");

                ItemDisplay.itemDisplayInstance.DisplayFilteredByID();
                break;
            default:
                break;
        }
        

    }

}
