using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ChangeSprite))]
public class ChangeSpriteEditor : Editor {
	


	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();	

		GUILayout.BeginHorizontal ();

		if (GUILayout.Button ("ToStone")) 
		{
			ChangeSprite changeS = (ChangeSprite)target;
			changeS.ChangeSpriteToStone ();
		}
		if (GUILayout.Button ("ToBase")) 
		{
			ChangeSprite changeS = (ChangeSprite)target;
			changeS.ChangeSpriteToBase ();
		}
		if (GUILayout.Button ("ToWooden")) 
		{
			ChangeSprite changeS = (ChangeSprite)target;
			changeS.ChangeSpriteToWooden ();
		}

		GUILayout.EndHorizontal ();


	}
		
}
