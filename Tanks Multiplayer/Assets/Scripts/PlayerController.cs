using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(PlayerSetup))]

public class PlayerController : NetworkBehaviour {


	PlayerHealth m_pHealth;
	PlayerMotor m_pMotor;
	public PlayerSetup m_pSetup;
	PlayerShoot m_pShoot;

	Vector3 m_originalPosition;
	//переменная для хранения начальной координаты спавна
	NetworkStartPosition[] m_spawnPoints;
	//массив с начальными координатами

	public int m_score;


	public GameObject m_spawnFx;

	// Use this for initialization
	void Start () 
	{
		m_pHealth = GetComponent<PlayerHealth> ();
		m_pMotor = GetComponent<PlayerMotor> ();
		m_pSetup = GetComponent<PlayerSetup> ();
		m_pShoot = GetComponent<PlayerShoot> ();
	
	}

	public override void OnStartLocalPlayer()
	//функция, запускающаяся при старте Локанльного игрока
	{

		m_spawnPoints = GameObject.FindObjectsOfType<NetworkStartPosition> ();
		m_originalPosition = transform.position;
		//запоминаем позицию, в которой игрок в первый раз респаунулся
	}
	


	Vector3 GetInput ()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		return new Vector3 (h, 0f, v);

	}

	void FixedUpdate ()
	{
		if (!isLocalPlayer || m_pHealth.m_isDead) {
			return;
		}
		Vector3 inputDirection = GetInput ();
		m_pMotor.MovePlayer (inputDirection);
	}

	// Update is called once per frame
	void Update () 
	{
		if (!isLocalPlayer || m_pHealth.m_isDead) {
			return;
		}	

		if (Input.GetMouseButtonDown(0)) {
			m_pShoot.Shoot ();
		} //стреляем при нажатии ЛКМ

		Vector3 inputDirection = GetInput ();
		if (inputDirection.sqrMagnitude > 0.25f) {
			m_pMotor.RotateChassis (inputDirection);
		}

		Vector3 turretDir = Utility.GetWorldPointFromScreenPoint (Input.mousePosition, m_pMotor.m_turret.position.y) - m_pMotor.m_turret.position;
		m_pMotor.RotateTurret (turretDir);
	}


	void Disable ()
	{
		StartCoroutine ("RespawnRoutine");
	}

	IEnumerator RespawnRoutine ()
	{
		transform.position = GetRandomSpawnPosition();
		m_pMotor.m_rigidBody.velocity = Vector3.zero;
		yield return new WaitForSeconds (1f);
		m_pHealth.Reset ();

		if (m_spawnFx != null) {
			GameObject spawnFx = Instantiate (m_spawnFx, transform.position + 0.5f * Vector3.up, Quaternion.identity) as GameObject;
			Destroy (spawnFx, 3f);
		}
	}

	Vector3 GetRandomSpawnPosition ()
	//фукнция, выдающая случайную начальную точку спауна
	{
		if (m_spawnPoints != null) {
			if (m_spawnPoints.Length > 0) {
				NetworkStartPosition startPos = m_spawnPoints [Random.Range (0, m_spawnPoints.Length)];
				return startPos.transform.position;
			}
		}
		return Vector3.zero	;
	}
}
