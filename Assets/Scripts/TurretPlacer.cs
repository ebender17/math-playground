using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TurretPlacer : MonoBehaviour
{
    [SerializeField]
    private Transform turret;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (turret == null)
            return;

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            turret.position = hit.point;

            Vector3 yAxis = hit.normal;
            
            // This deals better with walls
            // Vector3 xAxis = Vector3.Cross(yAxis, ray.direction).normalized;
            // Vector3 zAxis = Vector3.Cross(xAxis, yAxis);

            // This behaves better with low angles
            Vector3 zAxis = Vector3.Cross(transform.right, yAxis).normalized;
            Vector3 xAxis = Vector3.Cross(zAxis, yAxis);

            //  TODO : More proper solution would blend between these options

            Gizmos.color = Color.red;
            Gizmos.DrawRay(hit.point, xAxis);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(hit.point, yAxis);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(hit.point, zAxis);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(ray.origin, hit.point);

            turret.rotation = Quaternion.LookRotation(zAxis, yAxis);
        }
    }
#endif
}
