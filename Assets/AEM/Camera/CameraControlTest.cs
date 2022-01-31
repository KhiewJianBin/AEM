using AEM;
using AEM.Managers;
using UnityEngine;

public class CameraControlTest : GameManager
{
    public AEMCameraControl t;

    public override void Awake()
    {
        base.Awake();
        if (t == null)
        {
            t = GetComponent<AEMCameraControl>();
        }
    }   
    public override void Start()
    {
        if (t == null)
        {
            enabled = false;
            Debug.Log(this.gameObject + " Could Not Find CameraControl script");
        }
        else
        {
            AEMCamera a = t.GetCam(Random.Range(0, t.Cameralist.Count - 1));
            t.SetUpCam(0,0,0, Vector3.zero, Vector3.zero,CameraPreset.Chase);
            a.Cam.enabled = true;
        }
    }
    void Update()
    {
    }
}
