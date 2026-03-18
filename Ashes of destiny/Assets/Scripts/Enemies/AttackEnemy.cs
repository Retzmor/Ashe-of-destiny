using System.Collections;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] float damageAttack;
    [SerializeField] float cooldDownAttack;
    [SerializeField] float attackRange;
    [SerializeField] Transform zoneAttack;

    Animator anim;
    HealthPlayer healthPlayer;
    EnemyKnockback enemyKnockback;
    bool canAttack = true;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        enemyKnockback = GetComponent<EnemyKnockback>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (!canAttack)
            return;

        collision.gameObject.TryGetComponent(out healthPlayer);

        //anim.SetTrigger("Attack");

        canAttack = false;
        StartCoroutine(CoolDownAttack());
    }

    public void AttackPlayer()
    {
        Collider[] hitPlayers = Physics.OverlapSphere(zoneAttack.position, attackRange);

        foreach (Collider col in hitPlayers)
        {
            if (col.CompareTag("Player"))
            {
                if (col.TryGetComponent(out HealthPlayer hp))
                {
                    //hp.ChangeHealth(damageAttack, transform.position);
                    break; 
                }
            }
        }
    }
    IEnumerator CoolDownAttack()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        canAttack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(zoneAttack.position, attackRange);
    }
}
