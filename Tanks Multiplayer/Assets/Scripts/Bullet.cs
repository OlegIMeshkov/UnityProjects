using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Bullet : NetworkBehaviour {

	Rigidbody m_rigidbody;

	public int m_speed = 100;

	List<ParticleSystem> m_allParticles;

	public float m_lifetime = 5f;

	Collider m_collider;

	public ParticleSystem m_explosionFX;

	public List<string> m_bounceTags;
	//список тэгов
	public int m_bounces = 2;
	//количество ударов до смерти

	public float m_damage = 1f;
	//урон, ноносимый при попадании пули

	public List<string> m_collisionTags;
	//список тэгов для объектов, в которые могут попадать пули

	// Use this for initialization
	void Start () {
		m_allParticles = GetComponentsInChildren<ParticleSystem> ().ToList ();
		m_rigidbody = GetComponent<Rigidbody> ();
		m_collider = GetComponent<Collider> ();
		StartCoroutine ("SelfDestruct");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionExit (Collision collision)
	//функция, срабатывающая при выходе из столкновения
	{
		if (m_rigidbody.velocity != Vector3.zero)
			//если скорость не нулевая
		{
			transform.rotation = Quaternion.LookRotation (m_rigidbody.velocity);
			//изменить текущую ориентацию пули в направлении вектора скорости
		}
	}

	void Explode ()
	{
		m_collider.enabled = false;
		//выключаем коллайдер
		m_rigidbody.velocity = Vector3.zero;
		//скорость равна нулю
		m_rigidbody.Sleep ();
		//физический движок перестаёт обрабатывать физику этого объекта

		foreach (ParticleSystem ps in m_allParticles) {
			ps.Stop ();
			//оставнавливаем Систему частиц
		}
		if (m_explosionFX != null)//если эффект есть
		 {
			m_explosionFX.transform.parent = null;
			//эффект перестаёт быть дочерним
			m_explosionFX.Play ();
			//эффект проигрывается
		}

		if (isServer) {
			Destroy (gameObject);
			//уничтожаем пулю
			foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer> ()) {
				m.enabled = false;
				//для всех дочерних объектов выключаем меши
			}
		}

	}

	IEnumerator SelfDestruct ()
	{
		yield return new WaitForSeconds (m_lifetime);
		//ждем время жизни
		Explode ();
	}

	void OnCollisionEnter (Collision collision)
	//при входе в соударение
	{
		CheckCollisions (collision);
		//проверяем, столкнулись ли мы с объектом с тэгом из списка collisionTag
		Debug.Log("dd");
		if (m_bounceTags.Contains(collision.gameObject.tag))
		//если тэг у объекта соударения совпадает хоть с одним из списка
		{
			if (m_bounces <= 0) 
			//если количество соударений <=0
			{
				Explode ();
				//тогда взрываемся
			}
			m_bounces--;
			//иначе уменьшаем количество соударений
		}
	}

	void CheckCollisions (Collision collision)
	//функция проверки, с кем пуля сталкивается и что при этом делать
	{
		if (m_collisionTags.Contains(collision.collider.tag)) 
			//если в списке тэгов объектов, разрешенных для столкновения, 
			//есть тэг объекта, с которым мы сталкиваемся сейчас
		{
			Explode ();
			//то взрываемся
			PlayerHealth playerHealth = collision.gameObject.gameObject.GetComponentInChildren<PlayerHealth>();
			//запрашиваем у объекта, с которым сталкиваемся, компонент PlayerHealth
			if (playerHealth != null) {
				//если этот компонент есть
				playerHealth.Damage (m_damage);
				//то наносим объекту-игроку урон
			}
		}
		//если нет - ничего не делаем
	}


}
