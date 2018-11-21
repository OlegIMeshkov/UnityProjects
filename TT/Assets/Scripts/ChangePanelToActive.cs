using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanelToActive : MonoBehaviour
{

    public void SetPanelActive(int panelIndex)
    {
        foreach (RectTransform p in GameManager.instance.m_panelsArray)
        {
            p.gameObject.SetActive(false);
        }

        GameManager.instance.m_panelsArray[panelIndex].gameObject.SetActive(true);
    }

    

    public void TaskStart()
    {
        ItemBlock ib = this.gameObject.GetComponent<ItemBlock>();
        ItemEntry taskInRuntime = XMLManager.ins.itemDB.list.Find(t => t.itemID == ib.taskID);
        GameManager.instance.taskInRuntimeDescrtiption.text = taskInRuntime.itemDescription;
        GameManager.instance.taskInRuntimePriority.text = taskInRuntime.itemPriority.ToString();
        Debug.Log(taskInRuntime.itemDescription);
        GameManager.instance.taskInRuntimeScheduledTime_hours.text = ((int)(taskInRuntime.itemTimeEstimation / 60)).ToString();
        GameManager.instance.taskInRuntimeScheduledTime_minutes.text = ((int)(taskInRuntime.itemTimeEstimation % 60)).ToString();
    }
}
