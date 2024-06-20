using System.Collections.Generic;
using UnityEngine;

public static class ExtensionGameObject
{
    //function to obtain the bounds of the current object, or the bounds of its children gameobjects
    //https://forum.unity.com/threads/getting-the-bounds-of-the-group-of-objects.70979/
    //Modified To Get Collective Bounds Of ItSelf, and all children heirichy object (recursive loop)
    public static Bounds? getObjectBounds(this GameObject go)
    {
        Bounds? bounds = getMeshBounds(go);

        foreach (Transform child in go.transform)
        {
            if(bounds.HasValue == false)
            {
                bounds = getObjectBounds(child.gameObject);
            }
            else
            {
                Bounds? childBounds = getObjectBounds(child.gameObject);
                if (childBounds.HasValue)
                {
                    Bounds b = bounds.Value;
                    b.Encapsulate(childBounds.Value);
                    bounds = b;
                }
            }
        }   

        return bounds;
    }
    public static Bounds? getMeshBounds(this GameObject go)
    {
        MeshRenderer meshrender = go.GetComponent<MeshRenderer>();
        if (meshrender != null)
        {
            return meshrender.bounds;
        }
        return null;
    }
    public static Bounds? getObjectsBounds(this List<GameObject> gos)
    {
        if (gos == null || gos.Count < 1) return null;

        Bounds GOSbound = gos[0].GetComponent<Renderer>().bounds;
        for (int i = 1; i < gos.Count; i++)
        {
            GOSbound.Encapsulate(gos[i].GetComponent<Renderer>().bounds);
        }
        return GOSbound;
    }
    public static Transform RecursiveFindChild(this Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }
            else
            {
                Transform found = RecursiveFindChild(child, childName);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }
    public static Transform FindDeepChild_BFS(this Transform aParent, string aName) //Breadth-first search
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == aName)
                return c;
            foreach (Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }

    public static Transform FindDeepChild_DFS(this Transform aParent, string aName) //Depth-first search
    {
        foreach(Transform child in aParent)
        {
            if(child.name == aName )
                return child;
            var result = child.FindDeepChild_DFS(aName);
            if (result != null)
                return result;
        }
        return null;
    }
    public static Vector3 GetGlobalToLocalScaleFactor(this Transform t)
    {
        Vector3 factor = Vector3.one;

        while (true)
        {
            Transform tParent = t.parent;

            if (tParent != null)
            {
                factor.x *= tParent.localScale.x;
                factor.y *= tParent.localScale.y;
                factor.z *= tParent.localScale.z;

                t = tParent;
            }
            else
            {
                return factor;
            }
        }
    }
}
