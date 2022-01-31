using UnityEngine;

/// <summary>
/// Abstract Base class for any Input controller class
/// </summary>
public abstract class AEMController : MonoBehaviour
{
    protected abstract void Control();
    public abstract void Remove();
}