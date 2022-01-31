using UnityEngine;

public class WayPointGizmo : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "waypoint/wayPoint.png", true);
    }
}
