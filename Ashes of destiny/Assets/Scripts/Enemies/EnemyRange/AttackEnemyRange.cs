using System.Collections;
using UnityEngine;
using Zenject;

public class AttackEnemyRange : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] float attackCooldown = 2f;
    [SerializeField] float attackRange = 10f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint; 
    Animator anim;
    EnemyDetector detector; 
    bool canAttack = true;

    [Inject] private DiContainer _container; 

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        detector = GetComponent<EnemyDetector>();
    }

    private void Update()
    {
        if (detector.PlayerDetected && canAttack)
        {
            float distance = Vector3.Distance(transform.position, detector.Player.position);

            if (distance <= attackRange)
            {
                StartCoroutine(AttackRoutine());
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        canAttack = false;

        transform.LookAt(new Vector3(detector.Player.position.x, transform.position.y, detector.Player.position.z));
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
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
