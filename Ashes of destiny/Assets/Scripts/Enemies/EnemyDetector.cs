using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] float radius = 8f;
    [SerializeField] LayerMask playerLayer;

    public Transform Player { get; private set; }
    public bool PlayerDetected { get; private set; }

    void Update()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, playerLayer);

        if (hit.Length > 0)
        {
            PlayerDetected = true;
            Player = hit[0].transform;
        }
        else
        {
            PlayerDetected = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
