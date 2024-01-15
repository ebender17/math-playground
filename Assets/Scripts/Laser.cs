using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Laser : MonoBehaviour
{
    [SerializeField]
    private int maxBounces = 40;


#if UNITY_EDITOR
    // For the xy plane
    private void OnDrawGizmos()
    {
        Vector2 origin = transform.position;
        Vector2 direction = transform.right; // x axis
        Ray ray = new Ray(origin, direction);

        for (int i = 0; i < maxBounces; i++)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(ray.origin, hit.point);
                Gizmos.DrawSphere(hit.point, 0.1f);
                Vector2 reflected = Reflect(ray.direction, hit.normal);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(hit.point, (Vector2)hit.point + reflected);
                ray.direction = reflected;
                ray.origin = hit.point;
            }
            else
            {
                break;
            }
        }
    }
#endif

    Vector2 Reflect(Vector2 inDir, Vector2 n)
    {
        float projection = Vector2.Dot(inDir, n);
        return inDir - 2 * projection * n;
    }
}
