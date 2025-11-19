using System.Collections;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] float radiusAttackMelee;
    [SerializeField] float radiusAttackRange;
    [SerializeField] LayerMask layer;
    [SerializeField] GameObject _currentWeapon;
    [SerializeField] Transform targetAttack;
    [SerializeField] GameObject bullet;

    bool canAttackMelee = false;
    bool coolDownAttack = true;

    public GameObject CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }

    private void FixedUpdate()
    {
        Collider [] zoneAttackMelee = Physics.OverlapSphere(transform.position, radiusAttackMelee, layer);

        if(zoneAttackMelee.Length > 0)
        {
            canAttackMelee = true;
        }

        else
        {
            canAttackMelee = false;
        }
    }

    public void Attack()
    {
        if(coolDownAttack == true)
        {
           GameObject newBullet = Instantiate(bullet, targetAttack.position, targetAttack.rotation);
           StartCoroutine(CooldownAttack());
        }
    }

    IEnumerator CooldownAttack()
    {
        coolDownAttack = false;
        yield return new WaitForSeconds(5f);
        coolDownAttack = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAttackMelee);
    }
}
