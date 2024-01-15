using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WorldToLocalSpace : MonoBehaviour
{
    [SerializeField]
    private Vector2 worldCoord;
    [SerializeField]
    private Vector2 localCoord;


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // update in the insepector
        localCoord = WorldToLocal(worldCoord);
        Gizmos.DrawSphere(localCoord, 0.1f);
    }
#endif

    private Vector2 WorldToLocal(Vector2 world)
    {
        Vector2 delta = world - (Vector2)transform.position; // defining local space
        float x = Vector2.Dot(delta, transform.right);
        float y = Vector2.Dot(delta, transform.up);
        return new Vector2(x, y);
    }
}
