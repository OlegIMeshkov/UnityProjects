using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemDisplay : MonoBehaviour {

    public static ItemDisplay itemDisplayInstance;

	public ItemBlock blockPrefab;


    void Awake()
    {
        itemDisplayInstance = this;
    }


    // Use this for initialization
    void Start ()
	{

        XMLManager.ins.LoadItems();
        DisplayFilteredByPriority();
        GameManager.instance.tasksPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(GameManager.instance.tasksPanel.GetComponent<RectTransform>().sizeDelta.x, (GameManager.instance.tasksPanel.childCount) * 100 - Screen.height + GameManager.instance.newTaskButton.rect.height);
    }
	
	
	
	public void Display ()
    {
        ClearTaskPanel();
        FillTaskPanelWithList(XMLManager.ins.itemDB.list);
    }

    public void DisplayFilteredByPriority()
    {
        ClearTaskPanel();
       
        List<ItemEntry> orderedList = XMLManager.ins.itemDB.list.Where(t => t.isCompleted == false).OrderByDescending(t => t.itemPriority).ToList<ItemEntry>();
        FillTaskPanelWithList(orderedList);
    }


    public void DisplayFilteredByScheduledTime()
    {
        ClearTaskPanel();

        List<ItemEntry> orderedList = XMLManager.ins.itemDB.list.Where(t => t.isCompleted == false).OrderByDescending(t => t.itemTimeEstimationInSeconds).ToList<ItemEntry>();

        FillTaskPanelWithList(orderedList);
    }


    public void DisplayFilteredByID()
    {
        ClearTaskPanel();

        List<ItemEntry> orderedList = XMLManager.ins.itemDB.list.Where(t => t.isCompleted == false).OrderByDescending(t => t.itemID).ToList<ItemEntry>();
        FillTaskPanelWithList(orderedList);
    }


    private void FillTaskPanelWithList(List<ItemEntry> itemEntries)
    {
        foreach (ItemEntry item in itemEntries)
        {
            ItemBlock newBlock = Instantiate(blockPrefab) as ItemBlock;
            newBlock.transform.SetParent(GameManager.instance.tasksPanel.transform, false);
            newBlock.Display(item);
        }

        Debug.Log("Task panel has been filled by list" + itemEntries.ToString());
    }


    private static void ClearTaskPanel()
    {
        foreach (Transform child in GameManager.instance.tasksPanel)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("Task panel has been cleared");
    }

}
