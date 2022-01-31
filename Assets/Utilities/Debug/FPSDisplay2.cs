using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(Text))]
[RequireComponent(typeof(RectTransform))]

//Usage: Attach this script to camera
public class FPSDisplay2 : MonoBehaviour
{
    RectTransform fpsTransform;
    Text fpsText;

    float deltaTime = 0.0f;
    float ms;
    float fps;

    public int Size = 12;
    public Color FPSColor = Color.red;
    public TextAnchor FPSAnchor = TextAnchor.UpperRight;
    public Vector2 TargetScreenSize = new Vector2(1920, 1080);

    void Start()
    {
        //Check If Got Canvas
        if (!GetComponentInParent<Canvas>())
        {
            Debug.LogWarning(name + " Need Canvas In Parent");
            enabled = false;
        }

        fpsTransform = GetComponent<RectTransform>();
        fpsText = GetComponent<Text>();

        fpsTransform.anchoredPosition = Vector2.zero;
        fpsTransform.rotation = Quaternion.identity;
        fpsTransform.localScale = Vector3.one;

        //Anchor to Top Right
        fpsTransform.anchorMax = Vector2.one;
        fpsTransform.anchorMin = Vector2.one;
        fpsTransform.pivot = Vector2.one;
    }
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        //Calculate
        fps = 1.0f / deltaTime;
        ms = deltaTime * 1000.0f;

        //Display
        fpsTransform.sizeDelta = new Vector2(TargetScreenSize.x, TargetScreenSize.y);
        //fpsTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        fpsText.text = string.Format("{0:f2} fps ({1:f1} ms)", fps, ms);
        fpsText.alignment = FPSAnchor;
        fpsText.color = FPSColor;
        fpsText.fontSize = Size;
    }
}