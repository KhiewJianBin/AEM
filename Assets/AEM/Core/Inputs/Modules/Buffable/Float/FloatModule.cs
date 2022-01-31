/// <summary>
/// Base Module To Contain Only One int Number
/// </summary>
public abstract class FloatModule : BuffableModule
{
    public float basevalue = 0;

    public float Value => BuffData.ContainsKey("floatvalue") ? (float)BuffData["floatvalue"] : basevalue;
}