using System.Collections.Generic;
using UnityEngine;

namespace AEM.Inputs
{
    public enum InputType { Keyboard, Controller, Touch, VR }

    public abstract class InputState { };

    /// <summary>
    /// struct represent the current states of a keyboard key
    /// </summary>
    public class KeyState : InputState
    {
        public bool IsKey;
        public bool IsKeyDown;
        public bool IsKeyUp;
    }

    /// <summary>
    /// struct represent the current states of the controller buttons
    /// </summary>
    public class ControllerState : InputState
    {
        public bool IsButton;
        public bool IsButtonDown;
        public bool IsButtonUp;
    }

#if UNITY_ANDROID
    /// <summary>
    /// struct represent the current states of the touch interface
    /// </summary>
    public class TouchState : InputState
    {
        
        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
    }
    public class Touches
    {
        //TouchPhase.Began
        //TouchPhase.Moved
        //TouchPhase.Stationary
        //TouchPhase.Ended
        //TouchPhase.Canceled
    }
#endif 

    /// <summary>
    /// struct represent the current states of the VR controller buttons
    /// </summary>
    public class VRControllerState : InputState
    {
        public bool IsButton;
        public bool IsButtonDown;
        public bool IsButtonUp;

        public bool IsTouch;
        public bool IsTouchDown;
        public bool IsTouchUp;
    }

    /// <summary>
    /// Base InputManager to handle all or most inputs of the game.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// Implement singleton behaviour, having only one instance class only once
        /// </summary>
        public static InputManager Instance; //static instance for other scripts to access

        [Header("Allow Auto Adding Of New Controls For the Game Into Input Manager")]
        public bool CanAddNewInputName;
        [Header("Allow Auto Adding Of Alternate Input For Existing Controls in Input Manager")]
        public bool CanAddAlternateInputs;// use tp add new inputs to name
        List<string> InputName = new List<string>();//The name of the control that the game uses
        List<List<object>> Inputs = new List<List<object>>();//Each InputName will have a List of possible inputs/alternate inputs under it
        List<List<InputState>> InputsStates = new List<List<InputState>>();//Each inputs will have it's own input states

