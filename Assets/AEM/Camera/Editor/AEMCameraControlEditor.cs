using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AEMCameraControl), true), CanEditMultipleObjects]
public class AEMCameraControlEditor : AEMEditor
{
    GameObject ObjectPickergo = null;

    int ControlID;

    protected override void AfterDefaultInspector()
    {
        /*Get a Reference to the target script*/
        AEMCameraControl t = target as AEMCameraControl;

        #region buttons
        EditorGUILayout.BeginHorizontal();
        if (UnityGUIHelper.DrawButton("Add MainTarget", "test", EditorStyles.miniButtonLeft,ColorExtension.White, null, 25))
        {
            EditorGUIUtility.ShowObjectPicker<GameObject>(null, true, "", ControlID + 1);
            ObjectPickergo = null;
        }
        if (UnityGUIHelper.DrawButton("Add LookTarget", "test", EditorStyles.miniButtonRight, ColorExtension.White, null, 25))
        {
            EditorGUIUtility.ShowObjectPicker<GameObject>(null, true, "", ControlID + 2);
            ObjectPickergo = null;
        }
        EditorGUILayout.EndHorizontal();
        #endregion

        //Required for object picker
        ControlID = GUIUtility.GetControlID(FocusType.Passive);
        //if user selects anything on the objectpicker window
        if (commandName == "ObjectSelectorClosed")
        {
            if (ObjectPickergo == null)
            {
                //check to see if we user has selected a gameobject
                ObjectPickergo = EditorGUIUtility.GetObjectPickerObject() as GameObject;
                if (ObjectPickergo)
                {
                    //Check to see if gameobject is in scene
                    if (ObjectPickergo.activeInHierarchy)
                    {
                        int currentControlId = EditorGUIUtility.GetObjectPickerControlID();
                        //Match the Control ID to find what the objectpicker it is for
                        if (currentControlId == ControlID + 1)
                        {
                            //MainTarget
                            t.AddMainTarget(ObjectPickergo);
                        }
                        else if (currentControlId == ControlID + 2)
                        {
                            //LookTarget
                            t.AddLookTarget(ObjectPickergo);
                        }
                    }
                    else
                    {
                        Debug.Log("Please Select Object from Scene");
                    }
                }
            }
        }
    }
}
