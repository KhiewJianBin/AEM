using UnityEngine;

//Usage: Just Attach This To A GameObject
public class FPSDisplay : MonoBehaviour
{
    float deltaTime;
    float ms;
    float fps;

    public int Size = 12;
    public Color FpsColor = Color.red;
    public TextAnchor FpsAnchor = TextAnchor.UpperRight;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int width = Screen.width;
        int height = Screen.height;

        GUIStyle fpsstyle = new GUIStyle();
        Rect fpsrect = new Rect(0, 0, width, height);
        fpsstyle.alignment = FpsAnchor;
        fpsstyle.fontSize = Size;
        fpsstyle.normal.textColor = FpsColor;

        //Calculate
        fps = 1.0f / deltaTime;
        ms = deltaTime * 1000.0f;

        //Display
        string fpsText = string.Format("{0:f2} fps ({1:f1} ms)", fps,ms);
        GUI.Label(fpsrect, fpsText, fpsstyle);
    }
}