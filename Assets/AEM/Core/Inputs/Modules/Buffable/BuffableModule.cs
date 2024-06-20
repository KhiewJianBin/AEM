using System;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Buffable Modules are modules that it self can be modified by other modules(buffs)
/// </summary>
public abstract class BuffableModule : AEMModule
{
    [SerializeField] public List<Buff> BuffList = new List<Buff>();
    public bool HasBuffType<T>()
    {
        for (int i = 0; i <BuffList.Count; i++)
        {
            if(BuffList[i].GetType() == typeof(T))
            {
                return true;
            }
        }
        return false;
    }

    protected Dictionary<string, object> BuffData = new Dictionary<string, object>();
    
    public virtual void AddBuff(Buff newbuff)
    {
        if (BuffList.Count == 0)
        {
            BuffList.Add(newbuff);
        }
        else
        {
            //Check If Buff Is Already Added
            for (int i = 0; i < BuffList.Count; i++)
            {
                if (newbuff.Id == BuffList[i].Id)
                {
                    //Check if is stackable
                    if (newbuff.IsStackable)
                    {
                        BuffList[i].Stack(newbuff);
                        Destroy(newbuff);
                    }
                    else if (newbuff.AllowMultipleInstance == false) 
                    {
                        Destroy(newbuff);
                    }
                    return;
                }

                //Check Buff Queue Sequence and insert into SortedList based on Queue
                else if (newbuff.PriorityQueue < BuffList[i].PriorityQueue)
                {
                    BuffList.Insert(i,newbuff);
                    return;
                }
            }

            //Buff with highest Priority Queue goes to end of the list
            BuffList.Add(newbuff);
        }
    }

    public virtual void RemoveBuff(Buff bufftoremove)
    {
        BuffList.Remove(bufftoremove);
    }
    public virtual void RemoveAllBuffOfType<T>() where T: Buff
    {
        BuffList.RemoveAll(buff => buff.GetType() == typeof(T));
    }

    protected abstract void ApplyBuffs();
}