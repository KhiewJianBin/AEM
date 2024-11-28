#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(AEMEditor))]
[CanEditMultipleObjects]
public class AEMEditorTemplate : AEMEditor
{
    /*Decalre SerializedVarible*/
    SerializedProperty a;

    protected override void OnEnable()
    {
        /*Settings*/
        ShowScriptHeader = false;
        Lock = true;

        /*Excluded any property from drawing in insepctor*/
        ExcludeProperty.Add("variableName");

        /*Find the property in inspector*/
        a = serializedObject.FindProperty("a");

        /*Modify the value*/
        a.objectReferenceValue = null;

        /*Calls the base function, [must be at the bottom]*/
        base.OnEnable();
    }

    protected override void BeforeDefaultInspector()
    {
        /*Things to draw before default inspector*/
    }

    protected override void AfterDefaultInspector()
    {
        /*Things to draw after default inspector*/

        /*Get a Reference to the target script*/
        AEMEditor t = target as AEMEditor;
    }
}
#endif 