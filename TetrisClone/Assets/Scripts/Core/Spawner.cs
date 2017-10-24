using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Shape[] m_allShapes;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		 
	}

	Shape GetRandomShape ()
	{
		int i = Random.Range (0, m_allShapes.Length);
		if (m_allShapes [i]) {
			return m_allShapes [i];
		} 
		else 
		{
			Debug.LogWarning ("Warning! No shapes in spawner!");
			return null;
		}
	}

	public Shape SpawnShape ()
	{
		Shape shape = null;

		shape = Instantiate (GetRandomShape (), transform.position, Quaternion.identity) as Shape;
		if (shape) 
		{
			return shape;
		} else {
			Debug.LogWarning ("Warning! Invalid shape in spawner");
			return null;
		}
	}
}
