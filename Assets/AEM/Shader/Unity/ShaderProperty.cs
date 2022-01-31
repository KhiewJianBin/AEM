using UnityEngine;

public class ShaderProperty : MonoBehaviour
{
    public string Property = "_Color";
    public Renderer targetRenderer;
    Material mat;

    public void setVectorProperty(string PropertyName, Vector4 val)
    {
        if (!mat)
        {
            if (targetRenderer)
            {
                mat = targetRenderer.material;
            }
            else
            {
                Debug.LogError("No Target");
                return;
            }
        }
        mat.SetVector(PropertyName, val);
    }
    public void setIntProperty(string PropertyName, int val)
    {
        if (!mat)
        {
            if (targetRenderer)
            {
                mat = targetRenderer.material;
            }
            else
            {
                Debug.LogError("No Target");
                return;
            }
        }
        mat.SetInt(PropertyName, val);
    }
    public void setFloatProperty(string PropertyName, float val)
    {
        if (!mat)
        {
            if (targetRenderer)
            {
                mat = targetRenderer.material;
            }
            else
            {
                Debug.LogError("No Target");
                return;
            }
        }
        mat.SetFloat(PropertyName, val);
    }
    public void setColorProperty(string PropertyName,Color val)
    {
        if(!mat)
        {
            if(targetRenderer)
            {
                mat = targetRenderer.material;
            }
            else
            {
                Debug.LogError("No Target");
                return;
            }
        }
        mat.SetColor(PropertyName, val);
    }
}
