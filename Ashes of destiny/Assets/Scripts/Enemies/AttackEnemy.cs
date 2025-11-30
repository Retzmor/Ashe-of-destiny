using System.Collections;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] float damageAttack;
    [SerializeField] float cooldDownAttack;
    bool canAttack = true;
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && canAttack)
        {
            if(collision.gameObject.TryGetComponent<HealthPlayer>(out HealthPlayer healthPlayer))
            {
                StartCoroutine(CoolDownAttack(healthPlayer));
            }
        }
    }

   IEnumerator CoolDownAttack(HealthPlayer health)
    {
        canAttack = false;
        health.ChangeHealth(damageAttack);
        yield return new WaitForSeconds(cooldDownAttack);
        canAttack = true;
    }
}
