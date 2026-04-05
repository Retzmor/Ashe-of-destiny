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

        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            _hitEnemies.Add(other.gameObject);
            _targetsHit++;

            if (other.TryGetComponent(out HealthEnemy healthEnemy))
            {
                healthEnemy.TakeDamage(damage, forceImpulse);
            }

            else if (other.TryGetComponent(out HealthBoss hBoss))
            {
                hBoss.TakeDamage(damage);
                hBoss.ChangeMaterial();
                hBoss.Invoke("SetNormalMaterial", 0.2f);
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
            WoodCollision wood = other.GetComponentInParent<WoodCollision>();
            if (wood != null)
            {
            PlayerAudio player = FindAnyObjectByType<PlayerAudio>();
            player.PlayHitWood();
                wood.AnimationWoodBroke();
                if (!canPierce) Destroy(gameObject);
            }
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
    }
}