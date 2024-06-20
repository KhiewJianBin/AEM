using System;
using UnityEngine;

/// <summary>
/// Transform Extension Class
/// </summary>
public static class TransformExtension 
{
    /// <summary>
    /// Destroys all child of parent gameobject 
    /// </summary>
    public static void ClearContainer(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Destroys all child of parent gameobject 
    /// </summary>
    public static void ClearContainerImmediate(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            child.parent = null;
            GameObject.DestroyImmediate(child.gameObject);
        }
    }
    public static void SetLayerAllChildren(this Transform root, int layer)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
            child.gameObject.layer = layer;
        }
    }


}




[Serializable]
public struct TransformData
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public TransformData(Transform transform)
    {
        Position = transform.localPosition;
        Rotation = transform.localRotation;
        Scale = transform.localScale;
    }

    public void ApplyTo(Transform transform)
    {
        transform.localPosition = Position;
        transform.localRotation = Rotation;
        transform.localScale = Scale;
    }
}
