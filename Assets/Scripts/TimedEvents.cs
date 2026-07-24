using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : MonoBehaviour
{
    [Tooltip("Time remaining (in seconds) at which this event triggers.")]
    [SerializeField] private float triggerTime;

    [SerializeField] private UnityEvent onTrigger;

    private bool triggered = false;

    private void Update()
    {
        if (triggered)
            return;

        if (CountdownTimer.Instance.TimeRemaining <= triggerTime)
        {
            triggered = true;
            onTrigger.Invoke();
        }
    }

    public void ResetEvent()
    {
        triggered = false;
    }
}