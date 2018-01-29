using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumScript : MonoBehaviour {

	public AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider otherCollider)
	{
		if (otherCollider.gameObject.layer == 8)
			audioSource.Play ();
	}
}
