using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWaveSpawner : MonoBehaviour
{
	public GameObject[] randomObjects;
	public Vector3 spawnValues;
	public int objectCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (SpawnRandomObjects ());
	}

	IEnumerator SpawnRandomObjects ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < objectCount; i++) {
				Vector3 spawnPosition = new Vector3 (
					                       Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, 
					                       Random.Range (-spawnValues.z, spawnValues.z));
				Quaternion spawnRotation = Quaternion.identity;
				GameObject obj = randomObjects[Random.Range(0, randomObjects.Length)];
				Debug.Log (obj);
				Instantiate (obj, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
