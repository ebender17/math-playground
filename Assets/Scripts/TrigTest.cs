using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TrigTest : MonoBehaviour
{
    [SerializeField]
    [Range(0, 360)]
    private float angleDegrees = 0;


    Vector2 AngToDir(float angleRadians) => new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(Vector3.zero, Vector3.forward, 1);
        float angleRadians = angleDegrees * Mathf.Deg2Rad;
        Vector2 v = AngToDir(angleRadians);
        Gizmos.DrawRay(Vector2.zero, v);
    }
#endif
}
