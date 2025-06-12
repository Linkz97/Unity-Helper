using UnityEngine;
using UnityEngine.Events;

public class SimpleTimerEvent : MonoBehaviour
{
    [SerializeField] private bool isLoop = false;
    [SerializeField] private float intervals = 2f;
    [SerializeField] private UnityEvent events = null;
    
    private float currentTimer = 0f;
    private bool startTimer = false;

    private void OnEnable()
    {
        currentTimer = 0f;
        ResumeTimer();
    }

    public void PauseTimer()
    {
        SetTimerActivation(false);
    }

    public void ResumeTimer()
    {
        SetTimerActivation(true);
    }

    public void SetTimer(float time)
    {
        this.intervals = time;
    }

    private void SetTimerActivation(bool value)
    {
        startTimer = value;
    }
    
    private void Update()
    {
        if (startTimer)
        {

            if (currentTimer >= intervals)
            {
                RunEvent();
            }
            else
            {
                currentTimer += Time.deltaTime;
            }
        }
    }

    private void RunEvent()
    {
        if (isLoop)
        {
            currentTimer = 0f;
        }
        else
        {
            PauseTimer();
        }
        
        events?.Invoke();
    }
}
