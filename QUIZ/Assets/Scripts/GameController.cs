using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	private DataController dataController;
	private RoundData currentRoundData;
	private QuestionData[] questionPool;

	private bool isRoundActive = false;
	private float timeRemaining;
	private int questionIndex; 
	private int playerScore; 

	private List<GameObject> answerButtonGameObjects = new List<GameObject>(); 
	public SimpleObjectPool answerButtonObjectPool; 

	public Text questionDisplayText; 
	public Transform answerButtonParent; 

	public Text scoreDisplayText;

	public GameObject questionDisplay;
	public GameObject roundEndDisplay;

	public Text timeRemainingDisplayText;

	public Text highScoreDisplay;





	void Start () 
	{
		dataController = FindObjectOfType<DataController> ();
		//ищем DataController и заносим в переменную
		currentRoundData = dataController.GetCurrentRoundData ();
		//в переменную currentRoundData заносим значение функции GetCurrentRoundData
		questionPool = currentRoundData.questions;
		//в массив questionPool заносим вопросы из RoundData
		timeRemaining = currentRoundData.timeLimitInSeconds;
		//в переменную timeRemaining заносим время из RoundData 

		UpdateTimeRemainingDisplay ();


		playerScore = 0;
		questionIndex = 0;
		ShowQuestion ();
		isRoundActive = true;
	}

	private void RemoveAnswerButtons()
	{
		while (answerButtonGameObjects.Count > 0) 
		{
			answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
			answerButtonGameObjects.RemoveAt(0);
		}
	} 

	private void ShowQuestion()
	{
		RemoveAnswerButtons ();
		QuestionData questionData = questionPool [questionIndex];
		questionDisplayText.text = questionData.questionText;

		for (int i = 0; i < questionData.answers.Length; i++) 
		{
			GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
			answerButtonGameObjects.Add(answerButtonGameObject);
			answerButtonGameObject.transform.SetParent(answerButtonParent);

			AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
			answerButton.Setup(questionData.answers[i]);
		}
	}



	public void AnswerButtonClicked(bool isCorrect)
	{
		if (isCorrect) 
		{
			playerScore += currentRoundData.pointsAddedForCorrectAnswer;
			scoreDisplayText.text = "Score: " + playerScore.ToString();
		}

		if (questionPool.Length > questionIndex + 1) {
			questionIndex++;
			ShowQuestion ();
		} else 
		{
			EndRound();
		}

}

	public void EndRound()
	{
		isRoundActive = false;
		dataController.SubmitNewPlayerScore (playerScore);
		highScoreDisplay.text = dataController.GetHighestPlayerScore ().ToString ();

		questionDisplay.SetActive (false);
		roundEndDisplay.SetActive (true);

	}

	public void ReturnToMenu()
	{
		SceneManager.LoadScene ("MenuScreen");
	}

	void Update () 
	{
		if (isRoundActive) 
		{
			timeRemaining -= Time.deltaTime;
			UpdateTimeRemainingDisplay();

			if (timeRemaining <= 0f)
			{
				EndRound();
			}

		}
	}

	private void UpdateTimeRemainingDisplay()
	{
		timeRemainingDisplayText.text = "Time: " + Mathf.Round (timeRemaining).ToString ();
	}
}