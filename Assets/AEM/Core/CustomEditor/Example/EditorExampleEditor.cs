#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(EditorExample))]
[CanEditMultipleObjects]

public class EditorExampleEditor : AEMEditor
{
    AnimBool ShowExtraFields;
    AnimBool ShowTextFields;
    AnimBool ShowValueFields;
    AnimBool ShowVectorFields;
    AnimBool ShowSliderFields;
    AnimBool ShowSelectionFields;
    AnimBool ShowToggleFields;

    float f;

    protected override void OnEnable()
    {
        //ExcludeProperty.Add(serializedObject.FindProperty("m_Script"));
        ShowTextFields = new AnimBool(true);
        ShowTextFields.valueChanged.AddListener(Repaint);

        ShowExtraFields = new AnimBool(true);
        ShowExtraFields.valueChanged.AddListener(Repaint);

        ShowValueFields = new AnimBool(false);
        ShowValueFields.valueChanged.AddListener(Repaint);

        ShowVectorFields = new AnimBool(false);
        ShowVectorFields.valueChanged.AddListener(Repaint);

        ShowSliderFields = new AnimBool(false);
        ShowSliderFields.valueChanged.AddListener(Repaint);

        ShowSelectionFields = new AnimBool(false);
        ShowSelectionFields.valueChanged.AddListener(Repaint);

        ShowToggleFields = new AnimBool(false);
        ShowSelectionFields.valueChanged.AddListener(Repaint);
    }

    protected override void AfterDefaultInspector()
    {
        //var t = (target as EditorExample);

        //ShowTextFields
        ShowTextFields.target = EditorGUILayout.Toggle("Show Text", ShowTextFields.target);
        if (EditorGUILayout.BeginFadeGroup(ShowTextFields.faded))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("LabelField");
            EditorGUILayout.SelectableLabel("SelectableLabel");
            EditorGUILayout.TextField("TextField");
            EditorGUILayout.TextArea("TextArea");
            EditorGUILayout.PasswordField("PasswordField");
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        ShowValueFields.target = EditorGUILayout.Toggle("Show Value", ShowValueFields.target);
        if (EditorGUILayout.BeginFadeGroup(ShowValueFields.faded))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.IntField("IntField", 1);
            EditorGUILayout.FloatField("FloatField", 1.0f);
            EditorGUILayout.LongField("LongField",1);
            EditorGUILayout.DoubleField("DoubleField", 1.0f);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        //ShowVectorFields
        ShowVectorFields.target = EditorGUILayout.Toggle("Show Vector", ShowVectorFields.target);
        if (EditorGUILayout.BeginFadeGroup(ShowVectorFields.faded))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.Vector2Field("Vector2Field", Vector2.zero);
            EditorGUILayout.Vector3Field("Vector3Field", Vector3.zero);
            EditorGUILayout.Vector4Field("Vector4Field", Vector4.zero);
            EditorGUILayout.BoundsField("BoundsField", new Bounds());
            EditorGUILayout.RectField(new Rect());
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        //ShowSliderFields
        ShowSliderFields.target = EditorGUILayout.Toggle("Show Sliders", ShowSliderFields.target);
        if (EditorGUILayout.BeginFadeGroup(ShowSliderFields.faded))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.Slider("FloatSlider", 0, 0, 100);
            EditorGUILayout.IntSlider("IntSlider",5, 0, 10);

            f = EditorGUILayout.Knob(new Vector2(100, 100), f, 0, 10, "Knob", Color.white, Color.red, true);

            float a = 0;
            float b = 5;
            EditorGUILayout.MinMaxSlider(ref a, ref b, 0, 100);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        //ShowSelectionFields
        ShowSelectionFields.target = EditorGUILayout.Toggle("Show Selection", ShowSelectionFields.target);
        if (EditorGUILayout.BeginFadeGroup(ShowSelectionFields.faded))
        {
            EditorGUILayout.LayerField(0);
            EditorGUILayout.TagField("TagField");
            EditorGUILayout.ColorField("ColorField", Color.red);

            GUIContent[] a = { new GUIContent("1"), new GUIContent("2"), new GUIContent("3") };
            EditorGUILayout.Popup(0, a);

            string[] s = { "string1", "string2", "string3" };
            EditorGUILayout.MaskField(0, s);

            int[] c = { 1 };
            EditorGUILayout.IntPopup(1, a, c);
        }
        EditorGUILayout.EndFadeGroup();

        //ShowToggleFields
        ShowToggleFields.target = EditorGUILayout.BeginToggleGroup("ToggleGroup", ShowToggleFields.target);
        EditorGUILayout.Toggle("Toggle", true);
        EditorGUILayout.ToggleLeft("ToggleLeft", true);
        EditorGUILayout.EndToggleGroup();

        ShowExtraFields.target = EditorGUILayout.Toggle("Show Extras", ShowExtraFields.target);
        if (EditorGUILayout.BeginFadeGroup(ShowExtraFields.faded))
        {
            EditorGUI.indentLevel++;
            //EditorGUILayout.EnumMaskField()
            //EditorGUILayout.EnumMaskPopup;
            //EditorGUILayout.EnumPopup;
            //EditorGUILayout.ObjectField();
            EditorGUILayout.Foldout(true, "Foldout");
            EditorGUILayout.HelpBox("Help Box", MessageType.Info);
            EditorGUILayout.CurveField("CurveField", new AnimationCurve());
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        EditorGUILayout.BeginHorizontal();
        EditorGUI.indentLevel++;
        EditorGUILayout.FloatField(1);
        EditorGUILayout.FloatField(2);
        EditorGUILayout.FloatField(3);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginScrollView(Vector2.zero);
        EditorGUI.indentLevel++;
        EditorGUILayout.FloatField(1);
        EditorGUILayout.FloatField(2);
        EditorGUILayout.FloatField(3);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndScrollView();

        EditorGUILayout.BeginVertical();
        EditorGUI.indentLevel++;
        EditorGUILayout.FloatField(1);
        EditorGUILayout.FloatField(2);
        EditorGUILayout.FloatField(3);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }

    public void OnSceneGUI()
    {
        var t = (target as EditorExample);

        EditorGUI.BeginChangeCheck();
        //Vector3 pos = Handles.PositionHandle(t.lookAtPointss, Quaternion.identity);
        //if (EditorGUI.EndChangeCheck())
        //{
        //    Undo.RecordObject(target, "Move point");
         //   t.lookAtPointss = pos;
        //}
    }
}
#endif