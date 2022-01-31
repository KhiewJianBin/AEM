using System;
using UnityEngine;
using UnityEditor;
 
public class EditorGUIExtension
{
    /// <summary>
    /// Creates a filepath textfield with a browse button. Opens the open file panel.
    /// </summary>
    public static string FileLabel(string name, float labelWidth, string path, string extension)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(name, GUILayout.MaxWidth(labelWidth));
        string filepath = EditorGUILayout.TextField(path);
        if (GUILayout.Button("Browse"))
        {
            filepath = EditorUtility.OpenFilePanel(name, path, extension);
        }
        EditorGUILayout.EndHorizontal();
        return filepath;
    }
 
    /// <summary>
    /// Creates a folder path textfield with a browse button. Opens the save folder panel.
    /// </summary>
    public static string FolderLabel(string name, float labelWidth, string path)
    {
        EditorGUILayout.BeginHorizontal();
        string filepath = EditorGUILayout.TextField(name, path);
        if (GUILayout.Button("Browse", GUILayout.MaxWidth(60)))
        {
            filepath = EditorUtility.SaveFolderPanel(name, path, "Folder");
        }
        EditorGUILayout.EndHorizontal();
        return filepath;
    }
 
    /// <summary>
    /// Creates an array foldout like in inspectors. Hand editable ftw!
    /// </summary>
    public static string[] ArrayFoldout(string label, string[] array, ref bool foldout)
    {
        EditorGUILayout.BeginVertical();
        foldout = EditorGUILayout.Foldout(foldout, label);
        string[] newArray = array;
        if (foldout)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();
            int arraySize = EditorGUILayout.IntField("Size", array.Length);
            if (arraySize != array.Length)
                newArray = new string[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                string entry = "";
                if (i < array.Length)
                    entry = array[i];
                newArray[i] = EditorGUILayout.TextField("Element " + i, entry);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        return newArray;
    }
 
    /// <summary>
    /// Creates a toolbar that is filled in from an Enum. Useful for setting tool modes.
    /// </summary>
    public static Enum EnumToolbar(Enum selected)
    {
        string[] toolbar = Enum.GetNames(selected.GetType());
        Array values = Enum.GetValues(selected.GetType());
 
		for (int i=0; i  < toolbar.Length; i++)
		{
			string toolname = toolbar[i];
			toolname = toolname.Replace("_", " ");
			toolbar[i] = toolname;
		}
 
        int selectedIndex = 0;
        while (selectedIndex < values.Length)
        {
            if (selected.ToString() == values.GetValue(selectedIndex).ToString())
            {
                break;
            }
            selectedIndex++;
        }
        selectedIndex = GUILayout.Toolbar(selectedIndex, toolbar);
        return (Enum) values.GetValue(selectedIndex);
    }
 
	/// <summary>
	/// Creates a button that can be toggled. Looks nicer than GUI.toggle
	/// </summary>
	/// <returns>
	/// Toggle state
	/// </returns>
	/// <param name='state'>
	/// If set to <c>true</c> state.
	/// </param>
	/// <param name='label'>
	/// If set to <c>true</c> label.
	/// </param>
	public static bool ToggleButton(bool state, string label)
	{
		BuildStyle();
 
		bool outBool = false;
 
		if (state)
			outBool = GUILayout.Button(label, _toggledStyle);
		else
			outBool = GUILayout.Button(label);
 
		if (outBool)
			return !state;
		else
			return state;
	}

    static GUIStyle _toggledStyle;
	public GUIStyle StyleButtonToggled
	{
		get
		{
			BuildStyle();
			return _toggledStyle;
		}
	}
 
	static GUIStyle _labelTextStyle;
	public static GUIStyle StyleLabelText
	{
		get
		{
			BuildStyle();
			return _labelTextStyle;
		}
	}
 
	static void BuildStyle()
    {
		if (_toggledStyle == null)
		{
			_toggledStyle = new GUIStyle(GUI.skin.button);
			_toggledStyle.normal.background = _toggledStyle.onActive.background;
			_toggledStyle.normal.textColor = _toggledStyle.onActive.textColor;
		}
		if (_labelTextStyle == null)
		{
			_labelTextStyle = new GUIStyle(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).textField);
			_labelTextStyle.normal = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).button.onNormal;
		}
	}
}