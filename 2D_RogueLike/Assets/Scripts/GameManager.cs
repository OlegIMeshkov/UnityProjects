using UnityEngine;
using System.Collections;


using System.Collections.Generic;       //Позволяет использовать List<>

public class GameManager : MonoBehaviour
{
	public float levelStartDelay = 2f;                      //Time to wait before starting level, in seconds.
	public float turnDelay = 0.1f;                          //Задержка между ходами игрока.
	public int playerFoodPoints = 100;                      //Starting value for Player food points.
	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	[HideInInspector] public bool playersTurn = true;       //Boolean to check if it's players turn, hidden in inspector but public.


	private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
	private int level = 1;                                  //Current level number, expressed in game as "Day 1".
	private List<Enemy> enemies;                          //Список всех Врагов
	private bool enemiesMoving;                             //Логическая переменная для определения, движется ли объект или нет


	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		//Присваеваем enemies новый список врагов.
		enemies = new List<Enemy>();

		//Get a component reference to the attached BoardManager script
		boardScript = GetComponent<BoardManager>();

		//Call the InitGame function to initialize the first level 
		InitGame();
	}

	//This is called each time a scene is loaded.
	void OnLevelWasLoaded(int index)
	{
		//Add one to our level number.
		level++;
		//Call InitGame to initialize our level.
		InitGame();
	}

	//Initializes the game for each level.
	void InitGame()
	{

		//Очищаем список ото всех объектов, чтобы подготовить его для следующего уровня.
		enemies.Clear();

		//Call the SetupScene function of the BoardManager script, pass it current level number.
		boardScript.SetupScene(level);

	}


	//Update is called every frame.
	void Update()
	{
		//Проверяем, движется ли что-то в игре.
		if(playersTurn || enemiesMoving)

			//Если что-либо движется, то выходим.
			return;

		//Начинаем двигать Врагов.
		StartCoroutine (MoveEnemies ());
	}

	//Вызывайте эту функции для добавления Врага в список.
	public void AddEnemyToList(Enemy script)
	{
		//Добавляем Врага в List.
		enemies.Add(script);
	}


	//GameOver is called when the player reaches 0 food points
	public void GameOver()
	{

		//Enable black background image gameObject.
//		levelImage.SetActive(true);

		//Disable this GameManager.
		enabled = false;
	}

	//Корутина для поочередного передвижения врагов
	IEnumerator MoveEnemies()
	{
		//Пока enemiesMoving равна true, Игрок не может ходить
		enemiesMoving = true;

		//Ждем turnDelay секунд.
		yield return new WaitForSeconds(turnDelay);

		//Если Врагов нет:
		if (enemies.Count == 0) 
		{
			//Ждем turnDelay секунд между ходами.
			yield return new WaitForSeconds(turnDelay);
		}

		//Проходим список врагов в цикле.
		for (int i = 0; i < enemies.Count; i++)
		{
			//Вызываем функцию MoveEnemy у каждого элемента списка.
			enemies[i].MoveEnemy ();

			//Ждем moveTime, пока Враг сделает ход. 
			yield return new WaitForSeconds(enemies[i].moveTime);
		}
		//Когда все Враги сделали ход, переключаем playersTurn на true и Игрок сможет ходить.
		playersTurn = true;

		//Враги сделали ход, устанавливаем enemiesMoving на false.
		enemiesMoving = false;
	}
}