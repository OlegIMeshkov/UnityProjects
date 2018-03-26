using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

	public Rigidbody m_bulletPrefab;

	public Transform m_bulletSpawn;

	public int m_shotsPerBurst = 2;
	//количество выстрелов в обойме
	int m_shotsLeft;
	//выстрелов осталось
	bool m_isReloading;
	//статус перезарядки
	public float m_reloadTime = 1f;
	//время перезарядки



	// Use this for initialization
	void Start () 
	{
		m_shotsLeft = m_shotsPerBurst;
		m_isReloading = false;

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	[Command]
	void CmdShoot ()
	{
		Bullet bullet = null;
		//очищаем переменную, хранящую скрипт пули
		bullet = m_bulletPrefab.GetComponent<Bullet> ();
		//заносим в переменную скрипт
		Rigidbody rbody = Instantiate (m_bulletPrefab, m_bulletSpawn.position, m_bulletSpawn.rotation) as Rigidbody;
		//создаём экземпляр префаба пули и берём от него компонент Rigidbody
		if (rbody != null) {
			rbody.velocity = bullet.m_speed * m_bulletSpawn.transform.forward;
			NetworkServer.Spawn (rbody.gameObject);
		}
	}

	public void Shoot () //создадим функцию стрельбы
	{
		if (m_isReloading || m_bulletPrefab == null) {
			return;
		}
		//защитная проверка на статус перезарядки и наличие префаба пули

		CmdShoot ();
		//если rbody не пустая, то задаём ей скорость m_speed в направлении forward
		m_shotsLeft --;
		//уменьшаем количество оставшихся выстрелов
		if (m_shotsLeft <=0) 
		//если числов выстрелов меньше или равно нулю
		{
			StopCoroutine ("Reload");
		//запускаем корутину перезарядки
		}
	}

	IEnumerator Reload()
	{
		m_shotsLeft = m_shotsPerBurst;
		//количество оставшихся выстрелов равно полной обойме
		m_isReloading = true;
		//статус перезарядки = правда
		yield return new WaitForSeconds (m_reloadTime);
		//ждем время перезарядки
		m_isReloading = false;
		//статус преезарядки = ложь
	}
}
