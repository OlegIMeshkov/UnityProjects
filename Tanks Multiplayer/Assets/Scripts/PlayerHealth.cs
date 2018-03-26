using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;


public class PlayerHealth : NetworkBehaviour {

	float m_currentHealth; 
	//текущее количество жизней
	public float m_maxHealth = 3f;
	//максимальное количество жизней
	public bool m_isDead = false;
	//состояние объекта

	public GameObject m_deathPrefab;
	//переменная для системы частиц, активируемоой при уничтожении

	public RectTransform m_healthBar;
	//переменная для RectTransform HealthBar'а

	// Use this for initialization
	void Start () {
		m_currentHealth = m_maxHealth;
		//устанавливаем текущее значение жизней равным максимальному
		//StartCoroutine("CountDown");

	}

	IEnumerator CountDown ()
	//тестовая корутина для проверки 
	{
		yield return new WaitForSeconds (1f);
		Damage (1f);
		UpdateHealthBar (m_currentHealth);
		yield return new WaitForSeconds (1f);
		Damage (1f);
		UpdateHealthBar (m_currentHealth);
		yield return new WaitForSeconds (1f);
		Damage (1f);
		UpdateHealthBar (m_currentHealth);
	}

	// Update is called once per frame
	void Update () {
	
	}

	void UpdateHealthBar (float value)
	//функция для обновления состояния HealthBar'а
	{
		if (m_healthBar != null) {
			//если объект не пустой
			m_healthBar.sizeDelta = new Vector2 (value / m_maxHealth * 150f, m_healthBar.sizeDelta.y);
			//то изменяем его положение относительно анкоров
		}
	}

	public void Damage (float damage)
	//функция нанесения ущерба
	{
		m_currentHealth -= damage;
		//вычитаем из текущего здоровья урон

		UpdateHealthBar (m_currentHealth);
		//обновляем шкалу
		if (m_currentHealth <= 0 && !m_isDead) {
			//если текущее здоровье  становится меньше или равно нулю и мы пока живы,

			m_isDead = true;
			//то переключаемся в состояние "мертвый"
			Die ();
			//вызываем функцию "смерти", пока еще мы ее не написали
		}
	}

	void Die ()
	//функция, вызываемая при уничтожении танка
	{
		if (m_deathPrefab) {
		//если существует префаб
			GameObject deathFX = Instantiate (m_deathPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity) as GameObject;
			//создаем объект с префабом взрыва
			GameObject.Destroy (deathFX, 3f);
			//уничтожаем объект с префабов взрыва через 3 секунды
		}

		SetActiveState (false);
		//вызываем функцию, выключающую наш танк (её пока нет)

		gameObject.SendMessage ("Disable");
		//отправляем всем компонентам объекта сообщение "Disable". 
		//если хотя бы  на одном компоненте найдется метод с таким названием - он будет выполнен
	}

	void SetActiveState (bool state)
	//функция, переключающая компоненты Collider, Canvas, Renderer в состояние state
	{
		foreach (Collider c in GetComponentsInChildren<Collider>()) 
		{
			c.enabled = state;
		}
		//переключае все коллайдеры в состояние state

		foreach (Canvas c in GetComponentsInChildren<Canvas>()) 
		{
			c.enabled = state;
		}
		//переключаем все элементы UI в состояние state

		foreach (Renderer c in GetComponentsInChildren<Renderer>()) 
		{
			c.enabled = state;
		}
		//переключаем все компоненты отображения в состояние state
	}
}
