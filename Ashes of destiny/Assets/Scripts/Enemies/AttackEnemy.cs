using System.Collections;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] float damageAttack;
    [SerializeField] float cooldDownAttack;

    Animator anim;
    HealthPlayer healthPlayer;
    bool canAttack = true;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (!canAttack)
            return;

        collision.gameObject.TryGetComponent(out healthPlayer);

        anim.SetTrigger("Attack");

        canAttack = false;
        StartCoroutine(CoolDownAttack());
    }

    public void AttackPlayer()
    {
        if (healthPlayer == null)
            return;

        healthPlayer.ChangeHealth(damageAttack, transform.position);
    }



    IEnumerator CoolDownAttack()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        canAttack = true;
    }
}