        [Header("Toggles")]
        public bool ReadInput; //Input read toggle
        public bool ReadKeyboard; //Keyboard read toggle
        public bool ReadController; //Controller read toggle
        public bool ReadTouch; //Controller read toggle
        public bool ReadVR; //VR Controller read toggle

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Debug.LogWarning("There is already a InputManager in the Game. Removing InputManager from " + name);
                Destroy(this);
            }
        }

        /// <summary>
        /// Receive the required I/O inputs from System
        /// </summary>
        void Update()
        {
            if(ReadInput)
            {
                readInput();
            }
        }

        void readInput()
        {
            for (int i = 0; i < InputsStates.Count; i++)
            {
                List<InputState> inputstates = InputsStates[i];
                for (int j = 0; j < inputstates.Count; j++)
                {
                    if(ReadKeyboard && inputstates[j].GetType() == typeof(KeyState))
                    {
                        ((KeyState)inputstates[j]).IsKey = Input.GetKey((KeyCode)Inputs[i][j]);
                        ((KeyState)inputstates[j]).IsKeyDown = Input.GetKeyDown((KeyCode)Inputs[i][j]);
                        ((KeyState)inputstates[j]).IsKeyUp = Input.GetKeyUp((KeyCode)Inputs[i][j]);
                    }
                    else if (ReadController && inputstates[j].GetType() == typeof(ControllerState))
                    {
                        ((ControllerState)inputstates[j]).IsButton = Input.GetButton((string)Inputs[i][j]);
                        ((ControllerState)inputstates[j]).IsButtonDown = Input.GetButtonDown((string)Inputs[i][j]);
                        ((ControllerState)inputstates[j]).IsButtonUp = Input.GetButtonUp((string)Inputs[i][j]);
                    }
                    else if (ReadVR && inputstates[j].GetType() == typeof(VRControllerState))
                    {
                        ((VRControllerState)inputstates[j]).IsButton = Input.GetButton((string)Inputs[i][j]);
                        ((VRControllerState)inputstates[j]).IsButtonDown = Input.GetButtonDown((string)Inputs[i][j]);
                        ((VRControllerState)inputstates[j]).IsButtonUp = Input.GetButtonUp((string)Inputs[i][j]);
                    }
                }
            }
        }

        #region public static functions

        #region Keycode

        public static bool GetKeyboardPress(string control,KeyCode? defaultkey)
        {
            if (!Instance)
            {
                Debug.LogWarning("InputManager not found, unable to get Inputs");
                return false;
            }
            //Loop for all game controls
            for (int i = 0; i < Instance.InputName.Count; i++)
            {
                //find matching control
                if (Instance.InputName[i] == control)
                {
                    List<InputState> inputstate = Instance.InputsStates[i];
                    if (Instance.CanAddAlternateInputs)
                    {
                        List<object> inputsofcontrol = Instance.Inputs[i];
                        if (!inputsofcontrol.Contains(defaultkey))
                        {
                            inputsofcontrol.Add(defaultkey);//add new key
                            inputstate.Add(new KeyState()); //add new state for new input under control[i] name
                        }
                    }
                    foreach (InputState state in inputstate)
                    {
                        if (state.GetType() == typeof(KeyState))
                        {
                            if(((KeyState)state).IsKey)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
            //If input is not inside input manager
            if (Instance.CanAddNewInputName)
            {
                if (defaultkey.HasValue)
                {
                    Instance.InputName.Add(control);

                    List<object> newinputlist = new List<object>();
                    newinputlist.Add(defaultkey);
                    Instance.Inputs.Add(newinputlist);

                    List<InputState> newinputInputState = new List<InputState>();
                    newinputInputState.Add(new KeyState());
                    Instance.InputsStates.Add(newinputInputState); //state for new input

                    Debug.Log("Control Named " + control + " has been added as " + defaultkey.Value);
                }
                else
                {
                    Debug.Log("Assign a default key to " + control);
                }
            }
            else
            {
                Debug.LogWarning("Enable Option to add new input");
            }
            return false;
        }

        public static bool GetKeyboardDown(string control, KeyCode? defaultkey)
        {
            if (!Instance)
            {
                Debug.LogWarning("InputManager not found, unable to get Inputs");
                return false;
            }
            //Loop for all game controls
            for (int i = 0; i < Instance.InputName.Count; i++)
            {
                //find matching control
                if (Instance.InputName[i] == control)
                {
                    List<InputState> inputstate = Instance.InputsStates[i];
                    if (Instance.CanAddAlternateInputs)
                    {
                        List<object> inputsofcontrol = Instance.Inputs[i];
                        if (!inputsofcontrol.Contains(defaultkey))
                        {
                            inputsofcontrol.Add(defaultkey);//add new key
                            inputstate.Add(new KeyState()); //add new state for new input under control[i] name
                        }
                    }
                    foreach (InputState state in inputstate)
                    {
                        if (state.GetType() == typeof(KeyState))
                        {
                            if (((KeyState)state).IsKeyDown)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
            //If input is not inside input manager
            if (Instance.CanAddNewInputName)
            {
                if (defaultkey.HasValue)
                {
                    Instance.InputName.Add(control);

                    List<object> newinputlist = new List<object>();
                    newinputlist.Add(defaultkey);
                    Instance.Inputs.Add(newinputlist);

                    List<InputState> newinputInputState = new List<InputState>();
                    newinputInputState.Add(new KeyState());
                    Instance.InputsStates.Add(newinputInputState); //state for new input

                    Debug.Log("Control Named " + control + " has been added as " + defaultkey.Value);
                }
                else
                {
                    Debug.Log("Assign a default key to " + control);
                }
            }
            else
            {
                Debug.LogWarning("Enable Option to add new input");
            }
            return false;
        }

        public static bool GetKeyboardUp(string control, KeyCode? defaultkey)
        {
            if (!Instance)
            {
                Debug.LogWarning("InputManager not found, unable to get Inputs");
                return false;
            }
            //Loop for all game controls
            for (int i = 0; i < Instance.InputName.Count; i++)
            {
                //find matching control
                if (Instance.InputName[i] == control)
                {
                    List<InputState> inputstate = Instance.InputsStates[i];
                    if (Instance.CanAddAlternateInputs)
                    {
                        List<object> inputsofcontrol = Instance.Inputs[i];
                        if (!inputsofcontrol.Contains(defaultkey))
                        {
                            inputsofcontrol.Add(defaultkey);//add new key
                            inputstate.Add(new KeyState()); //add new state for new input under control[i] name
                        }
                    }
                    foreach (InputState state in inputstate)
                    {
                        if (state.GetType() == typeof(KeyState))
                        {
                            if (((KeyState)state).IsKeyUp)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
            //If input is not inside input manager
            if (Instance.CanAddNewInputName)
            {
                if (defaultkey.HasValue)
                {
                    Instance.InputName.Add(control);

                    List<object> newinputlist = new List<object>();
                    newinputlist.Add(defaultkey);
                    Instance.Inputs.Add(newinputlist);

                    List<InputState> newinputInputState = new List<InputState>();
                    newinputInputState.Add(new KeyState());
                    Instance.InputsStates.Add(newinputInputState); //state for new input

                    Debug.Log("Control Named " + control + " has been added as " + defaultkey.Value);
                }
                else
                {
                    Debug.Log("Assign a default key to " + control);
                }
            }
            else
            {
                Debug.LogWarning("Enable Option to add new input");
            }
            return false;
        }

        #endregion


        #if UNITY_ANDROID

        #region Touch
        public static Touch GetTouch(int index)
        {
            return Input.GetTouch(index);
        }
        public static bool GetTouchBegan(int index)
        {
            if (Input.touchCount > index)
                return true;
            return false;
        }
        public static bool GetTouchMoved(int index)
        {
            if (Input.touchCount > index)
                return true;
            return false;
        }
        public static bool GetTouchStationary(int index)
        {
            if (Input.touchCount > index)
                return true;
            return false;
        }
        public static bool GetTouchEnded(int index)
        {
            if (Input.touchCount > index)
                return true;
            return false;
        }
        public static bool GetTouchCanceled(int index)
        {
            if (Input.touchCount > index)
                return true;
            return false;
        }
        #endregion

        #endif


        #region GameController
        //public static bool GetButtonPress(string control, string defaultAxes)
        //{
        //    if (!Instance)
        //    {
        //        Debug.LogWarning("InputManager not found, unable to get Inputs");
        //        return false;
        //    }
        //    for (int i = 0; i < Instance.InputName.Count; i++)
        //    {
        //        if (Instance.InputName[i] == control)
        //        {
        //            return Instance.keyState[i].IsKeyUp;
        //        }
        //    }
        //    if (Instance.CanAddNewInputs)
        //    {
        //        if (defaultkey.HasValue)
        //        {
        //            Instance.InputName.Add(control);
        //            Instance.VRInputButtons.Add(defaultkey.Value);
        //            Instance.vrcontrollerState.Add(new VRControllerState());
        //            Debug.LogWarning("VRControl Named " + control + " has been added as " + defaultkey.Value);
        //        }
        //        else
        //        {
        //            Debug.LogWarning("Assign a default button to " + control);
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Enable Option to add new input");
        //    }
        //    return false;
        //}

        //public static bool GetButtonDown(string control, string defaultAxes)
        //{
        //    if (!Instance)
        //    {
        //        Debug.LogWarning("InputManager not found, unable to get Inputs");
        //        return false;
        //    }
        //    for (int i = 0; i < Instance.InputName.Count; i++)
        //    {
        //        if (Instance.InputName[i] == control)
        //        {
        //            return Instance.keyState[i].IsKeyUp;
        //        }
        //    }
        //    if (Instance.CanAddNewInputs)
        //    {
        //        if (defaultkey.HasValue)
        //        {
        //            Instance.InputName.Add(control);
        //            Instance.VRInputButtons.Add(defaultkey.Value);
        //            Instance.vrcontrollerState.Add(new VRControllerState());
        //            Debug.LogWarning("VRControl Named " + control + " has been added as " + defaultkey.Value);
        //        }
        //        else
        //        {
        //            Debug.LogWarning("Assign a default button to " + control);
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Enable Option to add new input");
        //    }
        //    return false;
        //}

        //public static bool GetButtonUp(string control, string defaultAxes)
        //{
        //    if (!Instance)
        //    {
        //        Debug.LogWarning("InputManager not found, unable to get Inputs");
        //        return false;
        //    }
        //    for (int i = 0; i < Instance.InputName.Count; i++)
        //    {
        //        if (Instance.InputName[i] == control)
        //        {
        //            return Instance.keyState[i].IsKeyUp;
        //        }
        //    }
        //    if (Instance.CanAddNewInputs)
        //    {
        //        if (defaultkey.HasValue)
        //        {
        //            Instance.InputName.Add(control);
        //            Instance.VRInputButtons.Add(defaultkey.Value);
        //            Instance.vrcontrollerState.Add(new VRControllerState());
        //            Debug.LogWarning("VRControl Named " + control + " has been added as " + defaultkey.Value);
        //        }
        //        else
        //        {
        //            Debug.LogWarning("Assign a default button to " + control);
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Enable Option to add new input");
        //    }
        //    return false;
        //}
        #endregion

        #region VRController
        public static bool GetButtonPress(string control, ulong? defaultkey)
        {
            if (!Instance)
            {
                Debug.LogWarning("InputManager not found, unable to get Inputs");
                return false;
            }
            //Loop for all game controls
            for (int i = 0; i < Instance.InputName.Count; i++)
            {
                //find matching control
                if (Instance.InputName[i] == control)
                {
                    List<InputState> inputstate = Instance.InputsStates[i];
                    if (Instance.CanAddAlternateInputs)
                    {
                        List<object> inputsofcontrol = Instance.Inputs[i];
                        if (!inputsofcontrol.Contains(defaultkey))
                        {
                            inputsofcontrol.Add(defaultkey);//add new key
                            inputstate.Add(new VRControllerState()); //add new state for new input under control[i] name
                        }
                    }
                    foreach (InputState state in inputstate)
                    {
                        if (state.GetType() == typeof(VRControllerState))
                        {
                            if (((VRControllerState)state).IsButton)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
            //If input is not inside input manager
            if (Instance.CanAddNewInputName)
            {
                if (defaultkey.HasValue)
                {
                    Instance.InputName.Add(control);

                    List<object> newinputlist = new List<object>();
                    newinputlist.Add(defaultkey);
                    Instance.Inputs.Add(newinputlist);

                    List<InputState> newinputInputState = new List<InputState>();
                    newinputInputState.Add(new VRControllerState());
                    Instance.InputsStates.Add(newinputInputState); //state for new input

                    Debug.Log("Control Named " + control + " has been added as " + defaultkey.Value);
                }
                else
                {
                    Debug.Log("Assign a default key to " + control);
                }
            }
            else
            {
                Debug.LogWarning("Enable Option to add new input");
            }
            return false;
        }

        public static bool GetButtonDown(string control, ulong? defaultkey, int index)
        {
            if (!Instance)
            {
                Debug.LogWarning("InputManager not found, unable to get Inputs");
                return false;
            }
            //Loop for all game controls
            for (int i = 0; i < Instance.InputName.Count; i++)
            {
                //find matching control
                if (Instance.InputName[i] == control)
                {
                    List<InputState> inputstate = Instance.InputsStates[i];
                    if (Instance.CanAddAlternateInputs)
                    {
                        List<object> inputsofcontrol = Instance.Inputs[i];
                        if (!inputsofcontrol.Contains(defaultkey))
                        {
                            inputsofcontrol.Add(defaultkey);//add new key
                            inputstate.Add(new VRControllerState()); //add new state for new input under control[i] name
                        }
                    }
                    foreach (InputState state in inputstate)
                    {
                        if (state.GetType() == typeof(VRControllerState))
                        {
                            if (((VRControllerState)state).IsButtonDown)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
            //If input is not inside input manager
            if (Instance.CanAddNewInputName)
            {
                if (defaultkey.HasValue)
                {
                    Instance.InputName.Add(control);

                    List<object> newinputlist = new List<object>();
                    newinputlist.Add(defaultkey);
                    Instance.Inputs.Add(newinputlist);

                    List<InputState> newinputInputState = new List<InputState>();
                    newinputInputState.Add(new VRControllerState());
                    Instance.InputsStates.Add(newinputInputState); //state for new input

                    Debug.Log("Control Named " + control + " has been added as " + defaultkey.Value);
                }
                else
                {
                    Debug.Log("Assign a default key to " + control);
                }
            }
            else
            {
                Debug.LogWarning("Enable Option to add new input");
            }
            return false;
        }

        public static bool GetButtonUp(string control, ulong? defaultkey, int index)
        {
            if (!Instance)
            {
                Debug.LogWarning("InputManager not found, unable to get Inputs");
                return false;
            }
            //Loop for all game controls
            for (int i = 0; i < Instance.InputName.Count; i++)
            {
                //find matching control
                if (Instance.InputName[i] == control)
                {
                    List<InputState> inputstate = Instance.InputsStates[i];
                    if (Instance.CanAddAlternateInputs)
                    {
                        List<object> inputsofcontrol = Instance.Inputs[i];
                        if (!inputsofcontrol.Contains(defaultkey))
                        {
                            inputsofcontrol.Add(defaultkey);//add new key
                            inputstate.Add(new VRControllerState()); //add new state for new input under control[i] name
                        }
                    }
                    foreach (InputState state in inputstate)
                    {
                        if (state.GetType() == typeof(VRControllerState))
                        {
                            if (((VRControllerState)state).IsButtonUp)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
            //If input is not inside input manager
            if (Instance.CanAddNewInputName)
            {
                if (defaultkey.HasValue)
                {
                    Instance.InputName.Add(control);

                    List<object> newinputlist = new List<object>();
                    newinputlist.Add(defaultkey);
                    Instance.Inputs.Add(newinputlist);

                    List<InputState> newinputInputState = new List<InputState>();
                    newinputInputState.Add(new VRControllerState());
                    Instance.InputsStates.Add(newinputInputState); //state for new input

                    Debug.Log("Control Named " + control + " has been added as " + defaultkey.Value);
                }
                else
                {
                    Debug.Log("Assign a default key to " + control);
                }
            }
            else
            {
                Debug.LogWarning("Enable Option to add new input");
            }
            return false;
        }
#endregion

        public static void RemoveInput(string control)
        {
            int index = Instance.InputName.IndexOf(control);
            Instance.InputName.RemoveAt(index);
            Instance.Inputs.RemoveAt(index);
            Debug.LogWarning("Control Named " + control + " has been removed");
        }

        #endregion
    }
}