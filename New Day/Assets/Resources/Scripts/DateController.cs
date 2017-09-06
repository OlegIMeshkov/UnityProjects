using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class DateController : MonoBehaviour {

	public Text dateText;
	public Image currentImage;
	public Sprite newImage;


	private string month;
	private string imageFilePath;


	// Use this for initialization
	void Awake () {

		Debug.Log (Screen.width);
		
		switch (System.DateTime.Now.Month) {
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
		}  //проверяем месяц

		ImageDownLoad ();


		Debug.Log (System.DateTime.Now.Day.ToString() + " " + month);
		dateText.text = System.DateTime.Now.Day.ToString() + " " + month;

	}


	public void ImageDownLoad()
	{
		imageFilePath = "Sprites/" + System.DateTime.Now.Day.ToString() + "_" + System.DateTime.Now.Month.ToString();

		Debug.Log (imageFilePath);

		newImage = Resources.Load (imageFilePath, typeof(Sprite)) as Sprite ;

		Debug.Log (newImage);

		//currentImage.sprite = newImage;
	}
	
}
