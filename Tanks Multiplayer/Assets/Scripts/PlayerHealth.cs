using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;


public class PlayerHealth : NetworkBehaviour {

	[SyncVar(hook = "UpdateHealthBar")]
	float m_currentHealth; 
	//текущее количество жизней
	public float m_maxHealth = 3f;
	//максимальное количество жизней
	[SyncVar]
	public bool m_isDead = false;
	//состояние объекта

	public GameObject m_deathPrefab;
	//переменная для системы частиц, активируемоой при уничтожении

	public RectTransform m_healthBar;
	//переменная для RectTransform HealthBar'а

	public PlayerController m_lastAttacker;

	// Use this for initialization
	void Start () {
		Reset ();
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

	public void Damage (float damage, PlayerController pc = null)
	//функция нанесения ущерба
	{
		if (!isServer) {
			return;
		}

		if (pc != null && pc!=this.GetComponent<PlayerController>()) {
			m_lastAttacker = pc;
		}
		m_currentHealth -= damage;
		//вычитаем из текущего здоровья урон


		if (m_currentHealth <= 0 && !m_isDead) {

			if (m_lastAttacker != null) {
				m_lastAttacker.m_score++;
				m_lastAttacker = null;

			}
			//если текущее здоровье  становится меньше или равно нулю и мы пока живы,

			GameManager.Instance.UpdateScoreboard ();
			m_isDead = true;
			//то переключаемся в состояние "мертвый"
			RpcDie ();
			//вызываем функцию "смерти", пока еще мы ее не написали
		}
	}


	[ClientRpc]
	void RpcDie ()
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

	public void Reset ()
	//функция, сбрасывающая все настройки танка в начальное состояние
	{
		m_currentHealth = m_maxHealth;

		SetActiveState (true);

		m_isDead = false;

	}
}
