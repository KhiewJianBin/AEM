using AEM.Managers;
using UnityEngine;

public class TimerExample : GameManager
{
    //Static Instance - So that Everyone can Access it
    public static TimerExample instance = null;

    [ReadOnly] public Timer StartEndTimer;
    [ReadOnly] public Timer IntervalTimer;

    public override void Awake()
    {
        base.Awake();

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public override void Start()
    {
        base.Start();

        StartEndTimer = Timer.CreateNew("Timer");
        StartEndTimer.transform.parent = transform;
        StartEndTimer.Setup(0, 1f, Timer.TimerType.StartEnd);

        IntervalTimer = Timer.CreateNew("Timer");
        IntervalTimer.transform.parent = transform;
        IntervalTimer.Setup(1f, Mathf.Infinity, Timer.TimerType.Interval);
    }
     
    void Update()
    {
        if (StartEndTimer.CounterHit)
        {
            print("start end hit");
            StartEndTimer.Reset();
        }
        if (IntervalTimer.CounterHit)
        {
            print("interval hit");
        }
    }
}