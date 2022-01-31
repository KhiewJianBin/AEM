using System;
using UnityEngine;

public class PropertyAtrributeExtention_Example : MonoBehaviour
{
    [Space]
    [ReadOnly] public float ReadOnlyFloat = 0;
    [ReadOnlyWhenPlaying] public float ReadOnlyWhenPlayingFloat = 0;

    [Space]
    public float statMax = 100;
    [StatsBar("statMax", StatsBarColor.Red)] public float health = 25;
    [StatsBar("statMax", StatsBarColor.Blue)] public float mana = 50;
    [StatsBar("statMax", StatsBarColor.Yellow)] public float strength = 90;

    [Space]
    [Vector3Extra] public Vector3 position;
    [Flags] public enum Fruit
    {
        None = 0,
        Apple = 1,
        Orange = 2,
        Banana = 4
    }

    //Working - recommanded to use this
    [EnumFlag] public Fruit fruit = Fruit.Apple | Fruit.Orange;

    //Has issues when all flag is set, will prints -1, cant fix due to how c# enum works
    [EnumFlag2] public Fruit fruit2 = Fruit.Apple | Fruit.Orange;

    [MinMaxSlider(0f, 10f)] public MinMax elevation = new MinMax(4f, 8f);

    //(color,function name,object to be passed)
    [Highlight(HighlightColor.Green, "ValidateHighlight", 1)] public int highlight;
    //this function must return type bool
    bool ValidateHighlight(int a) {return a == 1;}

    void Update()
    {
        print(fruit2);
        //print(fruit2);
    }
}
