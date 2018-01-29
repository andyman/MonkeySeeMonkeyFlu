using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParameterSet
{
    public Vector2 durationRange = new Vector2(0.5f, 2.0f);

    public string xParameterName = "";
    public Vector2 xValueRange = new Vector2(-1.0f, 1.0f);

    // leave blank if we're just using the x
    public string yParameterName = "";
    public Vector2 yValueRange = new Vector2(-1.0f, 1.0f);

    private int xParameterHash = 0;
    private int yParameterHash = 0;
    private bool usesY = false;

    private float startTime;
    private float nextTime;

    private Vector2 targetValue;
    private Vector2 startValue;
    private Vector2 currentValue;

    public void Start()
    {
        xParameterHash = Animator.StringToHash(xParameterName);
        if (yParameterName != null && yParameterName != "")
        {
            usesY = true;
            yParameterHash = Animator.StringToHash(yParameterName);
        }

    }

    public void UpdateAnimator(Animator animator)
    {

        if (Time.time > nextTime)
        {
            startTime = Time.time;
            nextTime = startTime + Random.Range(durationRange.x, durationRange.y);

            if (usesY)
            {
                Vector2 circle = Random.insideUnitCircle.normalized;
                targetValue.x = Mathf.Lerp(xValueRange.x, xValueRange.y, Mathf.InverseLerp(-1.0f, 1.0f, circle.x));
                targetValue.y = Mathf.Lerp(yValueRange.x, yValueRange.y, Mathf.InverseLerp(-1.0f, 1.0f, circle.y));
                startValue.x = animator.GetFloat(xParameterHash);
                startValue.y = animator.GetFloat(yParameterHash);
            }
            else
            {
                targetValue.x = Random.Range(xValueRange.x, xValueRange.y);
                startValue.x = animator.GetFloat(xParameterHash);
            }
        }

        float lerp = Mathf.InverseLerp(startTime, nextTime, Time.time);

        currentValue = Vector2.Lerp(startValue, targetValue, lerp);
        animator.SetFloat(xParameterHash,currentValue.x);

        if (usesY)
        {
            animator.SetFloat(yParameterHash, currentValue.y);
        }
    }
}

public class MonkeyAnimator : MonoBehaviour {

    // leave blank to get the component
    public Animator animator;

    public ParameterSet[] parameterSets;

    public string speedParameterName = "speed";
    private int speedParameterHash;

    private Vector3 oldPosition;
    private float speed;

    public Vector2 scratchInterval = new Vector2(3.0f, 10.0f);
    private float nextScratchTime;
    public string[] scratchTriggers;

    private bool holdingBanana = false;
    public GameObject[] accessories;

    // Use this for initialization
    void Start () {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        speedParameterHash = Animator.StringToHash(speedParameterName);
        oldPosition = transform.position;
        speed = 0.0f;

        for (int i = 0; i < parameterSets.Length; i++)
        {
            parameterSets[i].Start();
        }

        // set up an accessory if needed
        if (accessories != null && accessories.Length > 0)
        {
            accessories[Random.Range(0, accessories.Length)].SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < parameterSets.Length; i++)
        {
            parameterSets[i].UpdateAnimator(animator);
        }

        // calculate and update the speed param
        Vector3 position = transform.position;
        float distance = Vector3.Distance(oldPosition, position);
        speed = distance / Time.deltaTime;
        animator.SetFloat(speedParameterHash, speed, 0.1f, Time.deltaTime);
        oldPosition = position;

        // scratch
        if (Time.time > nextScratchTime)
        {
            nextScratchTime = Time.time + Random.Range(scratchInterval.x, scratchInterval.y);
            animator.SetTrigger(scratchTriggers[Random.Range(0, scratchTriggers.Length)]);

        }

	}

    public GameObject bananaPhone;

    [ContextMenu("Hold Banana")]
    public void HoldBanana()
    {
        bananaPhone.SetActive(true);
        animator.SetBool("holding_banana", true);
    }

    [ContextMenu("Stop Holding Banana")]
    public void StopHoldingBanana()
    {
        bananaPhone.SetActive(false);
        animator.SetBool("holding_banana", false);
    }

}
