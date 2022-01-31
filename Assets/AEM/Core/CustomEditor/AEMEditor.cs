#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class AEMEditor : Editor
{
    /// <summary>
    /// Whether or not to show the script field header
    /// </summary>
    public bool ShowScriptHeader = true;

    /// <summary>
    /// Used to allow or disallow the editing the inspector 
    /// A global static var
    /// </summary>
    public static bool Lock = false;

    /// <summary>
    /// List of properties to be excluded from drawing in the inspector
    /// </summary>
    protected List<string> ExcludeProperty = new List<string>();

    /// <summary>
    /// Unity Event CommandName
    /// </summary>
    protected string commandName;

    /// <summary>
    /// Unity function that draws the inspector
    /// </summary>
    public override void OnInspectorGUI()
    {
        //the current event type the gui is running right now
        commandName = Event.current.commandName;

        //Call the Update to all serialized object that appears in inspector
        serializedObject.Update();

        EditorGUILayout.Separator();

        //Locks the Inspector for editing
#region Lock
        Lock = EditorGUILayout.ToggleLeft("Lock", Lock);
        GUI.enabled = !Lock;
#endregion

        //Any GUI to draw here before default inspector
        BeforeDefaultInspector();

        //Main GUI drawing to excluded any property added into ExcludeProperty<string>
        DrawPropertiesExcluding(serializedObject, ExcludeProperty.ToArray());

        //Any GUI to draw here after default inspector
        AfterDefaultInspector();

        //Apply property modifications. Unity docs not explained clearly
        serializedObject.ApplyModifiedProperties();
    }
    protected virtual void OnEnable()
    {
        //Optional To be included and modified by child
        //ShowScriptHeader = false;
        //Lock = true;

        if (!ShowScriptHeader)
        {
            ExcludeProperty.Add("m_script");
        }
    }
    protected virtual void BeforeDefaultInspector() { }
    protected virtual void AfterDefaultInspector() { }

    public void ShowList(SerializedProperty list, bool showListSize = true)
    {
        EditorGUILayout.PropertyField(list);
        EditorGUI.indentLevel += 1;
        if (list.isExpanded)
        {
            if (showListSize)
            {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
            }
            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            }
        }
        EditorGUI.indentLevel -= 1;
    }
    public void ShowDisabledList(SerializedProperty list, bool showListSize = true, bool disabled = true)
    {
        EditorGUI.BeginDisabledGroup(disabled);
        ShowList(list, showListSize);
        EditorGUI.EndDisabledGroup();
    }
    public void DrawScriptField()
    {
        SerializedProperty script = serializedObject.FindProperty("m_Script");
        EditorGUILayout.ObjectField("Script", script.objectReferenceValue, typeof(MonoScript), false);
    }
    public void DrawFakeSeparateLine()
    {
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    }
}
#endif