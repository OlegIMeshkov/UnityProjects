using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour {

	public ItemBlock blockPrefab;

	// Use this for initialization
	void Start ()
	{
        XMLManager.ins.LoadItems();
        Display();
	}
	
	
	
	public void Display ()
	{
        foreach (Transform child in GameManager.instance.tasksPanel)
        {
            Destroy(child.gameObject);
        }
        foreach (ItemEntry item in XMLManager.ins.itemDB.list)
        {
            ItemBlock newBlock = Instantiate(blockPrefab) as ItemBlock;
            newBlock.transform.SetParent(GameManager.instance.tasksPanel.transform, false);
            newBlock.Display(item);
                                            
            
        }
	}
}
