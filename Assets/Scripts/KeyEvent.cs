using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyEvent : MonoBehaviour {

	public KeyCode keyCode;
	public UnityEvent action;

	public bool fireOnce = true;
	public bool fired = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((fireOnce && !fired || !fireOnce) && Input.GetKeyDown (keyCode)) {
			fired = true;
			action.Invoke ();
		}
	}
}
