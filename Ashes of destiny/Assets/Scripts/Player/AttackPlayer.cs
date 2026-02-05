using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

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


    Dictionary<Ashes, Coroutine> cooldowns = new();
    public GameObject CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }

    private void Start()
    {
        abilitiesPlayer = GetComponent<AbilitiesPlayer>();
    }


    private void FixedUpdate()
    {
        Collider[] zoneAttackMelee = Physics.OverlapSphere(transform.position, radiusAttackMelee, layer);

        if (zoneAttackMelee.Length > 0)
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
        if (ashes == null)
            return;

        if (ashes.ElementAttack == null)
            return;

        if (!cooldowns.ContainsKey(ashes))
            cooldowns.Add(ashes, null);

        if (cooldowns[ashes] == null)
        {
            cooldowns[ashes] = StartCoroutine(CooldownAttack(ashes));
            Instantiate(ashes.ElementAttack, targetAttack.position, targetAttack.rotation);
            ashes.DesactiveRock();
            StartCoroutine(abilitiesPlayer.CooldownVisual(abilitiesPlayer.CurrentButton, 5f));
            abilitiesPlayer.particulaActual.DesactiveParticule();
        }
    }

    IEnumerator CooldownAttack(Ashes ashes)
    {
        float currentTime = 0;
        while (currentTime < 5)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        abilitiesPlayer.particulaActual.ActivasParticulasLoop();
        cooldowns[ashes] = null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAttackMelee);
    }
}
