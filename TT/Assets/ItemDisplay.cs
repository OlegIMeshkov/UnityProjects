using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour {

	public ItemBlock blockPrefab;

	// Use this for initialization
	void Start ()
	{
        Display();
	}
	
	
	
	public void Display ()
	{
        foreach (ItemEntry item in XMLManager.ins.itemDB.list)
        {
            ItemBlock newBlock = Instantiate(blockPrefab) as ItemBlock;
            newBlock.transform.SetParent(GameManager.instance.tasksPanel.transform, false);
            newBlock.Display(item);




        
            
        }
	}
}
