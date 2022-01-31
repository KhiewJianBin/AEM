using UnityEngine;

public class CharaterModuleExample : MonoBehaviour
{
    public CharacterModule character;

    void Update()   
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FloatAddBuff buff = character.gameObject.AddModuleToGO<FloatAddBuff>();
            buff.BuffAddAmt = 10;
            buff.BuffDuration = float.PositiveInfinity;
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle
        {
            fontSize = 64,
            alignment = TextAnchor.UpperCenter
        };
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), character.Health.ToString(), style);
        GUI.Label(new Rect(0, 100, Screen.width, Screen.height), character.Mana.ToString(), style);
        GUI.Label(new Rect(0, 200, Screen.width, Screen.height), character.Energy.ToString(), style);
    }
}
