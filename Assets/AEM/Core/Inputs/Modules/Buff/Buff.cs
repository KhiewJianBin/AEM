using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Buff is a module that affects only buffable modules
/// </summary>
public abstract class Buff : AEMModule
{
    public abstract string Id { get; }
    public abstract int PriorityQueue { get; }
    public abstract bool IsStackable { get; }
    public abstract bool AllowMultipleInstance { get; }
    [Flags] protected enum StackType{
        ValueStack = 1, //Add together buff values 
        DurationStack = 2} //Add together buff duration
    protected abstract StackType stackType { get; }
    [SerializeField, ReadOnly] protected int StackLevel = 1;

    protected BuffableModule buffablemodule;
    protected Timer buffTimer;
    [ReadOnlyWhenPlaying] public float BuffDuration = Mathf.Infinity;

    // OnStart, find the parent with buffablemodule and tell them to add this buff
    void Start()
    {
        if (FindBuffableModuleAndAddBuff() == false)
        {
            Debug.LogWarning("The Module you are trying to Buff is not a Buffable Module");
        }
        else
        {
            //Create a buff timer
            if (!buffTimer && BuffDuration > 0)
            {
                buffTimer = Timer.CreateNew(ToString() + " Timer");
                buffTimer.transform.parent = transform;
                buffTimer.Setup(0, BuffDuration, Timer.TimerType.StartEnd);
            }
        }
    }
    bool FindBuffableModuleAndAddBuff()
    {
        buffablemodule = transform.parent.GetComponent<BuffableModule>();
        if (buffablemodule == null) return false;
        buffablemodule.AddBuff(this);

        return true;
    }

    //OnDisable, Remove the buff from the module
    void OnDisable()
    {
        if (buffablemodule) buffablemodule.RemoveBuff(this);
        Destroy(gameObject);
    }

    void Update()
    {
        //Check buff timer if timecounter has hit to remove buff
        if(buffTimer)
        {
            if (buffTimer.CounterHit)
            {
                if (buffablemodule) buffablemodule.RemoveBuff(this);
                Destroy(gameObject);
            }
        }
        else
        {
            if (buffablemodule) buffablemodule.RemoveBuff(this);
            Destroy(gameObject);
        }
    }

    public abstract void ApplyBuff(Dictionary<string, object> dataToBuff);
    public abstract void Stack(Buff buff);
}