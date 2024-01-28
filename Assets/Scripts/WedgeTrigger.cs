using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WedgeTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private float radius = 1;
    [SerializeField]
    private float height = 1;
    [SerializeField]
    [Range(0, 1)]
    private float angularThreshhold = 0.5f;


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!target) { return; }

        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;
        Gizmos.color = Handles.color = CheckInsideTrigger(target.position) ? Color.white : Color.red;

        Vector3 top = new Vector3(0, height, 0);

        Handles.DrawWireDisc(Vector3.zero, Vector3.up, radius);
        Handles.DrawWireDisc(top, Vector3.up, radius);

        float x = Mathf.Sqrt(1 - (angularThreshhold * angularThreshhold));

        Vector3 vLeft = new Vector3(-x, 0, angularThreshhold) * radius;
        Vector3 vRight = new Vector3(x, 0, angularThreshhold) * radius;

        Gizmos.DrawRay(Vector3.zero, vLeft);
        Gizmos.DrawRay(Vector3.zero, vRight);
        Gizmos.DrawRay(top, vLeft);
        Gizmos.DrawRay(top, vRight);

        Gizmos.DrawLine(Vector3.zero, top);
        Gizmos.DrawLine(vLeft, top + vLeft);
        Gizmos.DrawLine(vRight, top + vRight);
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

        // Cylindrical radius
        if (flatDistanceToTarget > radius)
            return false;

        return true;
    }
}
