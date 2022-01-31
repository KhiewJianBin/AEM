// unity quadtree https://github.com/unitycoder/UnityQuadtreeInTexture

using UnityEngine;

public class QuadtreeExample : MonoBehaviour
{
    Texture2D tex;
    public Renderer targetRenderer;
    QuadTree quadTree;

    Color32[] resetColorArray;
    Color32 resetColor = new Color32(0, 0, 0, 255);

    void Start()
    {
        int resolution = 256;

        // build texture
        tex = new Texture2D(resolution, resolution, TextureFormat.RGB24, false);
        tex.filterMode = FilterMode.Point;

        // clear texture
        resetColorArray = tex.GetPixels32();
        ClearTexture(tex);
        tex.Apply(false);

        Rect boundary = new Rect(0, 0, resolution, resolution);

        int capacity = 1;
        quadTree = new QuadTree(boundary, capacity);

        // visualize in quad
        targetRenderer.material.mainTexture = tex;
    }

    void Update()
    {
        // click with mouse to add points
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var x = hit.textureCoord.x * tex.width;
                var y = hit.textureCoord.y * tex.height;

                quadTree.Insert(new Vector2(x, y));

                quadTree.Show(tex);
                tex.Apply(false);
            }
        }
    }


    void ClearTexture(Texture2D tex)
    {
        for (int i = 0, len = resetColorArray.Length; i < len; i++)
        {
            resetColorArray[i] = resetColor;
        }
        tex.SetPixels32(resetColorArray);
    }

}
