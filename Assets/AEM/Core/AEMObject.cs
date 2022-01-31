using System.Collections.Generic;
using UnityEngine;

public abstract class AEMObject : MonoBehaviour
{
    public List<AEMModule> ModuleList = new List<AEMModule>();
    public List<Buff> BuffModuleList = new List<Buff>();

    protected abstract void OnEnable();

    protected abstract void OnDisable();
}
