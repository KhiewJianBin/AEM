using System.Collections.Generic;

public class FloatAddBuff : Buff
{
    public override string Id => "Float_Add_Buff";
    public override int PriorityQueue => 1;
    public override bool IsStackable => true;
    public override bool AllowMultipleInstance => !IsStackable && false;
    protected override StackType stackType => StackType.DurationStack;

    public int BuffAddAmt = 10;

    public override void ApplyBuff(Dictionary<string, object> dataToBuff)
    {
        dataToBuff["floatvalue"] = (float)dataToBuff["floatvalue"] + BuffAddAmt;
    }

    public override void Stack(Buff buff)
    {
        FloatAddBuff FloatAddBuff = (FloatAddBuff)buff;

        // Check if the buff allows value stacking
        if ((stackType & StackType.ValueStack) == StackType.ValueStack)
        {
            BuffAddAmt += FloatAddBuff.BuffAddAmt;
        }

        // Check if the buff allows duration stacking
        if ((stackType & StackType.DurationStack) == StackType.DurationStack)
        {
            BuffDuration += FloatAddBuff.BuffDuration;
            buffTimer.TimerEnd = BuffDuration;
        }
        else //reset timer to apply original buff duration
        {
            buffTimer.Reset();
        }

        StackLevel++;
    }
}




