using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AnyKeyAction : MonoBehaviour {

	public UnityEvent anyKeyPressedEvent;

	public bool pressed = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (pressed)
			return;
		
		if (Input.anyKeyDown || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
		{
			pressed = true;
			anyKeyPressedEvent.Invoke();
		}
	}
}
