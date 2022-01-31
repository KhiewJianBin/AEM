using UnityEngine;
using System.Collections.Generic;

public abstract class AEMModule : MonoBehaviour
{
    /// <summary>
    /// A Module May or May Not Have User Inputs that Tigger Stuff within the Module
    /// </summary>
    public Dictionary<string, InputTrigger> InputTriggers;
}
public static class AEMModuleGOExtension
{
    public static T AddModuleToGO<T>(this GameObject addToGO) where T : AEMModule
    {
        GameObject newgo = new GameObject(addToGO.name + " " + typeof(T).Name);
        newgo.transform.parent = addToGO.transform;
        return newgo.AddComponent<T>();
    }
}