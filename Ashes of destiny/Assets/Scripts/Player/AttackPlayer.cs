using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] float radiusAttackMelee;
    [SerializeField] float radiusAttackRange;
    [SerializeField] LayerMask layer;
    [SerializeField] GameObject _currentWeapon;
    [SerializeField] Transform targetAttack;

    AbilitiesPlayer abilitiesPlayer;

    bool canAttackMelee = false;
    bool coolDownAttack = true;
    bool coolDown = false;

    public GameObject CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }

    private void Start()
    {
        abilitiesPlayer = GetComponent<AbilitiesPlayer>();
    }


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

    public void Attack(Ashes ashes)
    {
        if (!coolDownAttack)
            return;

        if (ashes == null)
        {
            return;
        }

        if (ashes.ElementAttack == null)
        {
            return;
        }

        Instantiate(ashes.ElementAttack, targetAttack.position, targetAttack.rotation);
        ashes.DesactiveRock();

        StartCoroutine(CooldownAttack());
        StartCoroutine(abilitiesPlayer.CooldownVisual(abilitiesPlayer.CurrentButton, 5f));
        abilitiesPlayer.particulaActual.DesactiveParticule();
    }

    IEnumerator CooldownAttack()
    {
        coolDown = true;
        coolDownAttack = false;
        yield return new WaitForSeconds(5f);
        abilitiesPlayer.particulaActual.ActivasParticulasLoop();
        coolDownAttack = true;
        coolDown = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAttackMelee);
    }
}
