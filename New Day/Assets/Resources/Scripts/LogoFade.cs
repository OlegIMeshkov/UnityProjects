using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LogoFade : MonoBehaviour {

	public Text companyName;
	public Image companyLogo;
	public Text partnersName;


	public float waitBeforeFadeTime;
	public float fadeDuration;
	public float fadeDuration2;
	float fadeStartTime;
	float fadeStartTime2;
	float partnersFade;
	float abs1;
	bool nextScene = false;




	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//StartCoroutine (WaitTime());


			StartCoroutine (CompanyFade ());



		if (nextScene) 
		{
			SceneManager.LoadScene ("main");
		}
	}
		


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



}