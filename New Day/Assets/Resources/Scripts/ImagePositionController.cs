using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImagePositionController : MonoBehaviour {

	public Image leftImage;
	public Image rightImage;
	public Image centralImage;
	public Image[] images;

	#region Date Control Variables

	public Sprite newImage;

	private int currentDay;
	private bool dragging;

	private string month;
	private int previousMonth;
	private string imageFilePath;
	int anotherDay;
	int direction=0;

	System.DateTime newDayF;

	System.DateTime previousDay;
	System.DateTime nextDay;

	public float scrollSpeed;


	#endregion

	#region Unity Functions
	// Use this for initialization
	void Awake () 
	{
		newDayF = System.DateTime.Now;
		Debug.Log (direction);
		images = new Image[3] {leftImage, centralImage, rightImage};
		previousDay = System.DateTime.Now.AddDays(-1);
		nextDay = System.DateTime.Now.AddDays(1);
		ImageStartPosition ();
		ImageStart ();
		DateTextStart ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		ImageReposition ();


		if (!dragging) 
		{
			SnapImage ();
		}
	
	}

	#endregion


	#region Custom Functions

	void ImageStartPosition () 
	{
		for (int i = 0; i < images.Length; i++) 
		{
			Vector3 currentImagePos = images [i].GetComponent<RectTransform> ().position;
			currentImagePos = new Vector3 (Screen.width * (i - 0.5f), currentImagePos.y, currentImagePos.z);
		}

		Debug.Log (images [1].GetComponent<RectTransform> ().position);

	}    			  // функция расстановки изображения при старте приложения

	void ImageReposition () 
	{
		for (int i = 0; i < images.Length; i++) {
			if (images [i].GetComponent<RectTransform> ().position.x < -1f * Screen.width) { //если изображение вышло за экран слева, то перемещаем его в правую часть
				if (i != 0) {
					images [i].GetComponent<RectTransform> ().position = new Vector3 (images [i - 1].GetComponent<RectTransform> ().position.x + Screen.width, images [i].GetComponent<RectTransform> ().position.y, images [i].GetComponent<RectTransform> ().position.z);
				} else if (i == 0) {
					images [i].GetComponent<RectTransform> ().position = new Vector3 (images [images.Length - 1].GetComponent<RectTransform> ().position.x + Screen.width, images [i].GetComponent<RectTransform> ().position.y, images [i].GetComponent<RectTransform> ().position.z);
				}
				DateChangeLeftSwipe (i);	
			} else if (images [i].GetComponent<RectTransform> ().position.x > 2f * Screen.width) { //если изображение вышло за экран справа, то перемещаем его в левую часть
				if (i != images.Length - 1) {
					images [i].GetComponent<RectTransform> ().position = new Vector3 (images [i + 1].GetComponent<RectTransform> ().position.x - Screen.width, images [i].GetComponent<RectTransform> ().position.y, images [i].GetComponent<RectTransform> ().position.z);
				} else if (i == images.Length - 1) {
					images [i].GetComponent<RectTransform> ().position = new Vector3 (images [0].GetComponent<RectTransform> ().position.x - Screen.width, images [i].GetComponent<RectTransform> ().position.y, images [i].GetComponent<RectTransform> ().position.z);
				}
				DateChangeRightSwipe (i);
			} 
		}

	}    				 // функция перемещения изображения в случае его выхода за экран

	string CheckMonth (int currentMonth)
	{
		switch (currentMonth) {
		case (1):
			month = "Января";
			break;

		case (2):
			month = "Февраля";
			break;

		case (3):
			month = "Марта";
			break;

		case (4):
			month = "Апреля";
			break;

		case (5):
			month = "Мая";
			break;

		case (6):
			month = "Июня";
			break;

		case (7):
			month = "Июля";
			break;

		case (8):
			month = "Августа";
			break;
		case (9):
			month = "Сентября";
			break;
		case (10):
			month = "Октября";
			break;
		case (11):
			month = "Ноября";
			break;

		default:
			month = "Декабря";
			break;


		}  
		return month;

	}     // функция проверяки месяца

	Sprite ImageDownLoad(System.DateTime k)
	{
		imageFilePath = "Sprites/" + k.Day.ToString () + "_" + k.Month.ToString ();

		Debug.Log (imageFilePath);

		newImage = Resources.Load (imageFilePath, typeof(Sprite)) as Sprite;

		return newImage;
	}              //функция загрузки изображения согласно дате k

	void DateTextStart ()
	{
		newDayF = System.DateTime.Now;

		leftImage.gameObject.GetComponentInChildren<Text> ().text = (newDayF.AddDays(-1).Day).ToString () + " " + CheckMonth(previousDay.Month);
		centralImage.gameObject.GetComponentInChildren<Text> ().text = (newDayF.Day).ToString () + " " + CheckMonth(newDayF.Month);
		rightImage.gameObject.GetComponentInChildren<Text> ().text = (newDayF.AddDays(1).Day).ToString () + " " + CheckMonth(nextDay.Month);
		
	} 				   // функция установки начальных дат

	void ImageStart ()
	{
		for (int i = 0; i < images.Length; i++) 
		{
			images [i].sprite = ImageDownLoad(newDayF.AddDays(-1+i));
				//Resources.Load ("Sprites/" + System.DateTime.Now.AddDays(-1+i).Day.ToString() + "_" + System.DateTime.Now.AddDays(-1+i).Month.ToString(), typeof(Sprite)) as Sprite;
		}

	}

	void DateChangeLeftSwipe(int j)
	{
		
		if (direction == 0) 
		{
			newDayF = newDayF.AddDays (2);
			direction = 1;
		} 
		else if (direction == 1)
		{
			newDayF = newDayF.AddDays (1);
		} else 
			{
				newDayF = newDayF.AddDays (3);
				direction = 1;
			}

			images [j].gameObject.GetComponentInChildren<Text> ().text = (newDayF.Day).ToString () + " " + CheckMonth (newDayF.Month);
			images [j].gameObject.GetComponentInChildren<Image> ().sprite = ImageDownLoad (newDayF); 
		Debug.Log (direction);
	} 		 // функция изменения даты при прокрутке налево

	void DateChangeRightSwipe(int j)
	{
		
		if (direction == 0) {
			newDayF = newDayF.AddDays (-2);
			direction = -1;
		} else if (direction == -1) {
			newDayF = newDayF.AddDays (-1);
		} else {
			newDayF = newDayF.AddDays (-3);
			direction = -1;
		}


		images[j].gameObject.GetComponentInChildren<Text> ().text = (newDayF.Day).ToString () + " " + CheckMonth(newDayF.Month);
		images [j].gameObject.GetComponent<Image> ().sprite = ImageDownLoad (newDayF);
	
		Debug.Log (direction);




	} 		// функция изменения даты при прокрутке направо

	public void StartDragging ()
	{
		dragging = true;
	}  		    // вспомогательная функция для определения драга

	public void StopDragging ()
	{
		dragging = false;
	} 		     // вспомогательная функция для определения драга

	public void SnapImage ()
	{
		
			for (int i = 0; i < images.Length; i++) {
				if (images [i].GetComponent<RectTransform> ().position.x > 0f && images [i].GetComponent<RectTransform> ().position.x < Screen.width) {

				images [i].GetComponent<RectTransform> ().position = new Vector3 (Mathf.MoveTowards (images [i].GetComponent<RectTransform> ().position.x, 0.5f* Screen.width, Time.deltaTime* scrollSpeed),
						images [i].GetComponent<RectTransform> ().position.y,
						images [i].GetComponent<RectTransform> ().position.z);
					
				}
			if (images [i].GetComponent<RectTransform> ().position.x > -Screen.width && images [i].GetComponent<RectTransform> ().position.x < 0) {

				images [i].GetComponent<RectTransform> ().position = new Vector3 (Mathf.MoveTowards (images [i].GetComponent<RectTransform> ().position.x, -0.5f* Screen.width, Time.deltaTime* scrollSpeed),
					images [i].GetComponent<RectTransform> ().position.y,
					images [i].GetComponent<RectTransform> ().position.z);

			}

			if (images [i].GetComponent<RectTransform> ().position.x > Screen.width && images [i].GetComponent<RectTransform> ().position.x < 2*Screen.width) {

				images [i].GetComponent<RectTransform> ().position = new Vector3 (Mathf.MoveTowards (images [i].GetComponent<RectTransform> ().position.x, 1.5f* Screen.width, Time.deltaTime* scrollSpeed),
					images [i].GetComponent<RectTransform> ().position.y,
					images [i].GetComponent<RectTransform> ().position.z);

			}

			}
	}   				// функция снэпа изображения




	#endregion

}
