using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LocalToWorldSpace : MonoBehaviour
{
    [SerializeField]
    private Vector2 localCoord;


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 worldPosition = LocalToWorld(localCoord);
        Gizmos.DrawSphere(worldPosition, 0.1f);
    }
#endif

    private Vector2 LocalToWorld(Vector2 local)
    {
        Vector2 position = transform.position;
        position += local.x * (Vector2)transform.right;
        position += local.y * (Vector2)transform.up;
        return position;
    }
}
