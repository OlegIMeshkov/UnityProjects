using UnityEngine;
using System.Collections;

//Enemy наследует от базового абстрактного класса MovingObject.
public class Enemy : MovingObject
{
	public int playerDamage;                            //Урон, который наносит противник

	private Animator animator;                          //Переменная для хранения ссылки на компонент Animator
	private Transform target;                           //Transform для указания движения противника
	private bool skipMove;                              //Логическая переменная для определения необходимости пропустить ход.


	//Переопределеяем функцию Start 
 

		//Получаем ссылку на компонент Animator.
		animator = GetComponent<Animator> ();

		//Ищем объект с тэгом Player и получаем ссылку на его Transform.
		target = GameObject.FindGameObjectWithTag ("Player").transform;

		//Вызываем функцию Start() базового класса MovingObject.
		base.Start ();
	}


	//Переопределяем функцию AttemptMove, чтобы включить необходимый функционал по пропуску хода.
	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		//Проверяе skipMove, если true, то устанавлием его в false и пропускаем ход.
		if(skipMove)
		{
			skipMove = false;
			return;

		}

		//Вызываем AttemptMove базового класса MovingObject.
		base.AttemptMove <T> (xDir, yDir);

		//Теперь враг сделал ход и должен пропустить следующий ход.
		skipMove = true;
	}


	//MoveEnemy вызывается GameManger'ом каждый ход, чтобы сказать каждому Врагу, чтобы он попытался сделать ход в сторону Игрока.
	public void MoveEnemy ()
	{
		//Задаем начальные значения направления движения равным 0.
		int xDir = 0;
		int yDir = 0;

		//Если разница в позициях по вертикали практически равна 0:
		if(Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)

			//Если y-координата target'а (Игрока) больше y-координаты данного Врага, то нужно двигаться вверх, в противном случае вниз.
			yDir = target.position.y > transform.position.y ? 1 : -1;

		//Если разница в позициях по горизонтали практически равна 0:
		else
			//Если x-координата target'а (Игрока) больше x-координаты данного Врага, то нужно двигаться вправо, в противном случае влево.
			xDir = target.position.x > transform.position.x ? 1 : -1;

		//Вызываем AttemptMove и передаем в качестве параметра Player, т.к. Враг движется и ожидает встречи именно с Игроком.
		AttemptMove <Player> (xDir, yDir);
	}


	//OnCantMove вызывается, если Враг пытается переместиться на позицию, на которой стоит Игрок.
	protected override void OnCantMove <T> (T component)
	{
		//Объявляем hitPlayer и устанавливаем его равным указанному компоненту.
		Player hitPlayer = component as Player;

		//Вызываем LoseFood, передавая playerDamage.
		hitPlayer.LoseFood (playerDamage);

		//Проигрываем анимацию по триггеру.
		animator.SetTrigger ("enemyAttack");

	}
}