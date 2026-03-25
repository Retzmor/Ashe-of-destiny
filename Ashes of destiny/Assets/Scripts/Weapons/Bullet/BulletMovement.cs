using System.Collections;
using UnityEngine;
using Zenject;

public class BulletMovement : MonoBehaviour
{
    Rigidbody rb;
    Collider colliderBullet;

    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float rotationSpeed = 800f;
    [SerializeField] float forceImpulse;
    ParticleSystem particle;
    private Ability _myAbilityData;
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

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.GetComponent<PlayerCollisions>();
        }

        if (player != null)
        {
            foreach (Collider col in player.GetComponentsInChildren<Collider>())
            {
                if (col != null && colliderBullet != null)
                    Physics.IgnoreCollision(colliderBullet, col);
            }
        }
        Destroy(gameObject, 3f);
    }

    public void SetDirection(Vector3 dir)
    {
        dir.Normalize();
        transform.forward = dir;
        rb.linearVelocity = dir * speed;
        particle.Play(true);
    }

    public void SetupBullet(Ability data)
    {
        _myAbilityData = data;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (alreadyDamage) return;

        if (other.CompareTag("Enemy"))
        {
            alreadyDamage = true;

            if (other.TryGetComponent(out HealthEnemy healthEnemy))
            {
                healthEnemy.TakeDamage(damage, forceImpulse);
            }

            if (_myAbilityData != null && other.TryGetComponent(out EnemyStatus status))
            {
                status.ApplyElement(_myAbilityData.abilityName, _myAbilityData.icon);
            }
        }
    }
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
    }
}