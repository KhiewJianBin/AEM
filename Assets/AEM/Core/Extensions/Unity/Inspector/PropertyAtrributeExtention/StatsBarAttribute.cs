//BY https://gist.github.com/LotteMakesStuff

using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(StatsBarAttribute))]
public class StatsBarDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty valueMax = property.serializedObject.FindProperty((attribute as StatsBarAttribute).valueMax);

        float lineHight = EditorGUIUtility.singleLineHeight;
        float padding = EditorGUIUtility.standardVerticalSpacing;

        Rect barPosition = new Rect(position.position.x, position.position.y, position.size.x, lineHight);

        float fillPercentage = 0;
        string barLabel = "";
        bool error = false;

        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                if (valueMax == null)
                {
                    error = true;
                    barLabel = "you must provide a valueMax in the StatsBarAttribute!";
                }
                else
                {
                    fillPercentage = property.intValue / (float)valueMax.intValue;
                    barLabel = "[" + property.name + "] " + property.intValue + "/" + valueMax.intValue;
                }
                break;
            case SerializedPropertyType.Float:
                if (valueMax == null)
                {
                    if (property.floatValue > 1)
                    {
                        error = true;
                        barLabel = "property value is over 1, and no max value has been specified!";
                        break;
                    }

                    fillPercentage = property.floatValue / 1;
                    barLabel = "[" + property.name + "] " + (int)property.floatValue + "/" + 1;
                }
                else
                {
                    if (valueMax.propertyType != SerializedPropertyType.Float)
                    {
                        error = true;
                        barLabel = "valueMax's type has to match the type of this property";
                        break;
                    }

                    fillPercentage = property.floatValue / valueMax.floatValue;
                    barLabel = "[" + property.name + "] " + (int)property.floatValue + "/" + valueMax.floatValue;
                }
                break;
            default:
                error = true;
                barLabel = "unsupported type for a stats bar";
                return;
        }

        if (error)
        {
            GUI.Label(barPosition, barLabel);
        }
        else
        {
            Color color = GetColor((attribute as StatsBarAttribute).color);
            Color color2 = Color.white;
            DrawBar(barPosition, Mathf.Clamp01(fillPercentage), barLabel, color, color2);
        }

        EditorGUI.PropertyField(new Rect(position.position.x, position.position.y + lineHight + padding, position.size.x, lineHight), property);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (EditorGUIUtility.singleLineHeight * 2) + EditorGUIUtility.standardVerticalSpacing;
    }

    private void DrawBar(Rect position, float fillPercent, string label, Color barColor, Color labelColor)
    {
        if (Event.current.type != EventType.Repaint)
            return;

        Color savedColor = GUI.color;

        Rect fillRect = new Rect(position.x, position.y, position.width * fillPercent, position.height);

        EditorGUI.DrawRect(position, new Color(0.1f, 0.1f, 0.1f));
        EditorGUI.DrawRect(fillRect, barColor);

        // set alignment and cash the default
        TextAnchor align = GUI.skin.label.alignment;
        GUI.skin.label.alignment = TextAnchor.UpperCenter;

        // set the color and cash the default
        Color c = GUI.contentColor;
        GUI.contentColor = labelColor;

        // calculate the position
        Rect labelRect = new Rect(position.x, position.y - 3, position.width, position.height);

        // draw~
        EditorGUI.DropShadowLabel(labelRect, label);

        // reset color and alignment
        GUI.contentColor = c;
        GUI.skin.label.alignment = align;
    }

    private Color GetColor(StatsBarColor color)
    {
        switch (color)
        {
            case StatsBarColor.Red:
                return new Color32(255, 0, 63, 255);
            case StatsBarColor.Pink:
                return new Color32(255, 152, 203, 255);
            case StatsBarColor.Orange:
                return new Color32(255, 128, 0, 255);
            case StatsBarColor.Yellow:
                return new Color32(255, 211, 0, 255);
            case StatsBarColor.Green:
                return new Color32(102, 255, 0, 255);
            case StatsBarColor.Blue:
                return new Color32(0, 135, 189, 255);
            case StatsBarColor.Indigo:
                return new Color32(75, 0, 130, 255);
            case StatsBarColor.Violet:
                return new Color32(127, 0, 255, 255);
            default:
                return Color.white;
        }
    }
}
#endif

public class StatsBarAttribute : PropertyAttribute
{
    public string valueMax;
    public StatsBarColor color;

    public StatsBarAttribute(string valueMax = null, StatsBarColor color = StatsBarColor.Red)
    {
        this.valueMax = valueMax;
        this.color = color;
    }
}

public enum StatsBarColor
{
    Red,
    Pink,
    Orange,
    Yellow,
    Green,
    Blue,
    Indigo,
    Violet,
    White
}