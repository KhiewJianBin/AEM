using System.Collections.Generic;
using System.Linq;
using AEM;
using UnityEngine;

public class AEMCameraControl : MonoBehaviour
{
    public AEMCamera ActiveCamera;

    public List<AEMCamera> Cameralist;

    [SerializeField]
    protected List<GameObject> Maintargets;

    [SerializeField]
    protected List<GameObject> Looktargets;

    public void AddMainTarget(GameObject inmain)
    {
        Maintargets.Add(inmain);
    }
    public void RemoveMainTarget(GameObject inmain)
    {
        foreach (GameObject go in Maintargets)
        {
            if (inmain == go)
            {
                Maintargets.Remove(inmain);
                return;
            }
        }
    }
    public void AddLookTarget(GameObject inlook)
    {
        Looktargets.Add(inlook);
    }
    public void RemoveLookTarget(GameObject inlook)
    {
        foreach (GameObject go in Looktargets)
        {
            if (inlook == go)
            {
                Maintargets.Remove(inlook);
                return;
            }
        }
    }

    void Awake()
    {
        GetAllCamInGame();
    }
    void Start()
    {
    }
    void Update()
    {
        GetAllCamInGame();
    }

    void GetAllCamInGame()
    {
        Cameralist = FindObjectsOfType(typeof(AEMCamera)).Cast<AEMCamera>().ToList();
        //ActiveCamera = Camera.main.GetComponent<AEMCamera>();
    }
    public AEMCamera GetCam(int i)
    {
        if (i < Cameralist.Count)
        {
            return Cameralist[i];
        }
        throw new UnityException("GetCam Index Not Correct");
    }
 

    public void SetCurrentCamera(AEMCamera incam)
    {
        foreach (AEMCamera cam in Cameralist)
        {
            if (cam != incam)
            {
                cam.GetComponent<Camera>().enabled = false;
                cam.enabled = false;
            }
            else
            {
                cam.GetComponent<Camera>().enabled = true;
                cam.enabled = false;
            }
        }
    }

    #region Setups
    public void SetUpCam(AEMCamera incam,GameObject mainTarget,GameObject lookAtTarget, Vector3 inPosOffset, Vector3 inRotOffset, CameraPreset preset = CameraPreset.FixedLook)
    {
        SetCamMainTarget(incam, mainTarget);
        SetCamLookat(incam,lookAtTarget);
        SetCamPreset(incam, preset);
        SetCamPositionOffset(incam, inPosOffset);
        SetCamRotationOffset(incam, inRotOffset);
    }
    public void SetUpCam(int incam, int mainTargetindex, int lookAtTargetindex, Vector3 inPosOffset, Vector3 inRotOffset, CameraPreset preset = CameraPreset.FixedLook)
    {
        SetCamMainTarget(incam, mainTargetindex);
        SetCamLookat(incam, lookAtTargetindex);
        SetCamPreset(incam, preset);
        SetCamPositionOffset(incam, inPosOffset);
        SetCamRotationOffset(incam, inRotOffset);
    }
    public void SetCamMainTarget(AEMCamera incam, GameObject mainTarget)
    {
        incam.MainTarget = mainTarget;
    }
    public void SetCamMainTarget(int incam, int intarget)
    {
        if (incam >= Cameralist.Count)
        {
            print("Camera index" + incam + " does not exist");
            return;
        }
        if (intarget >= Maintargets.Count)
        {
            print("Maintargets [index " + intarget + "] does not exist");
            return;
        }
        Cameralist[incam].MainTarget = Maintargets[intarget];
    }   
    public void SetCamLookat(AEMCamera incam, GameObject lookAtTarget)
    {
        incam.LookAtTarget = lookAtTarget;
    }
    public void SetCamLookat(int incam, int lookAtTarget)
    {
        if (incam >= Cameralist.Count)
        {
            print("Camera index" + incam + " does not exist");
            return;
        }
        if (lookAtTarget >= Looktargets.Count)
        {
            print("LookAtTarget [index " + lookAtTarget + "] does not exist");
            return;
        }
        Cameralist[incam].LookAtTarget = Looktargets[lookAtTarget];
    }
    public void SetCamPreset(AEMCamera incam, CameraPreset preset)
    {
        incam.CameraType = preset;
    }
    public void SetCamPreset(int incam, CameraPreset preset)
    {
        Cameralist[incam].CameraType = preset;
    }
    public void SetCamPositionOffset(AEMCamera incam, Vector3 inPosOffset)
    {
        incam.CamPosOffset=inPosOffset;
    }
    public void SetCamPositionOffset(int incam, Vector3 inPosOffset)
    {
        Cameralist[incam].CamPosOffset = inPosOffset;
    }
    public void SetCamRotationOffset(AEMCamera incam, Vector3 inRotOffset)
    {
        incam.CamRotOffset = inRotOffset;
    }
    public void SetCamRotationOffset(int incam, Vector3 inRotOffset)
    {
        Cameralist[incam].CamRotOffset = inRotOffset;
    }

    #endregion
}