using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChangeSprite : MonoBehaviour {
	public float grid = 0.5f;



	void Start()
	{
		

	}

	void Update()
	{
		SetRoundPos ();
		SetSortingLayer ();

	}

	public void ChangeSpriteToStone()
	{
		
		foreach (Transform child in gameObject.transform)
		{
			SpriteRenderer spr = child.gameObject.GetComponent<SpriteRenderer> ();
			if (spr) 
			{
				
				string imageFilePath = "Sprites/stone/"+spr.sprite.name;
				Debug.Log (imageFilePath);
				spr.sprite = Resources.Load (imageFilePath, typeof(Sprite)) as Sprite;
			}
		}
	}

	public void ChangeSpriteToBase()
	{

		foreach (Transform child in gameObject.transform)
		{
			SpriteRenderer spr = child.gameObject.GetComponent<SpriteRenderer> ();
			if (spr) 
			{

				string imageFilePath = "Sprites/base/"+spr.sprite.name;
				Debug.Log (imageFilePath);
				spr.sprite = Resources.Load (imageFilePath, typeof(Sprite)) as Sprite;
			}
		}
	}

	public void ChangeSpriteToWooden()
	{

		foreach (Transform child in gameObject.transform)
		{
			SpriteRenderer spr = child.gameObject.GetComponent<SpriteRenderer> ();
			if (spr) 
			{

				string imageFilePath = "Sprites/wooden/"+spr.sprite.name;
				Debug.Log (imageFilePath);
				spr.sprite = Resources.Load (imageFilePath, typeof(Sprite)) as Sprite;
			}
		}
	}

	void SetSortingLayer()
	{
		foreach (Transform child in gameObject.transform)
		{
			SpriteRenderer spr = child.gameObject.GetComponent<SpriteRenderer> ();
			if (spr.sprite.name.Contains ("wall")) {
				spr.sortingOrder = 1;
			} else if (spr.sprite.name.Contains ("floor")) {
				spr.sortingOrder = 0;
			} else
				return;
			}
	}


	void SetRoundPos()
	{
		float recGrid = 1 / grid;
		foreach (Transform child in gameObject.transform)
		{
			child.transform.localPosition = new Vector3 (Mathf.Round (child.transform.localPosition.x* recGrid)/recGrid, Mathf.Round (child.transform.localPosition.y * recGrid)/recGrid, Mathf.Round (child.transform.localPosition.z));
		}
	}


}
