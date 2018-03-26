using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
	
	public Sprite dmgSprite;                    //Альтернативный спрайт после атаки стены игроком.
	public int hp = 3;                          //жизни стены

	private SpriteRenderer spriteRenderer;      //декларируем переменную для ссылки SpriteRenderer.


	void Awake ()
	{
		//Получаем ссылку на  SpriteRenderer.
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}


	//DamageWall вызывается, когда игрок бьет стену
	public void DamageWall (int loss)
	{
		 
		//меняем спрайт spriteRenderer на поврежденный.
		spriteRenderer.sprite = dmgSprite;

		//Вычитаем урон из хп
		hp -= loss;

		//Если жизней не осталось:
		if(hp <= 0)
			//Выключаем объект.
			gameObject.SetActive (false);
	}
}