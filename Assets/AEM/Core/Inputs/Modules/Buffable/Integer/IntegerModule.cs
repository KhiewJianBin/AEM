/// <summary>
/// Base Module To Contain Only One int Number
/// </summary>
public abstract class IntegerModule : BuffableModule
{
    public int intvalue = 0;

    public int Value => (int)BuffData["integervalue"];
}