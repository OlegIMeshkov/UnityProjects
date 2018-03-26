using UnityEngine;
using System.Collections;
using UnityEditor; //позволяет работать с окном Редактора
using System.IO; //позволяет работать с файлами

public class GameDataEditor : EditorWindow {

	//создадим объект GameData, с которым будем работать в инспекторе
	public GameData gameData;


	//создадим переменную с путем к файлу .json из текущей папки
	private string gameDataProjectFilePath = "/StreamingAssets/data.json";

	//создадим элемент меню
	[MenuItem("Window/Game Data Editor")]
	//создадим статическую функцию
	static void Init ()
	{
		//создаем окно редактора
		GameDataEditor window = (GameDataEditor)EditorWindow.GetWindow (typeof(GameDataEditor));
		//выводим окно
		window.Show ();

	}
	//работаем над интерфейсом редактора. EditorWindow.OnGUI используется
	//как раз для рисования управляющих элементов окна
	void OnGUI()
	{
		//Проверяем, загружены ли хоть какие-то данные
		if (gameData != null) 
		{
			//если да, то создадим сериализованный объект, чтобы выводить данные
			SerializedObject serializedObject = new SerializedObject(this);
			//ищем в этом объекте нужное свойство
			SerializedProperty serializedProperty = serializedObject.FindProperty ("gameData");
			//создаем поле для свойства
			EditorGUILayout.PropertyField(serializedProperty, true);
			//применим изменения в свойстве
			serializedObject.ApplyModifiedProperties ();
			//все описанное выше позволяет нам редактировать данные в окне редактора
			//теперь создадим кнопки и проверяем нажатие
			if (GUILayout.Button("Save data"))
			{
				SaveGameData ();
			}
		}
		//после закрытыя if(gameData !=null)
		//делаем проверку на нажатие кнопки загрузки данных

		if (GUILayout.Button ("Load data"))
		{
			LoadGameData ();
		}
	}

	//создадим функцию загрузки данных

	private void LoadGameData ()
	{
		//поместим в отдельную переменную путь до файла из папки проекта
		string filePath = Application.dataPath + gameDataProjectFilePath;

		//проверим наличие файла по этому адресу

		if (File.Exists (filePath)) {
			//если файл существует, читаем инфу и помещаем в переменную
			string dataAsJson = File.ReadAllText (filePath);
			//десериализуем dataAsJson в объект GameData с названием gameData
			gameData = JsonUtility.FromJson<GameData> (dataAsJson);
		}
		//в случае отсутствия файла по данному адресу...
		else 
		{
			//создадим новый экземпляр GameData и поместим его в gameData
			gameData = new GameData ();
		}
	}

	//создадим функцию загрузки данных

	private void SaveGameData()
	{
		//сериализуем объект gameData и помещаем его в переменную
		string dataAsJason = JsonUtility.ToJson(gameData);
		//ВНИМАНИЕ!!! Переменная dataAsJson имеет такое же название, как 
		// и в LoadGameData() . Однако надо понимать, что это локальные
		// переменные функций и они не зависят друг от друга.
		// мы сделали им одинаковые имена лишь для того, чтобы показать,
		// что они выполняют одинаковую функцию

		//теперь данный файл надо куда-то сохранить.
		//укажем путь
		string filePath = Application.dataPath + gameDataProjectFilePath;
		//по текущему адресу создадим новый файл/перезапишем существующий
		File.WriteAllText (filePath, dataAsJason);
	}
}
