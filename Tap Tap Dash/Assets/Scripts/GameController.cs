using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {
	
	// for Awake ()
	public static GameController instance;

	// for CreateNextLevel () 
	public GameObject[] levels;

	// for Setup ()
	public GameObject player;
	public GameObject cameraPrefab;
	public GameObject placeOfFall;
	public Vector3 fallPosition;

	// for Restart ()
	public bool gameOver=false;

	// for SavePlayerProgress ()
	public int currentLevel = 0;

	// for UI
	public Text currentLevelText;

	// for SaveFallPosition ()
	public Vector3 fallDelta;

	// for CheckIsDead ()
	public  Vector3 newStartPos;
	public GameObject pausePanel;

	//for ChangeColor ()
	private Camera cam;
	public Color[] colors;
	public Color currentColor;
	public bool changeColor = false;
	private float duration = 3f;
	private float t=0f;



	void Awake ()
	{
		instance = this;
		currentLevel = 0;
	}

	// Use this for initialization
	void Start ()
	{
		LoadPlayerProgress ();
		Setup ();
		player = FindObjectOfType<TurnDetection> ().gameObject;
		cam = FindObjectOfType<CameraController> ().gameObject.GetComponent<Camera>();
		cam.clearFlags = CameraClearFlags.SolidColor;
	}


	 void LateUpdate ()
	{
		if (changeColor)
		{
			ChangeColor ();
		}
	}

	void ChangeColor ()
	{	
			t += Time.deltaTime;
			float  abs = t / duration;
			cam.backgroundColor = Color.Lerp (cam.backgroundColor, colors[(int)Mathf.Repeat(currentLevel, colors.Length)], abs);
			if (abs >= duration)
			{
				abs = 0f;
				t = 0f;
				changeColor = false;
			}
		
	}


	public void ResetPlayerProgress()
	{
		PlayerPrefs.SetInt ("highestLevel", 0);
		PlayerPrefs.SetFloat ("fallPosX", 0f);
		PlayerPrefs.SetFloat ("fallPosY", 0.5f);
		PlayerPrefs.SetFloat ("fallPosZ", 0f);
	}

	void LoadPlayerProgress ()
	{

		if (PlayerPrefs.HasKey ("highestLevel")) 
		{
			currentLevel = PlayerPrefs.GetInt("highestLevel");
		}
		if (PlayerPrefs.HasKey ("fallPosX"))
		{
			fallDelta = new Vector3 (PlayerPrefs.GetFloat ("fallPosX"), PlayerPrefs.GetFloat ("fallPosY"), PlayerPrefs.GetFloat ("fallPosZ"));
			Debug.Log (fallDelta.ToString ());
			fallPosition = levels [currentLevel].transform.position + fallDelta;
		}
	}

	public void SavePlayerProgress()
	{
		PlayerPrefs.SetInt ("highestLevel", currentLevel);

	}

	void SaveFallPosition ()
	{
		PlayerPrefs.SetFloat ("fallPosX", fallDelta.x);
		PlayerPrefs.SetFloat ("fallPosY", fallDelta.y);
		PlayerPrefs.SetFloat ("fallPosZ", fallDelta.z);
		Debug.Log (fallDelta.ToString ());
	}



	//Загрузка игры
	void Setup ()
	{
		CreateNextLevel (gameObject.transform);
		Vector3 playerStartPosition = new Vector3 (gameObject.transform.position.x,0.5f, gameObject.transform.position.z);
		Vector3 cameraStartPosition = new Vector3 (0f,8.5f, 0f);
		Instantiate (player, playerStartPosition, Quaternion.identity);
		Instantiate (cameraPrefab, cameraStartPosition, Quaternion.Euler (90f, 0f, 0f));
		Instantiate (placeOfFall, fallPosition, Quaternion.Euler(90f,0f,0f));
	}

	void Update () 
	{
		CheckIsDead ();
		currentLevelText.text = currentLevel.ToString();
	}
		

	//Создание нового уровня
	public void CreateNextLevel(Transform levelPosition)
	{		
		float newPosZ;
		Vector3 newPos;
		newPosZ = levelPosition.transform.position.z+4.5f ;
		newPos = new Vector3 (levelPosition.transform.position.x, 0f, newPosZ);
		GameObject nextLevel = Instantiate (levels[currentLevel], newPos, Quaternion.Euler (90f, 0f, 0f)) as GameObject;
	}

	public void Restart()
	{
		gameOver = false;
		SceneManager.LoadScene (sceneBuildIndex: 0);
	}


	void CheckIsDead ()
	{
		if (player.transform.position.y < 0.45f) 
		{
			fallPosition = player.transform.position;
			player.gameObject.transform.position = fallPosition;
			player.gameObject.SetActive(false);
			
			fallDelta = fallPosition - newStartPos;
			SaveFallPosition ();


			pausePanel.SetActive (true);
		}
	}
}
