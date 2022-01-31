public class ManaModule : FloatModule
{
    public float Mana => BuffData.ContainsKey("floatvalue") ? (float)BuffData["floatvalue"] : basevalue;

    void Update()
    {
        ManaModuleLogic();
    }
    void ManaModuleLogic()
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
