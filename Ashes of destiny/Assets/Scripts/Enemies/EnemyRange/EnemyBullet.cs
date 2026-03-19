using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 15f;
    [SerializeField] float damage = 10f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] ParticleSystem vfx;

    bool hasHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector3 dir)
    {
        if (rb == null) rb = GetComponent<Rigidbody>();

        dir.Normalize();
        transform.forward = dir;

        rb.linearVelocity = dir * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out HealthPlayer health))
            {
                hasHit = true;
                health.ChangeHealth(damage, transform.position);
                DestroyProjectile();
            }
        }

        if (other.CompareTag("Untagged") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            hasHit = true;
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
