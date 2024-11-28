using UnityEngine;

namespace AEM
{
    public enum CameraPreset
    {
        FixedLook, // Look at Main_Target
        Position, // Copies Position of Main_Target
        Rotation, // Copies Rotation of Main_Target
        Chase,
        AnchorPosition,
        AnchorRotation,
        AnchorBoth,
        Custom
    }

    [RequireComponent(typeof(Camera))]
    public class AEMCamera : MonoBehaviour
    {
        /// <summary>
        /// The Unity camera
        /// </summary>
        public Camera Cam;
        /// <summary>
        /// CameraTypes
        /// </summary>
        public CameraPreset CameraType = CameraPreset.Custom;
        /// <summary>
        /// The Maintarget that this camera is following
        /// </summary>
        public GameObject MainTarget;
        /// <summary>
        /// The target that this camera is looking at
        /// </summary>
        public GameObject LookAtTarget;
        /// <summary>
        /// Camera Position Offset From MainTarget
        /// </summary>
        public Vector3 CamPosOffset = Vector3.zero;
        /// <summary>
        /// Camera Rotation Offset From MainTarget
        /// </summary>
        public Vector3 CamRotOffset = Vector3.zero;

        protected bool CopyPositionToggle = true;
        protected bool CopyRotationToggle = true;
        protected bool LookAtToggle = false;

        protected bool AnchorPositionToggle = false;
        protected bool AnchorRotationToggle = false;
        public float AnchorMaxDistance = 0;
        public float AnchorMaxRotation = 0;

        //Default Camera Axis Rotation: Normally The Game Follows the World Axis
        //But if the Game WorldAxis Changes, The Camera Axis Also changes to Match the game's
        public Vector3 AxisRotation = Vector3.zero;
        Matrix4x4 axisMatrix;
        Vector3 forward;
        Vector3 up;
        Vector3 right;

        void Awake()
        {
            //Assign UnityCamera ref
            Cam = GetComponent<Camera>();
        }

        void Update()
        {
            
        }

        void LateUpdate()
        {
            axisMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(-AxisRotation), Vector3.one);
            forward = axisMatrix.MultiplyVector(Vector3.forward);
            up = axisMatrix.MultiplyVector(Vector3.up);
            right = axisMatrix.MultiplyVector(Vector3.right);

            //Apply CameraType Config 
            switch (CameraType)
            {
                case CameraPreset.FixedLook:
                    FixedLook();
                    break;
                case CameraPreset.Position:
                    Position();
                    break;
                case CameraPreset.Rotation:
                    Rotation();
                    break;
                case CameraPreset.Chase:
                    Chase();
                    break;
                case CameraPreset.AnchorPosition:
                    AnchorPosition();
                    break;
                case CameraPreset.AnchorRotation:
                    AnchorRotation();
                    break;
                case CameraPreset.AnchorBoth:
                    AnchorBoth();
                    break;
                case CameraPreset.Custom:
                    break;
            }

            if (CopyPositionToggle)
            {
                forward = axisMatrix.MultiplyVector(MainTarget.transform.forward);
                up = axisMatrix.MultiplyVector(MainTarget.transform.up);
                right = axisMatrix.MultiplyVector(MainTarget.transform.right);

                transform.position = MainTarget.transform.position + (forward * CamPosOffset.z) +
                                     (up * CamPosOffset.y) + (right * CamPosOffset.x);
            }
            if (CopyRotationToggle)
            {
                forward = axisMatrix.MultiplyVector(MainTarget.transform.forward);
                up = axisMatrix.MultiplyVector(MainTarget.transform.up);
                right = axisMatrix.MultiplyVector(MainTarget.transform.right);

                transform.rotation =
                    Quaternion.Euler((MainTarget.transform.rotation.eulerAngles - AxisRotation) +
                                     (-forward * CamRotOffset.z) + (up * CamRotOffset.y) +
                                     (-right * CamRotOffset.x));
            }
            if (LookAtToggle)
            {
                transform.LookAt(LookAtTarget.transform);
            }
            if (AnchorPositionToggle)
            {
                Vector3 FollowDir = Vector3.Normalize(MainTarget.transform.position - transform.position);
                if (AnchorMaxDistance < (MainTarget.transform.position - transform.position).magnitude)
                {
                    transform.position = MainTarget.transform.position - FollowDir * AnchorMaxDistance;
                }
            }
            if (AnchorRotationToggle)
            {
                Vector3 FollowDir = Vector3.Normalize(MainTarget.transform.position - transform.position);
                Quaternion LookRotation = Quaternion.LookRotation(FollowDir);
                if (AnchorMaxRotation < Quaternion.Angle(LookRotation, transform.rotation))
                {
                    transform.rotation = Quaternion.RotateTowards(LookRotation, transform.rotation, AnchorMaxRotation);
                }
            }
        }

        public void Reset()
        {
            //TODO
        }

        #region CameraType setup
        void FixedLook()
        {
            CopyPositionToggle = false;
            CopyRotationToggle = false;
            LookAtToggle = true;
            AnchorPositionToggle = false;
            AnchorRotationToggle = false;

            //Fixed Cam Doesnt Have Offset: Pointless when you can just add position
            CamPosOffset = Vector3.zero;
            CamRotOffset = Vector3.zero;
        }

        void Position()
        {
            CopyPositionToggle = true;
            CopyRotationToggle = false;
            LookAtToggle = false;
            AnchorPositionToggle = false;
            AnchorRotationToggle = false;
        }

        void Rotation()
        {
            CopyPositionToggle = false;
            CopyRotationToggle = true;
            LookAtToggle = false;
            AnchorPositionToggle = false;
            AnchorRotationToggle = false;
        }

        void Chase()
        {
            CopyPositionToggle = true;
            CopyRotationToggle = true;
            LookAtToggle = false;
            AnchorPositionToggle = false;
            AnchorRotationToggle = false;
        }

        void AnchorPosition()
        {
            CopyPositionToggle = false;
            CopyRotationToggle = false;
            LookAtToggle = false;
            AnchorPositionToggle = true;
            AnchorRotationToggle = false;

            //Anchor Doesnt Have Offset: Pointless when you can just append anchor position
            CamPosOffset = Vector3.zero;
            CamRotOffset = Vector3.zero;
        }

        void AnchorRotation()
        {
            CopyPositionToggle = false;
            CopyRotationToggle = false;
            LookAtToggle = false;
            AnchorPositionToggle = false;
            AnchorRotationToggle = true;

            //Anchor Doesnt Have Offset: Pointless when you can just append anchor position
            CamPosOffset = Vector3.zero;
            CamRotOffset = Vector3.zero;
        }

        void AnchorBoth()
        {
            CopyPositionToggle = false;
            CopyRotationToggle = false;
            LookAtToggle = false;
            AnchorPositionToggle = true;
            AnchorRotationToggle = true;

            //Anchor Doesnt Have Offset: Pointless when you can just append anchor position
            CamPosOffset = Vector3.zero;
            CamRotOffset = Vector3.zero;
        }

        void Custom()
        {
        }

        #endregion
    }
}