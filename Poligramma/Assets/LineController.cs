using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LineController : MonoBehaviour {

	LineRenderer lr;
	Vector3[] mass = new Vector3[45];
	public float square = 0f;

	public float baseSquare = 200f;

	public Text baseText;
	public Text currentText;
	public Text resultText;


	// Use this for initialization
	void Start () {
		lr = GetComponent<LineRenderer> ();

		Restart ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void FillMassive ()
	{
		for (int i = 0; i < mass.Length; i++) {
			float randomY = Random.Range (0f, 30f);
			mass[i] = new Vector3 (i, randomY, 0f);
			lr.SetPosition (i,new Vector3(i, randomY, 0f));

		}

	}

	public float CalculateSquare (Vector3[] mass)
	{
		for (int i = 0; i < mass.Length; i++) {
			if (i == 0) {
				//Debug.Log ("мимо");
				continue;
			} else {
				
				if (mass [i].y < mass [i - 1].y) {
					square += (mass [i].y + mass [i - 1].y / 2f);
				//	Debug.Log ("Предыдущий элемент меньше");
				} else if (mass [i].y > mass [i - 1].y) {
					square += (1.5f * mass [i - 1].y - mass [i].y);
				//	Debug.Log ("Предыдущий элемент больше");
				} else if (mass [i].y == mass [i - 1].y) {
					square += mass [i].y;
				//	Debug.Log ("Элементы равны");
				}
			}

			//Debug.Log (mass[i].y.ToString());
		}

		//Debug.Log (square.ToString ());
		return square;
	}

	/*IEnumerator StartCalculate()
	{
		yield return new WaitForSeconds (2f);
		CalculateSquare (mass);
	}
	*/

	void DetectLie (float baseMeasure, float currentMeasure)
	{
		float k = currentMeasure / baseMeasure;

		if (k <= 1.1)
			resultText.text = "ПОФИГ";
		else if (k > 1.1f && k < 1.5f)
			resultText.text = "НОРМА";
		else if (k >= 1.5f)
			resultText.text = "ВРЁТ";
	}

	public void Restart()
	{
		baseText.text = baseSquare.ToString ();
		square = 0;
		FillMassive ();
		Debug.Log (CalculateSquare(mass));
		DetectLie (baseSquare, square);
		currentText.text = square.ToString ();
	}
}
