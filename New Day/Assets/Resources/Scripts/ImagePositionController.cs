using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VoxelBusters.NativePlugins;

public class ImagePositionController : MonoBehaviour {
	
	float sizing;




	#region Image Control Variables

	public Image leftImage;
	public Image rightImage;
	public Image centralImage;
	public Image[] images;

	#endregion

	#region Date Control Variables

	private string month;
	private string imageFilePath;
	int direction=0;

	System.DateTime newDayF;
	System.DateTime previousDay;
	System.DateTime nextDay;

	#endregion

	#region Swipe Control Variables

	private bool dragging;
	public float scrollSpeed;
	bool swipeLeft = false;
	bool swipeRight = false;
	public float currentTime;
	public float swipeTime = 0.2f;

	#endregion

	#region Unity Functions
	// Use this for initialization
	void Awake () 
	{



		sizing = 540f / Screen.width;
		newDayF = System.DateTime.Now;
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
		if (!swipeLeft || !swipeRight)
		ImageReposition ();


		if (!dragging && !swipeLeft && !swipeRight) 
		{
			SnapImage ();
		}

		if (swipeLeft) {
			SwipeLeft ();
		} else if (swipeRight) {
			SwipeRight ();
		}
	
	}

	#endregion


	#region Custom Functions

	void ImageStartPosition () 
	{

			for (int i = 0; i < images.Length; i++) 
			{
				RectTransform currentImageRect = images [i].GetComponent<RectTransform> ();
				currentImageRect.localPosition = new Vector3 (Screen.width * (i -1)* sizing, currentImageRect.localPosition.y, currentImageRect.localPosition.z);
			}
	}

	    			  // функция расстановки изображения при старте приложения

	void ImageReposition () 
	{
		for (int i = 0; i < images.Length; i++) {
			
			RectTransform rectTransform = images [i].GetComponent<RectTransform> ();
			if (rectTransform.position.x < -1f * Screen.width) { //если изображение вышло за экран слева, то перемещаем его в правую часть
				if (i != 0) {
					rectTransform.position = new Vector3 (images [i - 1].GetComponent<RectTransform> ().position.x + Screen.width, rectTransform.position.y, rectTransform.position.z);
				} else if (i == 0) {
					rectTransform.position = new Vector3 (images [images.Length - 1].GetComponent<RectTransform> ().position.x + Screen.width, rectTransform.position.y, rectTransform.position.z);
				}
				DateChangeLeftSwipe (i);	
			} else if (rectTransform.position.x > 2f * Screen.width) { //если изображение вышло за экран справа, то перемещаем его в левую часть
				if (i != images.Length - 1) {
					rectTransform.position = new Vector3 (images [i + 1].GetComponent<RectTransform> ().position.x - Screen.width, rectTransform.position.y, rectTransform.position.z);
				} else if (i == images.Length - 1) {
					rectTransform.position = new Vector3 (images [0].GetComponent<RectTransform> ().position.x - Screen.width, rectTransform.position.y, rectTransform.position.z);
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
		Sprite newImage = Resources.Load (imageFilePath, typeof(Sprite)) as Sprite;
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
			RectTransform rectTransform = images [i].GetComponent<RectTransform> ();
			if (rectTransform.position.x > 0f && rectTransform.position.x < Screen.width) {

				rectTransform.position = new Vector3 (Mathf.MoveTowards (rectTransform.position.x, 0.5f* Screen.width, Time.deltaTime* scrollSpeed),
					rectTransform.position.y,
					rectTransform.position.z);
					
				}
			if (rectTransform.position.x > -Screen.width && rectTransform.position.x < 0) {

				rectTransform.position = new Vector3 (Mathf.MoveTowards (rectTransform.position.x, -0.5f* Screen.width, Time.deltaTime* scrollSpeed),
					rectTransform.position.y,
					rectTransform.position.z);

			}

			if (rectTransform.position.x > Screen.width && rectTransform.position.x < 2*Screen.width) {

				rectTransform.position = new Vector3 (Mathf.MoveTowards (rectTransform.position.x, 1.5f* Screen.width, Time.deltaTime* scrollSpeed),
					rectTransform.position.y,
					rectTransform.position.z);

			}

			}
	}   				// функция снэпа изображения
		

	void SwipeLeft()
	{
			for (int i = 0; i < images.Length; i++) {
			RectTransform rectTransform = images [i].GetComponent<RectTransform> ();
			rectTransform.gameObject.transform.Translate (Vector3.right*Time.deltaTime* scrollSpeed/sizing);
		}

		if ((Time.time - currentTime) > swipeTime)
		{
			swipeLeft = false;
		}
	}						    // функция перемещения изображений влево

	void SwipeRight ()
	{
		for (int i = 0; i < images.Length; i++) {
			RectTransform rectTransform = images [i].GetComponent<RectTransform> ();
			rectTransform.gameObject.transform.Translate (Vector3.left*Time.deltaTime* scrollSpeed/sizing);
		}

		if ((Time.time - currentTime) > swipeTime)
		{
			swipeRight = false;
		}
	}   					  // функция перемещения изображений вправо

	public void SwiperLeft ()
	{
		currentTime = Time.time;
		swipeLeft = true;
	} 			   // вспомогательная функция определения нажатия кнопки "ВЛЕВО"

	public void SwiperRight ()	
	{
		currentTime = Time.time;
		swipeRight = true;
	}  			  // вспомогательная функция определения нажатия кнопки "ВПРАВО"

	#endregion


	public void ShareImageAtPathUsingShareSheet ()
	{
		// Create share sheet
		ShareSheet _shareSheet     = new ShareSheet();    




		_shareSheet.AttachScreenShot ();

		// Show composer
		NPBinding.UI.SetPopoverPointAtLastTouchPosition();
		NPBinding.Sharing.ShowView(_shareSheet, FinishedSharing);

	}

			void FinishedSharing (eShareResult _result)
			{
				Debug.Log("Finished sharing");

				Debug.Log("Share Result = " + _result);
			}
}
