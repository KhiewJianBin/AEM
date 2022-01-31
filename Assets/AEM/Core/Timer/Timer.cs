using UnityEngine;

public class Timer : MonoBehaviour
{
    public enum TimerType
    {
        Interval, /* For Repeated Events */
        StartEnd /* For Timed/Trigger Events*/
    }

    bool timerStatus;
    bool isCounting;
    bool countingEnded;

    public float CurrentTimer;
    public int RunCounter;
    public TimerType Timertype = TimerType.StartEnd;

    /* Settings Defaults : StartEnd */
    public float TimerStart = 0.0f;
    public float TimerEnd = 3.0f;

    /* Settings Defaults : Interval */
    public float IntervalTime = 3f;
    public float MaxRuns = 1f;

    /* Get Accessors */
    public bool CounterHit
    {
        get { return countingEnded; }
    }

    void Start()
    {
        On();
        Reset();
    }

    void Update()
    {
        if (timerStatus)
        {
            if (isCounting)
            {
                switch (Timertype)
                {
                    case TimerType.StartEnd:
                        CurrentTimer += Time.deltaTime;
                        if (CurrentTimer >= TimerEnd)
                        {
                            RunCounter++;
                            countingEnded = true;
                        }
                        else
                        {
                            countingEnded = false;
                        }
                        break;
                    case TimerType.Interval:
                        CurrentTimer += Time.deltaTime;
                        if (CurrentTimer > IntervalTime)
                        {
                            RunCounter++;
                            countingEnded = true;
                        }
                        else
                        {
                            countingEnded = false;
                        }
                        break;
                }
                if (countingEnded)
                {
                    switch (Timertype)
                    {
                        case TimerType.StartEnd:
                            isCounting = false;
                            break;
                        case TimerType.Interval:
                            if (float.IsInfinity(MaxRuns))
                            {
                                /* Unlimited Runs */
                                Reset();
                            }
                            else if (RunCounter >= MaxRuns)
                            {
                                /* Timer stop counting */
                                isCounting = false;
                                Debug.Log(name + " - Timer has finished counting");
                            }
                            else
                            {
                                /* Timer continue counting */
                                Reset();
                            }
                            break;
                    }
                }
            }
        }
    }

    /* public functions */
    public static Timer CreateNew(string name)
    {
        return new GameObject(name, typeof(Timer)).GetComponent<Timer>();
    }
    public void Setup(float startInterval, float endLimit, TimerType intype)
    {
        switch (intype)
        {
            case TimerType.StartEnd:
                TimerStart = startInterval;
                TimerEnd = endLimit;
                break;
            case TimerType.Interval:
                IntervalTime = startInterval;
                MaxRuns = endLimit;
                break;
        }

        Timertype = intype;
        Reset();
    }
    public void Reset()
    {
        switch (Timertype)
        {
            case TimerType.StartEnd:
                CurrentTimer = TimerStart;
                break;
            case TimerType.Interval:
                CurrentTimer = 0;
                break;
        }

        isCounting = true;
    }
    public bool IsActive()
    {
        return timerStatus;
    }
    public void On()
    {
        timerStatus = true;
    }
    public void Off()
    {
        timerStatus = false;
        Reset();
    }
    public void Pause()
    {
        timerStatus = false;
    }
}
