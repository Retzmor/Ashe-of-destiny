using System.Collections;
using UnityEngine;
using Zenject;
public class SimpleRangedAttack : MonoBehaviour
{
    [SerializeField] float attackRange = 12f;
    [SerializeField] float fireRate = 2f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    private EnemyDetector detector;
    private Animator anim;
    private HealthEnemy health;
    private float nextFireTime;
    [Inject] private DiContainer _container;
    void Start()
    {
        detector = GetComponent<EnemyDetector>();
        anim = GetComponentInChildren<Animator>();
        health = GetComponent<HealthEnemy>();
    }

    void Update()
    {
        if (!detector.PlayerDetected) return;

        float distance = Vector3.Distance(transform.position, detector.Player.position);

        if (distance <= attackRange)
        {
            Vector3 lookPos = detector.Player.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        if (anim != null) anim.SetTrigger("Attack");

    }
    public void LaunchProjectile()
    {
        if (detector.Player == null) return;

        Vector3 direction = (detector.Player.position - firePoint.position).normalized;

        GameObject projectile = _container.InstantiatePrefab(
            projectilePrefab,
            firePoint.position,
            Quaternion.LookRotation(direction),
            null
        );

        if (projectile.TryGetComponent(out EnemyBullet bullet))
        {
            bullet.SetDirection(direction);
        }
    }
}