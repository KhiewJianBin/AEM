/// <summary>
/// SpeedModule is just a number module
/// </summary>
public class SpeedModule : FloatModule
{
    public float Speed => BuffData.ContainsKey("floatvalue") ? (float)BuffData["floatvalue"] : basevalue;

    void Update()
    {
        SpeedModuleLogic();
    }
    void SpeedModuleLogic()
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
