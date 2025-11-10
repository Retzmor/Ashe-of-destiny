using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] float radiusAttackMelee;
    [SerializeField] float radiusAttackRange;
    [SerializeField] LayerMask layer;
    [SerializeField] GameObject _currentWeapon;

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
        Debug.Log("Ataque");
        // ir craneando como hacer el sistema de ataque
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAttackMelee);
    }
}
