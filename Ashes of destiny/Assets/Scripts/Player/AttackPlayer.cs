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
    [SerializeField] GameObject _bullet;

    AbilitiesPlayer abilitiesPlayer;

    bool canAttackMelee = false;
    bool coolDownAttack = true;
    bool coolDown = false;

    public GameObject CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }
    public GameObject Bullet { get => _bullet; set => _bullet = value; }

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

    public void Attack(Button botonActivo)
    {
        if(coolDownAttack == true)
        {
           GameObject newBullet = Instantiate(Bullet, targetAttack.position, targetAttack.rotation);
           StartCoroutine(CooldownAttack());
            if (botonActivo != null) 
            { 
                StartCoroutine(abilitiesPlayer.CooldownVisual(botonActivo, 5f)); 
            }
        }
    }

    IEnumerator CooldownAttack()
    {
        coolDown = true;
        coolDownAttack = false;
        yield return new WaitForSeconds(5f);
        coolDownAttack = true;
        coolDown = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAttackMelee);
    }
}
