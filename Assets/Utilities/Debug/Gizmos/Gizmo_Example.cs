using UnityEngine;

public class Gizmo_Example : MonoBehaviour
{
    public GameObject LineFrom;
    public GameObject LineTo;
    private void OnDrawGizmos()
    {
        GizmoExtension.DrawSphere(transform.position,2, Color.red);
        GizmoExtension.DrawWireSphere(transform.position, 2, Color.white);

        GizmoExtension.DrawCube(transform.position + new Vector3(1, 0, 0), new Vector3(3,3,3), Color.red);
        GizmoExtension.DrawWireCube(transform.position + new Vector3(1, 0, 0), new Vector3(3, 3, 3), Color.white);

        GizmoExtension.DrawLine(LineFrom.transform.position, LineTo.transform.position, Color.green);
        GizmoExtension.DrawRay(new Ray(transform.position, Vector3.up), Color.green);

        GizmoExtension.DrawFrustum(transform.position, 35, 100, 10, 1.77f, Color.black);


        GizmoExtension.DrawPoint(transform.position, 2, Color.blue);
        GizmoExtension.DrawBounds(new Bounds(transform.position+new Vector3(10, 0, 0), new Vector3(3,3,3)), Color.blue);
        GizmoExtension.DrawCircle(transform.position + new Vector3(20, 0, 0), Vector3.forward, 2, Color.blue);
        GizmoExtension.DrawCylinder(transform.position + new Vector3(40, 0, 0), new Vector3(50, 0, 0), Color.blue, 2);
        GizmoExtension.DrawCone(transform.position + new Vector3(50, 0, 0),Vector3.forward, Color.blue, 45);
        GizmoExtension.DrawArrow(transform.position + new Vector3(60, 0, 0), Vector3.forward, Color.blue);
        GizmoExtension.DrawCapsule(transform.position + new Vector3(70, 0, 0), new Vector3(80, 0, 0), Color.blue, 2);
    }
}
