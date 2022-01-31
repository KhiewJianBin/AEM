using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Vector3ExtraAttribute))]
public class Vector3ExtraDrawer : PropertyDrawer
{
    protected static float rand()
    {
        return Random.Range(-100, 100);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        if (UnityGUIHelper.DrawButton("0", "Zero", EditorStyles.miniButtonLeft, ColorExtension.Gray, 35))
        {
            property.vector3Value = Vector3.zero;
        }
        if (UnityGUIHelper.DrawButton("1", "One", EditorStyles.miniButtonMid, ColorExtension.Gray, 35))
        {
            property.vector3Value = Vector3.one;
        }
        if (UnityGUIHelper.DrawButton("R", "Random", EditorStyles.miniButtonRight, ColorExtension.Gray, 35))
        {
            property.vector3Value = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
        }
        EditorGUILayout.EndHorizontal();
    }
}
#endif

public class Vector3ExtraAttribute : PropertyAttribute { }