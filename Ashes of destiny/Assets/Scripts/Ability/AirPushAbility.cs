using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

public class AirPushAbility : MonoBehaviour
{
    public float radius = 8f;
    public float force = 25f;
    public LayerMask enemyLayer;

    void Start()
    {
        Destroy(gameObject, 3);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent(out EnemyKnockback knock))
            {
                knock.Push(transform.position, force);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
