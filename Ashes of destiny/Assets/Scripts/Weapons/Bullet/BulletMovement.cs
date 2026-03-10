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


    private void OnCollisionEnter(Collision collision)
    {
        if (alreadyDamage) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            alreadyDamage = true;

            if (collision.gameObject.TryGetComponent(out HealthEnemy healthEnemy))
            {
                healthEnemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wood"))
        {
            if (collision.gameObject.TryGetComponent(out WoodCollision wood))
            {
                wood.AnimationWoodBroke();
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log(collision.gameObject.name);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
    }
}