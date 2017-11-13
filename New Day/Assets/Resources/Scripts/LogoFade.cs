using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LogoFade : MonoBehaviour {

	public Text companyName;
	public Image companyLogo;
	public Text partnersName;

	/*

	public float waitBeforeFadeTime;
	public float fadeDuration;
	public float fadeDuration2;
	float fadeStartTime = 0f;
	float fadeStartTime2;
	float partnersFade;
	float abs1;
	bool nextScene = false;

*/


	float currentTime = 0f;
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



		/*if (nextScene) 
		{
			SceneManager.LoadScene ("main");
		}
		*/
	}
		
	/*

	IEnumerator CompanyFade()
	{	
		yield return new WaitForSeconds (waitBeforeFadeTime);

		fadeStartTime += Time.deltaTime;

		float abs = fadeStartTime / fadeDuration;

		companyName.color = new Color (companyName.color.r, companyName.color.g, companyName.color.b, Mathf.Lerp (companyName.color.a, 0f, abs));
		companyLogo.color = new Color (companyLogo.color.r, companyLogo.color.g, companyLogo.color.b, Mathf.Lerp (companyLogo.color.a, 0f, abs));

		//	yield return new WaitForSeconds (waitBeforeFadeTime);

		partnersName.gameObject.SetActive (true);

		abs *= (1 - Time.deltaTime) / fadeDuration;

		partnersName.color = new Color (partnersName.color.r, partnersName.color.g, partnersName.color.b, Mathf.Lerp (partnersName.color.a, 1f, abs));

		yield return new WaitForSeconds (waitBeforeFadeTime);


		nextScene = true;


	}

*/

	void Fading ()
	{
		

		abs = Time.deltaTime / fadeDuration;
		if (Time.time > sceneTime) {
			SceneManager.LoadScene ("main");
		}
		else if (Time.time > (blankToLogoTime + logoToBlankTime + designersToBlankTime)) 
		{
			partnersName.color = new Color (partnersName.color.r, partnersName.color.g, partnersName.color.b, Mathf.MoveTowards (partnersName.color.a, 0f, abs));
			Debug.Log ("4");
		} else if (Time.time > (blankToLogoTime + logoToBlankTime)) {
			partnersName.gameObject.SetActive (true);
			partnersName.color = new Color (partnersName.color.r, partnersName.color.g, partnersName.color.b, Mathf.MoveTowards (partnersName.color.a, 1f, abs));
			Debug.Log ("3");
		} else if (Time.time > blankToLogoTime) {
			//исчезновение лого
			companyName.color = new Color (companyName.color.r, companyName.color.g, companyName.color.b, Mathf.MoveTowards (companyName.color.a, 0f, abs));
			companyLogo.color = new Color (companyLogo.color.r, companyLogo.color.g, companyLogo.color.b, Mathf.MoveTowards (companyLogo.color.a, 0f, abs));
			Debug.Log ("2");
		} else if (Time.time < blankToLogoTime) {
			//появление лого

			companyName.color = new Color (companyName.color.r, companyName.color.g, companyName.color.b, Mathf.MoveTowards (companyName.color.a, 1f, abs));
			companyLogo.color = new Color (companyLogo.color.r, companyLogo.color.g, companyLogo.color.b, Mathf.MoveTowards (companyLogo.color.a, 1f, abs));
			Debug.Log ("1");
		} 

	}

}