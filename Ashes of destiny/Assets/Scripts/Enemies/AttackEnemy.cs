using System.Collections;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] float damageAttack = 10f;
    [SerializeField] float cooldownAttack = 3.0f; 
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] Transform zoneAttack;

    Animator anim;
    bool canAttack = true;
    private HealthEnemy healthEnemy; 

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        healthEnemy = GetComponent<HealthEnemy>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!canAttack) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            StartAttackSequence();
        }
    }

    private void StartAttackSequence()
    {
        canAttack = false;
        anim.SetTrigger("Attack");
        StartCoroutine(CoolDownRoutine());
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
                    hp.ChangeHealth(damageAttack, transform.position);
                    break;
                }
            }
        }
    }

    IEnumerator CoolDownRoutine()
    {
        yield return new WaitForSeconds(cooldownAttack);
        canAttack = true;
    }

    private void OnDrawGizmos()
    {
        if (zoneAttack != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(zoneAttack.position, attackRange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Viento"))
        {

        }
    }
}