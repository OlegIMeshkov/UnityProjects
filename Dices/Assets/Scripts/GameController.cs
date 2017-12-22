using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	#region Variables
	public float rows = 12f;
	public float columns = 12f;
	public int counter;
	public int isCounted =0;
	public int diceCount;
	int number = 0;

	Vector3 offset;
	public GameObject prefab;
	GameObject[] gameObjArr;
	Vector3 poolPosition = new Vector3 (500f, 500f, 500f);
	public bool counted = false;


	public Text uiPrefab;
	public Transform uiParent;
	public Text centerText;

	public static GameController instance;
	#endregion

	#region Unity Methods
	// Use this for initialization
	void Start ()
	{
		instance = this;
		CreateDicePool ();
	}


	// Update is called once per frame
	void Update () 
	{
		ShowResult ();
	}


	#endregion

	#region Custom Methods
	void ShowResult ()
	{
		if (isCounted == diceCount && counted == false && number != 0) {
			counted = true;
			Text uiText = Instantiate (uiPrefab, uiParent.transform) as Text;
			uiText.text = counter.ToString ();
			StartCoroutine (ShowCenterUI ());
		}
	}

	void CreateDicePool ()
	{
		gameObjArr = new GameObject[100];
		for (int i = 0; i < gameObjArr.Length; i++) {
			gameObjArr [i] = Instantiate (prefab, poolPosition + Vector3.one*3f*i, Quaternion.identity) as GameObject;
			gameObjArr [i].transform.SetParent (gameObject.transform);

		}
	}

	void Reposition ()
	{
		diceCount = Mathf.RoundToInt(FindObjectOfType<Slider> ().value);

		number = 0;

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {

				offset = new Vector3 (3f * i, 2f, 3f * j);
				gameObjArr [number].transform.position = Vector3.zero + offset;
				gameObjArr [number].GetComponent<ThrowDice> ().isActive = true;
				number++;
				if (number == diceCount) {
					return;
				}
			}
		}
	}

	public void Reset ()
	{
		for (int i = 0; i < gameObjArr.Length; i++)
		{
			gameObjArr [i].transform.eulerAngles = new Vector3 (0f, 0f, 0f);
			gameObjArr [i].GetComponent<ThrowDice> ().isActive = false;
			gameObjArr [i].GetComponent<ThrowDice> ().isThrown = false;
			gameObjArr [i].GetComponent<ThrowDice> ().diceCounted = false;
			gameObjArr [i].GetComponent<Rigidbody> ().useGravity = false;
			gameObjArr [i].GetComponent<Rigidbody> ().isKinematic = true;
			gameObjArr [i].transform.position = poolPosition + Vector3.one * 3f * i;


		}
		number = 0;
		counted = false;	
		isCounted = 0;
		counter = 0;
		Reposition ();
	}

	IEnumerator ShowCenterUI ()
	{
		centerText.gameObject.SetActive (true);
		centerText.text = counter.ToString ();
		yield return new WaitForSeconds (2f);
		centerText.gameObject.SetActive (false);
	}

	#endregion

}
