using AEM.Inputs;
using UnityEngine;
public class ShootController : AEMController
{
    public ShootModule Module;
	void Start()
    {
        if (!Module)
            Debug.LogWarning(this + "ShootModule not assign");
    }
	void Update()
    {
        if(Module)
            Control();
	}
    protected override void Control()
    {
        Module.shoot = InputManager.GetKeyboardDown("Shoot", KeyCode.Mouse0);
    }
    public override void Remove()
    {
        InputManager.RemoveInput("Shoot");
        Destroy(this);
    }
}