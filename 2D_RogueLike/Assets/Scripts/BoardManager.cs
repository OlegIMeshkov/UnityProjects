using UnityEngine;
using System.Collections;
using System; //позволяет использовать Serializable
using System.Collections.Generic;// List
using Random = UnityEngine.Random;
//чтобы не перепутать System.Random и UnityEngine.Random

public class BoardManager : MonoBehaviour {


	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}


	public int columns = 8; // столбцы поля
	public int rows =8; // строки поля
	//определяем сколько мин и макс стен
	//будет на поле
	public Count wallCount = new Count (5,9);
	//определяем сколько мин и макс еды
	//будет на поле
	public Count foodCount = new Count (1,5);
	//ссылки на префабы
	public GameObject exit;
	public GameObject[] wallTiles;
	public GameObject[] floorTiles;
	public GameObject[] foodTiles;
	public GameObject[] enemyTiles;
	public GameObject[] outerWallTiles;

	//родительский объект для хранения объектов поля
	private Transform boardHolder;
	//лист координат сетки поля
	private List <Vector3> gridPositions = new List<Vector3>();

	void InitialiseList()
	{
		gridPositions.Clear ();
		for (int x = 1; x < columns-1; x++) {
			for (int y = 0; y < rows-1; y++) {
				gridPositions.Add (new Vector3 (x, y, 0f));
			}
			
		}
	}


	void BoardSetup()
	{
		boardHolder = new GameObject ("Board").transform;

		for (int x = -1; x < columns + 1; x++) {
			for (int y = -1; y < rows + 1; y++) {
				GameObject toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];
				if (x == -1 || x == columns || y == -1 || y == rows)
					toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];

				GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;

				instance.transform.SetParent (boardHolder);
			}
		}
	}




	Vector3 RandomPosition()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}

	void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range (minimum,maximum+1);
		for (int i = 0; i < objectCount; i++) {
			Vector3 randomPosition = RandomPosition ();
			GameObject tileChoice = tileArray [Random.Range (0, tileArray.Length)];
			Instantiate (tileChoice,randomPosition,Quaternion.identity);
		}
	}


	public void SetupScene (int level)
	{
		BoardSetup ();
		InitialiseList ();
		LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
		LayoutObjectAtRandom (foodTiles, foodCount.minimum,foodCount.maximum);
		int enemyCount = (int)Mathf.Log (level, 2f);
		LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
		Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);

	}
}
