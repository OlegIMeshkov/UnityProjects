using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanelToActive : MonoBehaviour
{
    ItemBlock ib;
   
  


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
        ib = this.gameObject.GetComponent<ItemBlock>();
        GameManager.instance.currentTask = XMLManager.ins.itemDB.list.Find(t => t.itemID == ib.taskID);
        GameManager.instance.taskInRuntimeDescrtiption.text = GameManager.instance.currentTask.itemDescription;
        GameManager.instance.taskInRuntimePriority.text = GameManager.instance.currentTask.itemPriority.ToString();
        Debug.Log(GameManager.instance.currentTask.itemDescription);
        GameManager.instance.DisplayTimeTracking();
        
    }


}
