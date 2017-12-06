using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class GameController : MonoBehaviour {


	#region Variables
	public static GameController instance;
	[SerializeField]
	GameObject pointCollectible;
	[SerializeField]
	GameObject timeCollectible;
	GameObject player;


	public float score;
	public float scoreSize = 100f;
	public Text scoreText;
	public Text timeText;

	public float timeRemaining = 30f;
	public float timeSize = 5f;
	int timeRemainingInt;

	public GameObject gameOverPanel;

	int highScore;
	public Text highScoreText;
	#endregion

	#region Unity Methods
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		StartCoroutine(Loading ());

	}


	void Start ()
	{
		//StartSpawn ();
		gameOverPanel.SetActive (false);
		Time.timeScale = 1f;
		LoadHighScore ();
		UpdateHighScoreUI ();
	}

	// Update is called once per frame
	void Update () {
		if (timeRemaining<=0f || player.transform.position.y <-0.5f) {
			GameOver ();
		}

		PausePanel ();
	}

	void LateUpdate ()
	{
		UpdateUI ();

	}

	#endregion

	#region UpdateUI Methods
	void UpdateUI ()
	{
		timeRemaining -= Time.deltaTime;
		timeRemainingInt = (int)timeRemaining;
		timeText.text = "Time Remaining: " + timeRemainingInt.ToString ();
		scoreText.text = "Score: " + score.ToString ();
	}

	public void UpdatePointUI ()
	{
		score += scoreSize;
		scoreText.text = "Score: " + score.ToString ();
	}


	public void UpdateTimeUI ()
	{
		timeRemaining += timeSize;
		timeRemainingInt = (int)timeRemaining;
		timeText.text = "Time Remaining: " + timeRemainingInt.ToString ();
	}

	void UpdateHighScoreUI ()
	{
		highScoreText.text = "Highscore: " + highScore.ToString ();
	}

	#endregion

	#region SceneManagement Methods
	void GameOver ()
	{
		SaveHighScore ();
		Time.timeScale = 0f;
		gameOverPanel.SetActive (true);
	}

	public void StartGame ()
	{
		SceneManager.LoadScene ("Main");
	}

	public void ToMenu ()
	{
		SceneManager.LoadScene ("Menu");
	}

	public void QuitGame ()
	{
		Application.Quit ();
	}

	public void Restart ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void Continue ()
	{
		gameOverPanel.SetActive (false);
		Time.timeScale = 1f;
	}

	void PausePanel ()
	{
		if (Input.GetButtonDown("Quit")) 
		{
			if (gameOverPanel.activeSelf) {
				Continue ();
			} else {
				gameOverPanel.SetActive (true);
				Time.timeScale = 0f;
			}
		}
	}

	#endregion

	#region Spawn Objects Methods and Coroutines

	/*void StartSpawn ()
	{
		for (int i = 0; i < 5; i++) {
			Vector3 startSpawnPosition = new Vector3 (Random.Range (-50f, 50f), 1f, Random.Range (-50f, 50f)); 
			Instantiate (timeCollectible, startSpawnPosition, Quaternion.identity);
		}

		for (int i = 0; i < 5; i++) {
			Vector3 startSpawnPosition = new Vector3 (Random.Range (-50f, 50f), 1f, Random.Range (-50f, 50f)); 
			Instantiate (pointCollectible, startSpawnPosition, Quaternion.identity);
		}

	}
	*/

	public IEnumerator SpawnTimeCollectible ()
	{
		yield return new WaitForSeconds (1f);
		Vector3 spawnPosition = new Vector3 (Random.Range (-50f, 50f), 1f, Random.Range (-50f, 50f)); 
		Instantiate (timeCollectible, spawnPosition, Quaternion.identity);

	}

	public IEnumerator SpawnPointCollectible ()
	{
		yield return new WaitForSeconds (1f);
		Vector3 spawnPosition = new Vector3 (Random.Range (-50f, 50f), 1f, Random.Range (-50f, 50f)); 
		Instantiate (pointCollectible, spawnPosition, Quaternion.identity);

	}
	#endregion

	#region HighScore Load/Save Methods

	void SaveHighScore()
	{
		if (score > highScore) {
			highScore = (int)score;
			Debug.Log (highScore.ToString());
			PlayerPrefs.SetInt ("HighScore", highScore);
		}

	}

	void LoadHighScore ()
	{
		if (PlayerPrefs.HasKey ("HighScore")) {
			highScore = PlayerPrefs.GetInt ("HighScore");
		} else
			highScore = 0;
	}

	#endregion


	IEnumerator Loading()
	{
		var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "gcandplayer"));
		yield return bundleLoadRequest;


		var myLoadedAssetBundle = bundleLoadRequest.assetBundle;

		var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync ("Player");
		yield return assetLoadRequest;

		GameObject prefab = assetLoadRequest.asset as GameObject;

		player = prefab;
			Instantiate (prefab, new Vector3 (0f, 0.5f, 0f), Quaternion.identity) ;

		myLoadedAssetBundle.Unload(false);



		bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "collectibles"));
		yield return bundleLoadRequest;


		myLoadedAssetBundle = bundleLoadRequest.assetBundle;

		assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync ("TimeCollectible");
		yield return assetLoadRequest;

		prefab = assetLoadRequest.asset as GameObject;

		timeCollectible = prefab;
		for (int i = 0; i < 5; i++) {
			yield return new WaitForEndOfFrame ();
			Instantiate (prefab, new Vector3 (Random.Range(-50f, 50f), 0.5f, Random.Range(-50f, 50f)), Quaternion.identity) ;
		}



		assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync ("PointCollectible");
		yield return assetLoadRequest;

		prefab = assetLoadRequest.asset as GameObject;

		pointCollectible = prefab;
		for (int i = 0; i < 5; i++) {
			yield return new WaitForEndOfFrame ();
			Instantiate (prefab, new Vector3 (Random.Range(-50f, 50f), 0.5f,Random.Range(-50f, 50f)), Quaternion.identity) ;
		}

		myLoadedAssetBundle.Unload(false);
	}


}
