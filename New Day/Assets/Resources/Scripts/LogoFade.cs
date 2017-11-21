using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LogoFade : MonoBehaviour {


	public Image companyLogo;
	public Text partnersName;

	public float fadeDuration;
	public float blankToLogoTime;
	public float logoToBlankTime;
	public float blankToDesignersTime;
	public float designersToBlankTime;

	public float sceneTime;


	float abs;





	// Update is called once per frame
	void Update ()
	{
		Fading();


	}
		


	void Fading ()
	{
		

		abs = Time.deltaTime / fadeDuration;
		if (Time.time > sceneTime) {
			SceneManager.LoadScene ("main");
		}
		else if (Time.time > (blankToLogoTime + logoToBlankTime + designersToBlankTime)) 
		{
			partnersName.color = new Color (partnersName.color.r, partnersName.color.g, partnersName.color.b, Mathf.MoveTowards (partnersName.color.a, 0f, abs));

		} else if (Time.time > (blankToLogoTime + logoToBlankTime)) {
			partnersName.gameObject.SetActive (true);
			partnersName.color = new Color (partnersName.color.r, partnersName.color.g, partnersName.color.b, Mathf.MoveTowards (partnersName.color.a, 1f, abs));

		} else if (Time.time > blankToLogoTime) {
			//исчезновение лого

			companyLogo.color = new Color (companyLogo.color.r, companyLogo.color.g, companyLogo.color.b, Mathf.MoveTowards (companyLogo.color.a, 0f, abs));

		} else if (Time.time < blankToLogoTime) {
			//появление лого


			companyLogo.color = new Color (companyLogo.color.r, companyLogo.color.g, companyLogo.color.b, Mathf.MoveTowards (companyLogo.color.a, 1f, abs));

		} 

	}

}