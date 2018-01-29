// INSTRUCTIONS
// Use this class to call events after a delay.
// Useful for making something happen without setting up an animation.
//

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using System.Collections;

/** Convenience class that simply invokes the events after a delay. */
public class DelayedEvent : MonoBehaviour {

	/** Initial delay (in seconds) before invoking the events */
	public float delay = 1.0f;

	/** Events to invoke after the delay */
	[SerializeField]
	public UnityEvent events;
	
	/** Start is called on the frame when a script is enabled just
	 * before any of the Update methods is called the first time.
	 * This starts the RunEventsAfterDelay coroutine.
	 */
	void Start () {	
	}

	void OnEnable() {
		StartCoroutine(RunEventsAfterDelay(delay));
	}
	/** Coroutine that waits for the delay and then invokes the events */
	IEnumerator RunEventsAfterDelay(float delay)
	{
		// delay
		yield return new WaitForSeconds(delay);

		// run the events
		events.Invoke ();
	}
}
