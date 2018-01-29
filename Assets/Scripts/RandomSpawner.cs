using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
	public GameObject randomObject;
	public Vector3 spawnValues;
	// requires X and Y to each have a least one non-zero value
	public int noSpawnXStart = 0;
	public int noSpawnXEnd = 0;
	public int noSpawnZStart = 0;
	public int noSpawnZEnd = 0;
	public int objectCount;

	// Use this for initialization
	void Start ()
	{
		SpawnRandomObjects ();
	}

	void SpawnRandomObjects ()
	{
		for (int i = 0; i < objectCount; i++) {
			float x = Random.Range (-spawnValues.x, spawnValues.x);
			float z = Random.Range (-spawnValues.z, spawnValues.z);
			if (x > noSpawnXStart && x < noSpawnXEnd && z > noSpawnZStart && z < noSpawnZEnd) {
				continue;
			}

			Vector3 spawnPosition = new Vector3 (x, spawnValues.y, z);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (randomObject, spawnPosition, spawnRotation);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
