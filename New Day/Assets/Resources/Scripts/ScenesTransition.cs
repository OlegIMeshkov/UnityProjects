using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScenesTransition : MonoBehaviour {

	public Image panelBackGround;

	float currentTime = 0f;
	public float panelFadeDuration;

	float abs;





	// Update is called once per frame
	void Update ()
	{
		if (gameObject.activeSelf)
		PanelFading();


	}



	void PanelFading ()
	{

		currentTime += Time.deltaTime;
		abs = Time.deltaTime / panelFadeDuration;

		if (currentTime <= panelFadeDuration) {
			//исчезновение фона
			panelBackGround.color = new Color (panelBackGround.color.r, panelBackGround.color.g, panelBackGround.color.b, Mathf.MoveTowards (panelBackGround.color.a, 0f, abs));

		} else
			gameObject.SetActive (false);

	}

}
