using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public AudioClip pickupSound;
    public GameObject pickupEffect;

    public float health;
    public float social;
    public float healthImmunityTime;

    public Vector3 rotationRate;

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
        // do some rotation
        transform.rotation *= Quaternion.Euler(rotationRate * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        GermCollector collector = other.GetComponent<GermCollector>();
        if (collector == null) return;

        pickupEffect.SetActive(true);
        if (health != 0.0f)
        {
            collector.health += health;
            collector.FlashHealthSlider(false);
        }

        if (social != 0.0f)
        {
            collector.social += social;
            collector.FlashSocialSlider(false);
        }

        if (healthImmunityTime > 0.0f)
        {
            collector.AddImmunity(healthImmunityTime);
        }

        AudioSource.PlayClipAtPoint(pickupSound, other.transform.position);
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, 6.0f);
    }

}
