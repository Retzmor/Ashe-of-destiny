using UnityEngine;
using Zenject;

public class BulletMovement : MonoBehaviour
{
    Rigidbody rb;
    Collider colliderBullet;

    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float rotationSpeed = 800f;
    ParticleSystem particle;

    bool alreadyDamage = false;

    [Inject] PlayerCollisions player;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        particle = GetComponentInChildren<ParticleSystem>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
    void Start()
    {
        colliderBullet = GetComponent<Collider>();

        foreach (Collider col in player.GetComponentsInChildren<Collider>())
        {
            Physics.IgnoreCollision(colliderBullet, col);
        }

        Destroy(gameObject, 5f);
    }

    public void SetDirection(Vector3 dir)
    {
        dir.Normalize();
        transform.forward = dir;
        rb.linearVelocity = dir * speed;
        particle.Play(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (alreadyDamage) return;

        if (other.CompareTag("Enemy"))
        {
            alreadyDamage = true;

            if (other.TryGetComponent(out HealthEnemy healthEnemy))
            {
                healthEnemy.TakeDamage(damage, 15);
            }

            Destroy(gameObject);
        }
    }


    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
    }
}