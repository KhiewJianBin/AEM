using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Dictionary<Text, string> TextDict = new Dictionary<Text, string>();
    public Dictionary<Image, string> ImageDict = new Dictionary<Image, string>();

    //public List<Button> ButtonList = new List<Button>();
    //public List<Dropdown> DropdownList = new List<Dropdown>();
    //public List<Slider> SliderList = new List<Slider>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Debug.LogWarning("There is already a UIManager in the Game. Removing UIManager from " + name);
            Destroy(this);
        }
    }

    public void NewUi(string uiGroupName, params object[] args)
    {
        foreach (object o in args)
        {
            if (o.GetType() == typeof(Text))
            {
                TextDict.Add((Text) o, uiGroupName);
            }
            else if (o.GetType() == typeof(Image))
            {
                ImageDict.Add((Image) o, uiGroupName);
            }
            else
            {
                Debug.LogWarning("UI of type " + o.GetType() + " is not supported by UIManager");
            }
        }
    }

    public void ShowUi(string uiGroupName)
    {
        foreach (KeyValuePair<Text, string> kvp in TextDict)
        {
            if (kvp.Value == uiGroupName)
            {
                kvp.Key.gameObject.SetActive(true);
            }
        }
        foreach (KeyValuePair<Image, string> kvp in ImageDict)
        {
            if (kvp.Value == uiGroupName)
            {
                kvp.Key.gameObject.SetActive(true);
            }
        }
    }

    public void HideUi(string uiGroupName)
    {
        foreach (KeyValuePair<Text, string> kvp in TextDict)
        {
            if (kvp.Value == uiGroupName)
            {
                kvp.Key.gameObject.SetActive(false);
            }
        }
        foreach (KeyValuePair<Image, string> kvp in ImageDict)
        {
            if (kvp.Value == uiGroupName)
            {
                kvp.Key.gameObject.SetActive(false);
            }
        }
    }

    public void DeleteUi(string uiGroupName)
    {
        foreach (KeyValuePair<Text, string> kvp in TextDict)
        {
            if (kvp.Value == uiGroupName)
            {
                Destroy(kvp.Key);
            }
        }
        foreach (KeyValuePair<Image, string> kvp in ImageDict)
        {
            if (kvp.Value == uiGroupName)
            {
                Destroy(kvp.Key);
            }
        }
    }
}