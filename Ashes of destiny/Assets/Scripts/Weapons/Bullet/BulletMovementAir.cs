using UnityEngine;
using Zenject;

public class BulletMovementAir : MonoBehaviour
{
    Rigidbody rb;
    Collider colliderBullet;

    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float rotationSpeed = 800f;
    [SerializeField] float initialScale = 0.5f;
    [SerializeField] float maxScale = 3.0f;
    [SerializeField] float growthSpeed = 2.0f;
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
        transform.localScale = Vector3.one * initialScale;
        colliderBullet = GetComponent<Collider>();
        foreach (Collider col in player.GetComponentsInChildren<Collider>())
        {
            Physics.IgnoreCollision(colliderBullet, col);
        }

        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        if (transform.localScale.x < maxScale)
        {
            transform.localScale += Vector3.one * growthSpeed * Time.deltaTime;
        }
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