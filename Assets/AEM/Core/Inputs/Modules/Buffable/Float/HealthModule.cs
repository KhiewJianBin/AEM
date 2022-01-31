public class HealthModule : FloatModule
{
    public float Health => BuffData.ContainsKey("floatvalue") ? (float)BuffData["floatvalue"] : basevalue;

    void Update()
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
