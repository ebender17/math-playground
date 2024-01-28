using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Turret : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private WedgeTrigger wedgeTrigger = null;
    [SerializeField]
    private Transform gunTransform = null;
    [SerializeField]
    private float smoothingFactor = 10.0f;


    Quaternion lastTargetRotation = Quaternion.identity;


    private void Update()
    {
        if (wedgeTrigger.CheckInsideTrigger(target.position))
        {
            Vector3 vecToTarget = target.position - gunTransform.position;
            Quaternion targetRotation = Quaternion.LookRotation(vecToTarget, gunTransform.up);
            gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation, targetRotation, smoothingFactor * Time.deltaTime);
            lastTargetRotation = targetRotation;
            return;
        }

        gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation, lastTargetRotation, smoothingFactor * Time.deltaTime);
    }
}
