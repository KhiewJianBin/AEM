using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

public static class UnityGameObjectExtensions
{
    /// <summary>
    /// Activates (calls SetActive(true)) this gameObject
    /// </summary>
    public static void Activate(this GameObject go)
    {
        go.SetActive(true);
    }

    /// <summary>
    /// Deactivates (calls SetActive(false)) this gameObject
    /// </summary>
    public static void Deactivate(this GameObject go)
    {
        go.SetActive(false);
    }
    
    /// <summary>
    /// Switch Active and Deactive
    /// </summary>
    public static void ToggleActive(this GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    /// <summary>
    /// Adds and returns a child gameObject to this gameObject with the specified name and HideFlags
    /// </summary>
    public static GameObject AddEmptyChild(this GameObject parent, string name, HideFlags flags = HideFlags.None)
    {
        GameObject child = new GameObject(name);
        child.hideFlags = flags;
        child.transform.parent = parent.transform;
        child.transform.Reset();
        return child;
    }

    /// <summary>
    /// Adds and returns a parent gameObject to this gameObject with the specified name and HideFlags
    /// </summary>
    public static GameObject AddEmptyParent(this GameObject child, string name, HideFlags flags = HideFlags.None)
    {
        GameObject parent = new GameObject(name);
        parent.hideFlags = flags;
        parent.transform.position = child.transform.position;
        parent.transform.rotation = child.transform.rotation;
        parent.transform.localScale = child.transform.lossyScale;
        child.transform.parent = parent.transform;
        child.transform.Reset();
        return parent;
    }

    /// <summary>
    /// Recursively returns children gameObjects active only or includeInactive
    /// </summary>
    public static IEnumerable<GameObject> GetChildren(this GameObject parent, bool includeInactive)
    {
        var transform = parent.transform;
        int count = transform.childCount;
        GameObject child;
        for (int i = 0; i < count; i++)
        {
            child = transform.GetChild(i).gameObject;
            if (includeInactive || child.gameObject.activeInHierarchy)
            {
                yield return child;
            }
        }
    }

    /// <summary>
    /// Destroy active children
    /// </summary>
    public static void DestroyActiveChildren(this GameObject gameObject)
    {
        var children = gameObject.GetChildren(false).ToList();
        for (int i = 0, cnt = children.Count; i < cnt; i++)
        {
            var child = children[i];
            child.Destroy();
        }
    }

    /// <summary>
    /// Destroy active children
    /// </summary>
    public static void DestroyAllChildren(this GameObject gameObject)
    {
        var children = gameObject.GetChildren(false).ToList();
        for (int i = 0, cnt = children.Count; i < cnt; i++)
        {
            var child = children[i];
            child.Destroy();
        }
    }

    /// <summary>
    /// Returns the names of all the components attached to this gameObject
    /// </summary>
    public static string[] GetComponentsNames(this GameObject go)
    {
        return go.GetComponents<Component>().Select(c => c.GetType().Name).ToArray();
    }

    /// <summary>
    /// Gets the parent with a certain name 
    /// </summary>
    public static GameObject FindParentWithName(this GameObject child, string name)
    {
        Transform currentParent = child.transform.parent;
        while (currentParent != null)
        {
            if (currentParent.name == name)
            {
                return currentParent.gameObject;
            }
            currentParent = currentParent.parent;
        }
        return null;
    }

    /// <summary>
    /// Gets the child gameObject whose name is specified by 'wanted'
    /// The search is non-recursive by default unless true is passed to 'recursive'
    /// </summary>
    public static GameObject FindChildWithName(this GameObject inside, string name)
    {
        foreach (Transform child in inside.transform)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Function to obtain the bounds of the current object, or the bounds of its children gameobjects
    /// https://forum.unity.com/threads/getting-the-bounds-of-the-group-of-objects.70979/
    /// </summary>
    public static Bounds GetBounds(this GameObject go)
    {
        Bounds bounds;
        Renderer childRender;
        bounds = GetRenderBounds(go);
        if (bounds.extents.x == 0)
        {
            bounds = new Bounds(go.transform.position, Vector3.zero);
            foreach (Transform child in go.transform)
            {
                childRender = child.GetComponent<Renderer>();
                if (childRender)
                {
                    bounds.Encapsulate(childRender.bounds);
                }
                else
                {
                    bounds.Encapsulate(GetBounds(child.gameObject));
                }
            }
        }
        return bounds;
    }

    /// <summary>
    /// Get bounds from gameobject renderer
    /// </summary>
    public static Bounds GetRenderBounds(this GameObject objeto)
    {
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        Renderer render = objeto.GetComponent<Renderer>();
        if (render != null)
        {
            return render.bounds;
        }
        return bounds;
    }

    public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
    {
        return go.AddComponent<T>().GetCopyOf(toAdd) as T;
    }
}