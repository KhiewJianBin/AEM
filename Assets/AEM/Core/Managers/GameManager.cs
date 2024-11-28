using UnityEngine;

/// <summary>
/// Base GameManager Class to handle game events
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Game Controls

    /// <summary>
    /// All the different type of controls that the game has - Represented as strings
    /// </summary>
    public static string[] GameControls;

    /// <summary>
    /// Default Keyboard Controls - Display as KeyCodes
    /// </summary>
    public static KeyCode[] DefaultKeyboardKeys;

    /// <summary>
    /// Default Alternate Keyboard Controls - Display as KeyCodes
    /// </summary>
    public static KeyCode[] DefaultAltKeyboardKeys;

    /// <summary>
    /// Default Joystick Controls - Display as strings
    /// </summary>
    public static string[] DefaultJoyStickAxes;

    /// <summary>
    /// Default Alternate Joystick Controls - Display as String
    /// </summary>
    public static string[] DefaultAltJoyStickAxes;

    /// <summary>
    /// Virtual helper function to set the default controls of the game
    /// Overide this and implement the game controls
    /// </summary>
    protected virtual void DefaultControlsSetup()
    {
        GameControls = new string[] //All the types controls that the game has.
        {
            "Forward",
            "Backward",
            "Left",
            "Right",
            "Up",
            "Down",
        };
        DefaultKeyboardKeys = new KeyCode[] //Set Keyboard Keys - Ordered according with GameControls
        {
            KeyCode.W,
            KeyCode.S,
            KeyCode.A,
            KeyCode.D,
            KeyCode.Q,
            KeyCode.E
        };
        DefaultAltKeyboardKeys = new KeyCode[] //Set AltKeyboard Keys - Ordered according with GameControls
        {
            KeyCode.UpArrow,
            KeyCode.DownArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow,
            KeyCode.PageUp,
            KeyCode.PageDown
        };
        /* Note: JoystickAxes must also be setup in Unity Project->InputManager->Keys Axes(String) */
        DefaultJoyStickAxes = new string[] //Set JoysitckAxes - Ordered according with GameControls
        {
            "LSUp",
            "LSDown",
            "LSLeft",
            "LSRight",
            "RSUp",
            "RSDown",
        };
        DefaultAltJoyStickAxes = new string[] //Set AltJoysitckAxes - Ordered according with GameControls
        {
            "LSUp",
            "LSDown",
            "LSLeft",
            "LSRight",
            "RSUp",
            "RSDown",
        };
    }

    #endregion 

    /// <summary>
    /// Set the Default Controls on Awake
    /// </summary>
    public virtual void Awake()
    {
        DefaultControlsSetup();
    }

    /// <summary>
    /// Check for First Launch and save the number of times played into playerPref on Start
    /// </summary>
    public virtual void Start()
    {
        if (PlayerPrefs.HasKey("TimesGamePlayed"))
        {
            PlayerPrefs.SetInt("TimesGamePlayed", PlayerPrefs.GetInt("TimesGamePlayed") + 1);
            Debug.Log("Welcome Back! TimesPlayed : " + PlayerPrefs.GetInt("TimesGamePlayed"));
        }
        else
        {   
            PlayerPrefs.SetInt("TimesGamePlayed", 1);
            Debug.Log("First Time Playing!");
        }
    }
}
