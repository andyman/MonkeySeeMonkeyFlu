using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ReloadLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReloadLevel() {
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadSceneAsync(currentSceneIndex);
	}
}
