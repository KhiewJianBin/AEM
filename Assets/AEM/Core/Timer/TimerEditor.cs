#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Timer))]
[CanEditMultipleObjects]
public class TimerEditor : AEMEditor
{
    protected override void BeforeDefaultInspector()
    {
        ExcludeProperty.Add("TimerStart");
        ExcludeProperty.Add("TimerEnd");
        ExcludeProperty.Add("IntervalTime");
        ExcludeProperty.Add("MaxRuns");
    }

    protected override void AfterDefaultInspector()
    {
        Timer t = target as Timer;

        EditorGUI.indentLevel++;
        if (t.Timertype == Timer.TimerType.StartEnd)
        {
            t.TimerStart = EditorGUILayout.FloatField("Timer Start", t.TimerStart);
            t.TimerEnd = EditorGUILayout.FloatField("Timer End", t.TimerEnd);
        }
        else
        {
            t.IntervalTime = EditorGUILayout.FloatField("Interval Time", t.IntervalTime);
            t.MaxRuns = EditorGUILayout.FloatField("MaxRuns", t.MaxRuns);
        }
        EditorGUI.indentLevel--;

        if (GUILayout.Button("Reset"))
            t.Reset();
    }
}

#endif