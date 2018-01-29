using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour {
    public MonkeyAnimator monkeyAnimator;

    private bool titleStage = true;

    public UnityEvent titleStartEvent;

    public GameObject[] messageSteps;

    public int nextMessageStepIndex = 0;
    private bool textStage = false;

    private AudioSource audioSource;
    private bool finalStage = false;

    private float nextTextTime;

    public UnityEvent finalStageStartEvent;

	// Use this for initialization
	void Start () {
        monkeyAnimator.HoldBanana();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(titleStage && Input.GetButtonDown("Jump"))
        {
            titleStartEvent.Invoke();
            titleStage = false;
            textStage = true;
            nextTextTime = Time.time + 2.0f;
        }
        else if (textStage && (Input.GetButtonDown("Jump") || Time.time > nextTextTime))
        {
            ShowTextStage();
            nextMessageStepIndex++;
            if (nextMessageStepIndex >= messageSteps.Length)
            {
                textStage = false;
                finalStage = true;
                finalStageStartEvent.Invoke();
            }

            nextTextTime = Time.time + 3.0f;
        }
        else if (finalStage && Input.GetButtonDown("Jump"))
        {
            finalStage = false;
            SceneManager.LoadSceneAsync(1);
        }
	}
    public void ShowTextStage()
    {
        messageSteps[nextMessageStepIndex].SetActive(true);

    }
}
