using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RadialTrigger : MonoBehaviour
{
    [SerializeField]
    private float radius = 1;
    [SerializeField]
    private Transform player = null;


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 center = transform.position;
        
        if (player == null)
            return;

        Vector3 playerPosition = player.transform.position;
        Vector3 delta = center - playerPosition;
        float sqrDistance = delta.x * delta.x + delta.y * delta.y + delta.z * delta.z;
        bool inside = sqrDistance <= radius * radius;

        Gizmos.color = inside ? Color.white : Color.red;
        Gizmos.DrawSphere(center, radius);
    }
#endif
}
