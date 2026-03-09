using UnityEngine;
using Zenject;

public class BulletMovement : MonoBehaviour
{
    Rigidbody rb;
    Collider colliderBullet;

    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float rotationSpeed = 800f;

    bool alreadyDamage = false;

    [Inject] PlayerCollisions player;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.linearVelocity = transform.forward * 20f;

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
        rb.linearVelocity = dir.normalized * speed;  
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
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
    }
}