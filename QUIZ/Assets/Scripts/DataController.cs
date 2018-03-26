using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;


public class DataController : MonoBehaviour {

	private RoundData[] allRoundData;

	private PlayerProgress playerProgress;
	private string gameDataFileName = "data.json";


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		LoadGameData ();
		LoadPlayerProgress ();

		SceneManager.LoadScene ("MenuScreen");
	}
	
	public RoundData GetCurrentRoundData()
	{
		return allRoundData [0];

	}

	public void SubmitNewPlayerScore (int newScore)
	{
		if (newScore > playerProgress.highestScore) 
		{
			playerProgress.highestScore = newScore;
			SavePlayerProgress ();
		}

	}

	public int GetHighestPlayerScore ()
	{
		return playerProgress.highestScore;
	}


	private void LoadPlayerProgress ()
	{
		playerProgress = new PlayerProgress ();

		if (PlayerPrefs.HasKey ("highestScore")) {
			playerProgress.highestScore = PlayerPrefs.GetInt ("highestScore");
		}
	}

	private void SavePlayerProgress ()
	{
		PlayerPrefs.SetInt ("highestScore", playerProgress.highestScore);

	}

	private void LoadGameData ()
	{
		string filePath = Path.Combine (Application.streamingAssetsPath, gameDataFileName);

		if (File.Exists (filePath)) { // если файл существует
			string dataAsJson = File.ReadAllText (filePath); //собираем из файла всю информацию и помещаем в строчную переменную
			//теперь мы хотим эту информацию десериализовать
			// то есть преобразовать из текста в объект GameData
			GameData loadedData = JsonUtility.FromJson<GameData> (dataAsJson);
			//теперь у нас есть GameData объект и мы хотим сделать его доступным в игре
			allRoundData = loadedData.allRoundData;
			/*смысл тут вот какой. Переменная allRoundData есть и в GameData и в 
			DataController. Данной строкой мы говорим, что переменная, используемая
			в DataController - это переменная из GameData.
			Поскольку мы присваиваем переменной из DataController другое значение,
			мы можем поменять ее спецификатор-тип доступа на приватный */
		}
		//теперь рассматриваем вариант, когда файла нет
		else
		{
			//выведем в консоль сообщение в виде ошибки
			Debug.LogError ("Cannot load game data!");
		}

	}


}


