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
        GameObject newBullet = Instantiate(bullet, targetAttack.position,targetAttack.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAttackMelee);
    }
}
