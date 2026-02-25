using UnityEngine;
using Zenject;

public class BulletMovement : MonoBehaviour
{
    Rigidbody rb;
    Collider colliderBullet;
    Collider colliderPlayer;
    [SerializeField] float speed;
    [SerializeField] float damage;
    bool alreadyDamage = false;

    [Inject] PlayerCollisions player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliderBullet = GetComponent<Collider>();
        colliderPlayer = player.GetComponent<Collider>();
        rb.useGravity = false;
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, 5f);
        foreach (Collider col in player.GetComponentsInChildren<Collider>())
        {
            Physics.IgnoreCollision(colliderBullet, col);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (alreadyDamage)
            return;
        if(collision.gameObject.CompareTag("Enemy"))
        {
            alreadyDamage = true;
            collision.gameObject.TryGetComponent<HealthEnemy>(out HealthEnemy healthEnemy);
            healthEnemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
