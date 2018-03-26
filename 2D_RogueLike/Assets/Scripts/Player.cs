using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;      //Позволяет использовать SceneManager

//Player наследуется от MovingObject - базового абстрактного класса для движущихся объектов 
public class Player : MovingObject
{
	public float restartLevelDelay = 1f;        //Задержка перед рестартом уровня
	public int pointsPerFood = 10;              //Количество очков, добавляемое при подборе объекта food
	public int pointsPerSoda = 20;              //Количество очков, добавляемое при подборе объекта soda
	public int wallDamage = 1;                  //Урон, который игрок наносит стене


	private Animator animator;                  //Переменная для хранения ссылки на компонент Animator
	private int food;                           //Переменная для хранения очков еды игрока


	//Переопределенная функция Start(), которая была виртуальной в классе MovingObject
	protected override void Start ()
	{
		//Получаем ссылку на компонент Animator
		animator = GetComponent<Animator>();

		//Получаем текущее значение еды из GameManager.instance между уровнями.
		food = GameManager.instance.playerFoodPoints;

		//Вызываем базовую функцию Start() из базового абстрактного класса MovingObject.
		base.Start ();
	}


	//Функция вызывается, когда объект становится неактивным (API Unity)
	private void OnDisable ()
	{
		//Когда объект Player выключен, заносим текущее значение еды в GameManager, чтобы его можно было перезагрузить на следующем уровне.
		GameManager.instance.playerFoodPoints = food;
	}


	private void Update ()
	{
		//Если очередь игрока не наступила, то выходим из функции.
		if(!GameManager.instance.playersTurn) return;

		int horizontal = 0;     //Переменная для хранения направления передвижения по горизонтали.
		int vertical = 0;       //Переменная для хранения направления передвижения по вертикали.


		//Получаем значение ввода по горизонтали и округляем.
		horizontal = (int) (Input.GetAxisRaw ("Horizontal"));

		//Получаем значение ввода по вертикали и округляем.
		vertical = (int) (Input.GetAxisRaw ("Vertical"));

		//Проверяем, если двигаемся по горизонтали, то перестаём двигаться по вертикали.
		if(horizontal != 0)
		{
			vertical = 0;
		}

		//Проверяем, есть ли смещение по вертикали или горизонтали
		if(horizontal != 0 || vertical != 0)
		{
			//Вызываем AttemptMove, передавая в качестве параметра Wall
			//Передаем значения horizontal и vertical в качестве направления движения
			AttemptMove<Wall> (horizontal, vertical);
		}
	}

	//AttemptMove в этом классе переопределяет виртуальную функцию AttemptMove абстрактного класса MovingObject
	//AttemptMove принимает параметр T, который для игрока будет равен Wall, она также принимает значения x и y в качестве направления движения.
	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		//Каждый раз, когда игрок движется, отнимаем единицу еды
		food--;

		//Вызываем базовую функцию AttemptMove абстрактного класса.
		base.AttemptMove <T> (xDir, yDir);

		//Попадание Linecasta дайт нам результат работы функции Move.
		RaycastHit2D hit;

		//Если Move возвращает true, значит игрок мог двигаться на пустую клетку.
		if (Move (xDir, yDir, out hit)) 
		{
			//Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
		}

		//Проверяем, закончилась ли игра.
		CheckIfGameOver ();

		//По окончанию хода устанавливаем playersTurn класса GameManager на false.
		GameManager.instance.playersTurn = false;
	}


		//OnCantMove переопределяет абстрактную функцию OnCantMove класса MovingObject.
		//Она принимает параметр T, который для игрока будет равен Wall, которую игрок можт атаковать и уничтожить.
		protected override void OnCantMove <T> (T component)
		{
			//Создаем переменную hitWall, равную компоненту, переданному в качестве параметра.
			Wall hitWall = component as Wall;

			//Вызываем функцию DamageWall скрипта Wall, висящего на стене, которую мы бьем.
			hitWall.DamageWall (wallDamage);

			//Включаем триггер в аниматореn.
			animator.SetTrigger ("playerChop");
		}


	//OnTriggerEnter2D вызывается в момент, когда игрок сталкивается с другим триггер-коллайдером
	private void OnTriggerEnter2D (Collider2D other)
	{
		//Проверяем, является ли тэг другого объекта равным Exit.
		if(other.tag == "Exit")
		{
			//Вызываем функцию Restart с задержкой.
			Invoke ("Restart", restartLevelDelay);

			//Выключаем объект.
			enabled = false;
		}

		//Проверяем, является ли тэг другого объекта равным Food.
		else if(other.tag == "Food")
		{
			//Добавляем pointsPerFood к общему значению food .
			food += pointsPerFood;

			//Выключаем объект food.
			other.gameObject.SetActive (false);
		}

		//Проверяем, является ли тэг другого объекта равным Soda.
		else if(other.tag == "Soda")
		{
			//Добавляем pointsPerSoda к общему значению food.
			food += pointsPerSoda;


			//Выключаем объект soda.
			other.gameObject.SetActive (false);
		}
	}


	//Restart перезагружает сцену при вызове.
	private void Restart ()
	{
		//Загружаем первую по очереди сцену в списке Build Settings.
		SceneManager.LoadScene (0);
	}


	//LoseFood вызывается, когда враг атакует игрока
	//В качестве параметра принимается значение урона.
	public void LoseFood (int loss)
	{
		//Устанавливаем триггер анимации.
		animator.SetTrigger ("playerHit");

		//Вычитаем урон из общего количества еды.
		food -= loss;

		//Проверяем, закончена игра или нет.
		CheckIfGameOver ();
	}


	//CheckIfGameOver проверяет, достаточно ли очков еды у игрока и если нет, то прекращает игру.
	private void CheckIfGameOver ()
	{
		//Проверяем значение переменной food
		if (food <= 0) 
		{

			//Вызываем функцию GameOver из GameManager.
			GameManager.instance.GameOver ();
		}
	}
}