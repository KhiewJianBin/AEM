using UnityEngine;

public static class UnityGUIHelper
{
    public static bool DrawButton( string text, string tooltip,GUIStyle style, float? width = null, float? height = null)
    {
        GUIContent content = new GUIContent();
        content.text = text;
        content.tooltip = tooltip;

        GUIStyle buttonstyle = new GUIStyle(style);
        buttonstyle.fontStyle = FontStyle.Normal;
        buttonstyle.normal.textColor = Color.black;
        buttonstyle.wordWrap = true;
        
        if (width.HasValue)
            buttonstyle.fixedWidth = width.Value;
        else
            buttonstyle.stretchHeight = true;

        if (height.HasValue)
            buttonstyle.fixedHeight = height.Value;
        else
            buttonstyle.stretchWidth = true;

        return GUILayout.Button(content, buttonstyle);
    }

    // EditorStyles.miniButtonLeft
    public static bool DrawButton(string text, string tooltip, GUIStyle style, Color color, float? width = null, float? height = null)
    {
        //Keep a ref to original color
        Color colorref = GUI.color;

        //change the color
        GUI.color = color;

        //draw the button
        bool buttonclick = DrawButton(text, tooltip, style, width, height);
        
        //restore the to original color
        GUI.color = colorref;

        return buttonclick;
    }
}
