public class EnergyModule : FloatModule
{
    public float Energy => BuffData.ContainsKey("floatvalue") ? (float)BuffData["floatvalue"] : basevalue;

    void Update()
    {
        EnergyModuleLogic();
    }
    void EnergyModuleLogic()
    {
        ApplyBuffs();
    }
    protected override void ApplyBuffs()
    {
        BuffData["floatvalue"] = basevalue;

        foreach (Buff b in BuffList)
        {
            b.ApplyBuff(BuffData);
        }
    }
}
