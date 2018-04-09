using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk {

	public Material cubeMaterial;
	public Block[,,] chunkData;
	public GameObject chunk;
	float prob;

	void BuildChunk()
	{
		chunkData = new Block[World.chunkSize,World.chunkSize,World.chunkSize];

		for(int z = 0; z < World.chunkSize; z++)
			for(int y = 0; y < World.chunkSize; y++)
				for(int x = 0; x < World.chunkSize; x++)
				{
					Vector3 pos = new Vector3(x,y,z);
					prob = Random.Range (0, 100);
					if(prob < 20)
						chunkData[x,y,z] = new Block(Block.BlockType.DIRT, pos, 
						                chunk.gameObject, this);
					else if(prob < 40)
						chunkData[x,y,z] = new Block(Block.BlockType.GRASS, pos, 
							chunk.gameObject, this);
					else if(prob < 60)
						chunkData[x,y,z] = new Block(Block.BlockType.STONE, pos, 
							chunk.gameObject, this);
					else if(prob < 80)
						chunkData[x,y,z] = new Block(Block.BlockType.WATER, pos, 
							chunk.gameObject, this);
					else
						chunkData[x,y,z] = new Block(Block.BlockType.AIR, pos, 
						                chunk.gameObject, this);
				}
	}

	public void DrawChunk()
	{
		for(int z = 0; z < World.chunkSize; z++)
			for(int y = 0; y < World.chunkSize; y++)
				for(int x = 0; x < World.chunkSize; x++)
				{
					chunkData[x,y,z].Draw();	
				}
		CombineQuads();
	}

	// Use this for initialization
	public Chunk (Vector3 position, Material c) {
		
		chunk = new GameObject(World.BuildChunkName(position));
		chunk.transform.position = position;
		cubeMaterial = c;
		BuildChunk();
	}
	
	void CombineQuads()
	{
		
		MeshFilter[] meshFilters = chunk.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length) {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            i++;
        }

        
        MeshFilter mf = (MeshFilter) chunk.gameObject.AddComponent(typeof(MeshFilter));
        mf.mesh = new Mesh();

        mf.mesh.CombineMeshes(combine);

		MeshRenderer renderer = chunk.gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		renderer.material = cubeMaterial;

		foreach (Transform quad in chunk.transform) {
     		GameObject.Destroy(quad.gameObject);
 		}

	}

}
