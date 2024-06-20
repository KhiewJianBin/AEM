using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomPropertyDrawer(typeof(ObjectDropdownAttribute))]
public class ObjectSelectorDropdown : PropertyDrawer
{
    List<Object> m_List = new List<Object>();
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Event e = Event.current;
        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            if ((e.type == EventType.DragPerform ||
                e.type == EventType.DragExited ||
                e.type == EventType.DragUpdated ||
                e.type == EventType.Repaint) &&
                position.Contains(e.mousePosition) && e.shift)
            {
                if (DragAndDrop.objectReferences != null)
                {
                    m_List.Clear();
                    foreach (var o in DragAndDrop.objectReferences)
                    {
                        m_List.Add(o);
                        var go = o as GameObject;
                        if (go == null && o is Component)
                        {
                            go = ((Component)o).gameObject;
                            m_List.Add(go);
                        }
                        if (go != null)
                            foreach (var c in go.GetComponents<Component>())
                                if (c != o)
                                    m_List.Add(c);
                    }
                    var fieldInfo = property.GetPropertyReferenceType();
                    if (fieldInfo != null)
                    {
                        var type = fieldInfo.FieldType;
                        for (int i = m_List.Count - 1; i >= 0; i--)
                        {
                            if (m_List[i] == null || !type.IsAssignableFrom(m_List[i].GetType()))
                                m_List.RemoveAt(i);
                        }
                    }
                    var att = attribute as ObjectDropdownFilterAttribute;
                    if (att != null)
                    {
                        var type = att.filterType;
                        for (int i = m_List.Count - 1; i >= 0; i--)
                        {
                            if (!type.IsAssignableFrom(m_List[i].GetType()))
                                m_List.RemoveAt(i);
                        }
                    }
                    if (m_List.Count == 0)
                        DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                    else
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                        if (e.type == EventType.DragPerform)
                        {
                            GenericMenu gm = new GenericMenu();
                            GenericMenu.MenuFunction2 func = (o) => {
                                property.objectReferenceValue = (Object)o;
                                property.serializedObject.ApplyModifiedProperties();
                            };
                            foreach (var item in m_List)
                                gm.AddItem(new GUIContent(item.name + "(" + item.GetType().Name + ")"), false, func, item);
                            gm.ShowAsContext();
                            e.Use();
                        }
                    }
                    m_List.Clear();
                }
            }
            EditorGUI.ObjectField(position, property, label);
        }
        else
            EditorGUI.PropertyField(position, property, label);
    }
}
public static class SerializedPropertyExt
{
    public static FieldInfo GetPropertyReferenceType(this SerializedProperty aProperty)
    {
        var currentType = aProperty.serializedObject.targetObject.GetType();
        FieldInfo fi = null;
        var parts = aProperty.propertyPath.Split('.');
        foreach (string fieldName in parts)
        {
            fi = currentType.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (fi == null)
                return null;
            currentType = fi.FieldType;
        }
        return fi;
    }
}
#endif

public class ObjectDropdownAttribute : PropertyAttribute
{

}
public class ObjectDropdownFilterAttribute : PropertyAttribute
{
    public System.Type filterType;
    public ObjectDropdownFilterAttribute(System.Type aType)
    {
        filterType = aType;
    }
}