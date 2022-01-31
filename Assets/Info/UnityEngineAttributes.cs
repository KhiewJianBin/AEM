using System;
using UnityEngine;

/// <summary>
/// Apply to classes sub MonoBehaviour.Requires matching class name and file name.
/// </summary>
[AddComponentMenu("Name/Name")]
public class ClassName : MonoBehaviour
{
    /// <summary>
    /// hdr:	If set to true the Color is treated as a HDR color.
    /// maxBrightness:	Maximum allowed HDR color component value when using the HDR Color Picker.
    /// maxExposureValue:	Maximum exposure value allowed in the HDR Color Picker.
    /// minBrightness:	Minimum allowed HDR color component value when using the Color Picker.
    /// minExposureValue:	Minimum exposure value allowed in the HDR Color Picker.
    /// showAlpha:	If false then the alpha bar is hidden in the ColorField and the alpha value is not shown in the Color Picker.
    /// </summary>
    [ColorUsageAttribute(true, true, 1, 2, 1, 2)]
    public Color ColorField;

    [ContextMenu("Do Something")] //Adds a right click menu to component, Used on a function
    void DoSomething()
    {
        Debug.Log("Perform ");
    }

    [ContextMenuItem("Do", "Do Something")]//Adds a right click menu to component, Used on a field
    public float Somefield;

    [Multiline(4)] //Make a string field to have multi-line, only works with string
    public string Somefield2;
}

[CreateAssetMenu]// Make the SciptableObject class an asset to be listed in assets/create submenu. to be easily created and stored in the project as ".asset" files.
public class ClassName2 : MonoBehaviour
{
    [Delayed]//Make a variable not update unless user press enter in inspector. float, int, or string 
    public string Somefield2;
}
[DisallowMultipleComponent]//Prevents MonoBehaviour of same type (or subtype) to be added more than once to a GameObject.
public class ClassName3 : MonoBehaviour
{

}
/// <summary>
/// - Update is only called when something in the scene changed.
///- OnGUI is called when the Game View recieves an Event.
///- OnRenderObject and the other rendering callback functions are called on every repaint of the Scene View or Game View.
/// </summary>
[ExecuteInEditMode] //Allows monobehavour to run in edit mode
public class ClassName4 : MonoBehaviour
{
    [GUITarget(1)]// Label will appear on display 0 and 1 only, use for TV and Wii U dev that has more than one gui display
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 100), "Visible on TV and Wii U GamePad only");
    }
    [Header("Shield Settings")] // adds a header to the field in inspector
    public int shield = 0;
}
[HelpURL("http://example.com/docs/MyComponent.html")]//Provide a custom documentation URL for a class.
public class ClassName5 : MonoBehaviour
{
    [HideInInspector]//Makes a variable not show up in the inspector but be serialized.
    public int varaible = 5;

    //[ImageEffectAllowedInSceneView]//Any Image Effect with this attribute can be rendered into the scene view camera.

    //[ImageEffectOpaque]
    //Any Image Effect with this attribute will be rendered after opaque geometry but before transparent geometry
    //This allows for effects which extensively use the depth buffer(SSAO, etc) to affect only opaque pixels.This attribute can be used to reduce the amount of visual artifacts in a scene with post processing.

    //[ImageEffectTransformsToLDR]
    //When using HDR rendering it can sometime be desirable to switch to LDR rendering during ImageEffect rendering.
    //Using this Attribute on an image effect will cause the destination buffer to be an LDR buffer, and switch the rest of the Image Effect pipeline into LDR mode. It is the responsibility of the Image Effect that this Attribute is associated to ensure that the output is in the LDR range.

    //[PreferBinarySerialization]

    [Range(0,1)]//Attribute used to make a float or int variable in a script be restricted to a specific range.
    public float test;
}

[RequireComponent(typeof(Rigidbody))]/// automatically adds required components as dependencies.
public class PlayerScript : MonoBehaviour
{
    //RuntimeInitializeOnLoadMethodAttribute

    [SerializeField]//force Serialize
    private bool hasHealthPotion = true;

    //[SharedBetweenAnimatorsAttribute]

    [Space(10)]//Use this PropertyAttribute on a field to add some spacing in the Inspector.
    public int shield = 0;

    [TextArea]
    public string MyTextArea;
    //Attribute to make a string be edited with a height-flexible and scrollable text area.

    [Tooltip("Health value between 0 and 100.")]//add tool tip
    public int health = 0;

    //UnityAPICompatibilityVersion
}