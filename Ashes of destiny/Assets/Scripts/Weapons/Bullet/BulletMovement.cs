using System.Collections;
using System.Collections.Generic; // Necesario para la lista
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

    // --- NUEVA LÓGICA DE PERFORACIÓN ---
    [SerializeField] bool canPierce = true;
    [SerializeField] int maxTargets = 3;
    private int _targetsHit = 0;
    // Esta lista evita que la bala golpee al MISMO enemigo más de una vez
    private List<GameObject> _hitEnemies = new List<GameObject>();

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
        if (particle != null) particle.Play(true);
    }

    public void SetupBullet(Ability data)
    {
        _myAbilityData = data;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.activeInHierarchy) return;
        if (_hitEnemies.Contains(other.gameObject)) return;

        if (other.CompareTag("Enemy"))
        {
            // 2. REGISTRAMOS AL ENEMIGO EN LA LISTA (para no repetir daño ni combos)
            _hitEnemies.Add(other.gameObject);
            _targetsHit++;

            // 3. APLICAMOS DAÑO
            if (other.TryGetComponent(out HealthEnemy healthEnemy))
            {
                healthEnemy.TakeDamage(damage, forceImpulse);
            }

            if (_myAbilityData != null && other.TryGetComponent(out EnemyStatus status))
            {
                status.ApplyElement(_myAbilityData.abilityName, _myAbilityData.icon);
            }

            if (!canPierce || _targetsHit >= maxTargets)
            {
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.isStatic || other.CompareTag("Untagged"))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
    }
}