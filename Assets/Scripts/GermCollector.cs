using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GermCollector : MonoBehaviour {

    public List<ParticleCollisionEvent> collisionEvents;

    public AudioSource audioSource;
    public AudioClip[] healthSounds;
    public AudioClip[] socialSounds;

    public float health = 100.0f;
    public float maxHealth = 100.0f;
    public float diseaseAdjustment = -1.0f;

    public float social = 50.0f;
    public float socialMax = 100.0f;
    public float socialAdjustment = 10.0f;
    public float socialDrainRate = -5.0f;

    public Slider healthSlider;
    public Slider socialSlider;

    public Image healthFill;
    public Image socialFill;

    private Color healthColor;
    private Color socialColor;

    public Text immunityLabel;

    private float immuneUntilTime = 0.0f;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        healthColor = healthFill.color;
        socialColor = socialFill.color;
    }

    // Update is called once per frame
    void Update () {
        if (Time.time > immuneUntilTime)
        {
            social += socialDrainRate * Time.deltaTime;
            immunityLabel.enabled = false;
        }
        else
        {
            immunityLabel.enabled = true;
            Color c = immunityLabel.color;
            c.a = Mathf.Clamp01((immuneUntilTime - Time.time) / 5.0f);
            immunityLabel.color = c;

        }
        social = Mathf.Min(social, socialMax);
        health = Mathf.Min(health, maxHealth);

        healthSlider.value = health / maxHealth;
        socialSlider.value = social / socialMax;

        healthColor.a = Mathf.Lerp(healthColor.a, 0.2f, Time.deltaTime *3.0f);
        socialColor.a = Mathf.Lerp(socialColor.a, 0.2f, Time.deltaTime * 1.0f);

        healthFill.color = healthColor;
        socialFill.color = socialColor;

	}

    void OnParticleCollision(GameObject other)
    {
        int layer = other.layer;

        if (layer == 10 && Time.time > immuneUntilTime)
        {
            health += diseaseAdjustment;
            FlashHealthSlider();
        }
        else if (layer == 11)
        {
            social += socialAdjustment;
            FlashSocialSlider();
        }

    }

    public void FlashHealthSlider(bool playSound = true)
    {
        healthColor.a = 1.0f;
        healthFill.color = healthColor;

        audioSource.Stop();
        audioSource.pitch = Random.Range(0.8f, 1.2f);

        audioSource.clip = healthSounds[Random.Range(0, healthSounds.Length)];
        audioSource.Play();
    }

    public void FlashSocialSlider(bool playSound = true)
    {
        socialColor.a = 1.0f;
        socialFill.color = socialColor;

        audioSource.Stop();
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.clip = socialSounds[Random.Range(0, socialSounds.Length)];
        audioSource.Play();
    }

    public void AddImmunity(float immunityTime)
    {
        immuneUntilTime = Time.time + immunityTime;
    }
}
