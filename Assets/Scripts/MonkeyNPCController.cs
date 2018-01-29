using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyNPCController : MonoBehaviour {
    public Rigidbody rb;

    public Vector2 wanderIntervalRange = new Vector2(6.0f, 12.0f);
    public float wanderRadius = 10.0f;

    public float speed = 3.0f;
    private float nextWanderTime;
    private Vector3 destination;
    private Vector3 initialPosition;

    public GameObject diseaseStuff;

	// Use this for initialization
	void Start () {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        initialPosition = transform.position;
        nextWanderTime = Time.time + Random.value * wanderIntervalRange.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextWanderTime)
        {
            nextWanderTime = Time.time + Random.Range(wanderIntervalRange.x, wanderIntervalRange.y);

            Vector2 circle = Random.insideUnitCircle;
            destination = initialPosition + (new Vector3(circle.x, 0.0f, circle.y)) * wanderRadius;
        }

        // move towards destination
        Vector3 diff = destination - transform.position;
        diff.y = 0.0f;
        float dist = diff.magnitude;

        if (dist > 1f)
        {
            Vector3 dir = diff.normalized;
            rb.velocity = dir * speed;
            rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5.0f);
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime);
        }

    }
}
