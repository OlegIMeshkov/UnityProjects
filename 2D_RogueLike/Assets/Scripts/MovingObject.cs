using UnityEngine;
using System.Collections;

// Абстрактный класс позволяет создавать незавершенные классы и члены класса, которые должны быть определены в дочернем классе
public abstract class MovingObject : MonoBehaviour
{
	public float moveTime = 0.1f;           //Время, которое уходит на передвижение.
	public LayerMask blockingLayer;         //Слой, на котором проверяются столкновения.


	private BoxCollider2D boxCollider;      //BoxCollider2D, прикрепленный к объекту.
	private Rigidbody2D rb2D;               //Rigidbody2D, прикрепленный к объекту.
	private float inverseMoveTime;          //Величина, обратная времени, созданная для более эффективных вычислений


	//Защищенные, виртуальные функции могут быть переопределены в дочерних классах.
	protected virtual void Start ()
	{
		//Получаем ссылку на BoxCollider2D
		boxCollider = GetComponent <BoxCollider2D> ();

		//Получаем ссылку на Rigidbody2D
		rb2D = GetComponent <Rigidbody2D> ();

		//Используя такую запись, мы в дальнейшем будем только умножать, а умножение быстрее деления.
		inverseMoveTime = 1f / moveTime;
	}


	//Функция Move возвращает true, если можно двигаться и false, если нет. 
	//Move принимает параметры для направления x, направления y  и RaycastHit2D для проверки столкновения.
	protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
	{
		//Устанавливаем стартовую позицию, с которой будем двигаться на основании transform position.
		Vector2 start = transform.position;

		//Вычисление конечной позиции, основанной на параметрах направления, указанных при вызове функции.
		Vector2 end = start + new Vector2 (xDir, yDir);

		//Выключаем boxCollider, чтобы луч не попал в собственный коллайдер.
		boxCollider.enabled = false;

		//Посылаем линию от старта до конца, проверяя столкновения в слое blockingLayer.
		hit = Physics2D.Linecast (start, end, blockingLayer);

		//Включаем boxCollider после каста
		boxCollider.enabled = true;

		//Проверяем, было ли столкновение
		if(hit.transform == null)
		{
			//Если нет, то включаем Корутину SmoothMovement, передавая в качестве аргумента end.
			StartCoroutine (SmoothMovement (end));

			//Возвращаем true, чтобы передать, что функция выполнена успешно.
			return true;
		}

		//Возвращаем false, чтобы передать, что функция не выполнена.
		return false;
	}


	//Корутина для передвижения предметов, принимающая в качестве параметра координаты точки назначения.
	protected IEnumerator SmoothMovement (Vector3 end)
	{
		//Вычисляем оставшуюся дистанцию для прохождения. 
		//Square magnitude используется вместо magnitude поскольку является более "дешевой" по потреблению ресурсов.
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		//Пока это расстояние больше маленького значения эпсилон:
		while(sqrRemainingDistance > float.Epsilon)
		{
			//Устанавливаем новую позицию, основанную на moveTime
			Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

			//При помощи MovePosition перемещаем объект в новую позицию.
			rb2D.MovePosition (newPostion);

			//Вычисляем оставшуюся позицию.
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;

			//Ждем конца кадра и возвращаемся в начало цикла, пока выполняется условие.
			yield return null;
		}
	}


	//Защищенные, виртуальные функции могут быть переопределены в дочерних классах.
	//AttemptMove имеет дженерик параметр T  для определения типа компонента, с которым наш объект предполагает взаимодействовать, если не сможет передвигаться(игрок для врагов, стены для игрока).
	protected virtual void AttemptMove <T> (int xDir, int yDir)
		where T : Component
	{
		//Hit сохранит данный об объекте, с которым произошло столкновение при вызове Move.
		RaycastHit2D hit;

		//Создаем canMove, в которой храним значение возможности передвижения.
		bool canMove = Move (xDir, yDir, out hit);

		//Проверяем, попал ли Linecast
		if(hit.transform == null)
			//Если ни во что не попали, выходим из функции и не исполняем последующий код.
		
			return;

		//Получаем ссылку на компонент, прикрепленный к объекту, в который попали.
		T hitComponent = hit.transform.GetComponent <T> ();

		//Если canMove - false и hitComponent не пустой, это значит MovingObject заблокирован и ударился обо что-то, с чем можно взаимодействовать.
		if(!canMove && hitComponent != null)

			//Вызываем OnCantMove и передаем hitComponent в качестве параметра.
			OnCantMove (hitComponent);
	}


	//Абстрактный класс позволяет создавать незавершенные классы и члены класса, которые должны быть определены в дочернем классе
	//OnCantMove будет переопределена в дочернем классе
	protected abstract void OnCantMove <T> (T component)
		where T : Component;
}