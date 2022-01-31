using System.Collections.Generic;

public class FloatMultiplierBuff : Buff
{
    public override string Id => "Float_Multiplier_Buff";
    public override int PriorityQueue => 2;
    public override bool IsStackable => true;
    public override bool AllowMultipleInstance => !IsStackable && false;
    protected override StackType stackType => StackType.ValueStack | StackType.DurationStack;

    public int BuffMultiplierAmt = 2;

    public override void ApplyBuff(Dictionary<string, object> dataToBuff)
    {
        dataToBuff["floatvalue"] = (float)dataToBuff["floatvalue"] * BuffMultiplierAmt;
    }

    public override void Stack(Buff buff)
    {
        FloatMultiplierBuff floatAddBuff = (FloatMultiplierBuff)buff;

        // Check if the buff allows value stacking
        if ((stackType & StackType.ValueStack) == StackType.ValueStack)
        {
            BuffMultiplierAmt += floatAddBuff.BuffMultiplierAmt;
        }

        // Check if the buff allows duration stacking
        if ((stackType & StackType.DurationStack) == StackType.DurationStack)
        {
            BuffDuration += floatAddBuff.BuffDuration;
            buffTimer.TimerEnd = BuffDuration;
        }
        else //reset timer to apply original buff duration
        {
            buffTimer.Reset();
        }

        StackLevel++;
    }
}




