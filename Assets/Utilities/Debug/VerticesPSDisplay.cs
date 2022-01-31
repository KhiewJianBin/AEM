using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Info:
/// Utility Script that shows the number of vertices of object and frame
/// 
/// How To Use:
/// Attach this script to GameObject with MeshRenderer.
/// </summary>

public class VPS : MonoBehaviour
{
    public Mesh mesh;
    public bool ShowFps;
    void Start()
    {
        
        GetComponent<Text>().material.color = Color.black;
    }

    void LateUpdate()
    {
        if (!mesh)
            mesh = FindObjectOfType<MeshFilter>().mesh;

        if (Time.frameCount % 5 == 0)
        {
            int vps = (int)(mesh.vertexCount / Time.smoothDeltaTime) / 1000;
            GetComponent<Text>().text = "Vertices per second:\n" + vps + "k";

            int fps = (int) (1.0 / Time.smoothDeltaTime);
            if (ShowFps)
                GetComponent<Text>().text += "\nFrames per second:\n" + fps;
        }
    }
}