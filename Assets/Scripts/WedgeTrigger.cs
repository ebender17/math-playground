using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WedgeTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private float radiusOuter = 1;
    [SerializeField]
    private float radiusInner = 0.3f;
    [SerializeField]
    private float height = 1;
    [SerializeField]
    // [Range(0, 1)]
    // private float angularThreshhold = 0.5f;
    [Range(0, 180)]
    private float fovDegrees = 45; // This is not a fast as 'WedgeTrigger' script but provides the same functionality and can set fov in degrees


    float fovRadians => fovDegrees * Mathf.Deg2Rad;
    float AngularThreshold => Mathf.Cos(fovRadians / 2);


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!target) { return; }

        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;
        Gizmos.color = Handles.color = CheckInsideTrigger(target.position) ? Color.white : Color.red;

        Vector3 top = new Vector3(0, height, 0);

        // Handles.DrawWireDisc(Vector3.zero, Vector3.up, radius);
        // Handles.DrawWireDisc(top, Vector3.up, radius);

        float x = Mathf.Sqrt(1 - (AngularThreshold * AngularThreshold));

        Vector3 vLeftDir = new Vector3(-x, 0, AngularThreshold);
        Vector3 vRightDir = new Vector3(x, 0, AngularThreshold);
        Vector3 vLeftOuter = vLeftDir * radiusOuter;
        Vector3 vRightOuter = vRightDir * radiusOuter;
        Vector3 vLeftInner = vLeftDir * radiusInner;
        Vector3 vRightInner = vRightDir * radiusInner;

        Handles.DrawWireArc(Vector3.zero, Vector3.up, vLeftOuter, fovDegrees, radiusOuter);
        Handles.DrawWireArc(top, Vector3.up, vLeftOuter, fovDegrees, radiusOuter);
        Handles.DrawWireArc(Vector3.zero, Vector3.up, vLeftInner, fovDegrees, radiusInner);
        Handles.DrawWireArc(top, Vector3.up, vLeftInner, fovDegrees, radiusInner);

        Gizmos.DrawLine(vLeftInner, vLeftOuter);
        Gizmos.DrawLine(vRightInner, vRightOuter);
        Gizmos.DrawLine(top + vLeftInner, top + vLeftOuter);
        Gizmos.DrawLine(top + vRightInner, top + vRightOuter);

        Gizmos.DrawLine(vLeftInner, top + vLeftInner);
        Gizmos.DrawLine(vRightInner, top + vRightInner);
        Gizmos.DrawLine(vLeftOuter, top + vLeftOuter);
        Gizmos.DrawLine(vRightOuter, top + vRightOuter);
    }
#endif

    public bool CheckInsideTrigger(Vector3 position)
    {
        Vector3 vecToTargetWorld = (position - transform.position);
        Vector3 vecToTargetLocal = transform.InverseTransformVector(vecToTargetWorld);

        // Height position check
        if (vecToTargetLocal.y > height || vecToTargetLocal.y < 0)
            return false;

        // Angular check
        Vector3 flatDirToTarget = vecToTargetLocal;
        flatDirToTarget.y = 0;
        float flatDistanceToTarget = flatDirToTarget.magnitude;
        flatDirToTarget /= flatDistanceToTarget;
        if (flatDirToTarget.z < AngularThreshold)
            return false;

        // Cylindrical radius
        if (flatDistanceToTarget > radiusOuter || flatDistanceToTarget < radiusInner)
            return false;

        return true;
    }
}
