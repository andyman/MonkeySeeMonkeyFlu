using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainLevelController : MonoBehaviour {

	public GermCollector collector;
	public AudioClip losingSound;

	private bool isOver = false;

    public Text timerText;
    private float startTime;
    public float seasonDuration = 60.0f;

    public RuntimeAnimatorController danceAnimatorController;

	// Update is called once per frame
	void Update () 
	{
        if (timerStarted)
        {
            // lose
            if (!isOver && (collector.health <= 0.0f || collector.social <= 0.0f))
            {
                timerText.text = "YOU DEAD, MONKEY!";
                isOver = true;
                SceneManager.LoadSceneAsync(2);
                Debug.Log("Game Over");
            }
            // victory
            else if (!isOver && Time.time >= endTime)
            {
                timerText.text = "FLU SEASON OVER!";
                isOver = true;
                Win();
            }
            else
            {
                timerText.text = Mathf.Ceil(endTime - Time.time).ToString();
            }

            if (won && Input.GetButton("Jump") && Time.time > restartAvailableTime)
            {
                SceneManager.LoadSceneAsync(1);
            }
        }
	}
    public UnityEvent winEvent;
    private bool won = false;
    private float restartAvailableTime;

    public void Win()
    {
        if (won) return;
        won = true;
        // npc monkeys dance
        MonkeyNPCController[] npcMonkeys = GameObject.FindObjectsOfType<MonkeyNPCController>();
        for(int i = 0; i < npcMonkeys.Length; i++)
        {
            npcMonkeys[i].GetComponentInChildren<Animator>().runtimeAnimatorController = danceAnimatorController;
            npcMonkeys[i].enabled = false;
            npcMonkeys[i].GetComponent<Rigidbody>().isKinematic = true;
            npcMonkeys[i].diseaseStuff.SetActive(false);
        }

        // pc dance
        collector.GetComponent<PlayerController>().enabled = false;
        collector.enabled = false;

        collector.GetComponentInChildren<Animator>().runtimeAnimatorController = danceAnimatorController;
        collector.GetComponent<Rigidbody>().isKinematic = true;
        restartAvailableTime = Time.time + 2.0f;
        winEvent.Invoke();
    }
    private float endTime;
    private bool timerStarted = false;
    public UnityEvent startTimerEvent;

    public void StartTimer()
    {
        startTime = Time.time;
        endTime = seasonDuration + startTime;
        timerStarted = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (timerStarted) return;

        // if player
        if (other.gameObject.layer == 8)
        {
            StartTimer();
            startTimerEvent.Invoke();
        }
    }
}
