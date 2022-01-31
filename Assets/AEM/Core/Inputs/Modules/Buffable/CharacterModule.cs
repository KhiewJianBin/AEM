using System;
using UnityEngine;

public class CharacterModule : BuffableModule
{
    [ReadOnly] public HealthModule healthModule;
    [ReadOnly] public ManaModule manaModule;
    [ReadOnly] public EnergyModule energyModule;

    public float Health => ((Func<float>)BuffData["Health"]).Invoke();
    public float Mana => ((Func<float>)BuffData["Mana"]).Invoke();
    public float Energy => ((Func<float>)BuffData["Energy"]).Invoke();

    Func<float> healthFunc;
    Func<float> manaFunc;
    Func<float> energyFunc;

    void Start()
    {
        healthModule = gameObject.AddModuleToGO<HealthModule>();
        manaModule = gameObject.AddModuleToGO<ManaModule>();
        energyModule = gameObject.AddModuleToGO<EnergyModule>();

        
    }
    void Update()
    {
        healthFunc = () => healthModule.Value;
        manaFunc = () => manaModule.Value;
        energyFunc = () => energyModule.Value;
        BuffData["Health"] = healthFunc;
        BuffData["Mana"] = manaFunc;
        BuffData["Energy"] = energyFunc;

        ApplyBuffs();
    }

    public override void AddBuff(Buff newbuff)
    {
        if (newbuff.GetType() == typeof(FloatAddBuff))
        {
            healthModule.gameObject.AddModuleToGO<FloatAddBuff>().GetCopyOf(newbuff);
            manaModule.gameObject.AddModuleToGO<FloatAddBuff>().GetCopyOf(newbuff);
            energyModule.gameObject.AddModuleToGO<FloatAddBuff>().GetCopyOf(newbuff);
        }
        
        base.AddBuff(newbuff);
    }
    protected override void ApplyBuffs()
    {
        if(Input.GetKey(KeyCode.A))
        {
            BuffData["Health"] = manaFunc;
            BuffData["Mana"] = healthFunc;
        }
        foreach (Buff b in BuffList)
        {
            if(b.GetType() == typeof(FloatAddBuff))
            {
                if(healthModule.HasBuffType<FloatAddBuff>() == false)
                    healthModule.gameObject.AddModuleToGO<FloatAddBuff>().GetCopyOf(b);

                if (manaModule.HasBuffType<FloatAddBuff>() == false)
                    healthModule.gameObject.AddModuleToGO<FloatAddBuff>().GetCopyOf(b);

                if (energyModule.HasBuffType<FloatAddBuff>() == false)
                    healthModule.gameObject.AddModuleToGO<FloatAddBuff>().GetCopyOf(b);
            }
            else
            {
                b.ApplyBuff(BuffData);
            }
        }
    }
    public override void RemoveBuff(Buff bufftoremove)
    {
        if (bufftoremove.GetType() == typeof(FloatAddBuff))
        {
            healthModule.RemoveAllBuffOfType<FloatAddBuff>();
            manaModule.RemoveAllBuffOfType<FloatAddBuff>();
            energyModule.RemoveAllBuffOfType<FloatAddBuff>();
        }

        base.RemoveBuff(bufftoremove);
    }
}
