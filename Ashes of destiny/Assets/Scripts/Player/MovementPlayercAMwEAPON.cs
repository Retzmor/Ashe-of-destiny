using UnityEngine;

public class MovementPlayercAMwEAPON : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float rayDistance = 100f;

    void LateUpdate()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.origin + ray.direction * rayDistance;
        }

        Vector3 dir = targetPoint - transform.position;
        dir.y = 0f; 

        if (dir.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
