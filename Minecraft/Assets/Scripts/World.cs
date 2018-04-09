using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World : MonoBehaviour {

	public Material textureAtlas;
	public static int columnHeight = 4;
	public static int chunkSize = 16;
	public static int worldSize = 2;
	public static Dictionary<string, Chunk> chunks;
	public static int tilesCount;
	public Text tilesCountText;
	public Text fpsText;
	public Vector3 worldResolution = new Vector3 (256f, 64f, 256f);

	public static string BuildChunkName(Vector3 v)
	{
		return (int)v.x + "_" + 
			         (int)v.y + "_" + 
			         (int)v.z;
	}

	IEnumerator BuildChunkColumn()
	{
		for(int i = 0; i < columnHeight; i++)
		{
			Vector3 chunkPosition = new Vector3(this.transform.position.x, 
												i*chunkSize, 
												this.transform.position.z);
			Chunk c = new Chunk(chunkPosition, textureAtlas);
			c.chunk.transform.parent = this.transform;
			chunks.Add(c.chunk.name, c);
		}

		foreach(KeyValuePair<string, Chunk> c in chunks)
		{
			c.Value.DrawChunk();
			yield return null;
		}
		
	}

	IEnumerator BuildWorld()
	{
		for(int z = 0; z < (int)(worldResolution.z/chunkSize); z++)
			for(int x = 0; x < (int)(worldResolution.x/chunkSize); x++)
				for(int y = 0; y < (int)(worldResolution.y/chunkSize); y++)
				{
					Vector3 chunkPosition = new Vector3(x*chunkSize, y*chunkSize, z*chunkSize);
					Chunk c = new Chunk(chunkPosition, textureAtlas);
					c.chunk.transform.parent = this.transform;
					chunks.Add(c.chunk.name, c);
					
				}

		foreach(KeyValuePair<string, Chunk> c in chunks)
		{
			c.Value.DrawChunk();
			yield return null;
		}
		
	}

	// Use this for initialization
	void Start () {
		
		chunks = new Dictionary<string, Chunk>();
		this.transform.position = Vector3.zero;
		this.transform.rotation = Quaternion.identity;
		StartCoroutine(BuildWorld());
	}
	
	// Update is called once per frame
	void Update () {
		
		float fpsRate = 1 / Time.unscaledDeltaTime;
		fpsText.text = ((int)fpsRate).ToString ();
		tilesCountText.text = tilesCount.ToString ();
	}
}
