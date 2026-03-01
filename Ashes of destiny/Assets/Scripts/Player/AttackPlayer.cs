using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using Zenject;
using System;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] float radiusAttackMelee;
    [SerializeField] float radiusAttackRange;
    [SerializeField] LayerMask layer;
    [SerializeField] GameObject _currentWeapon;
    [SerializeField] Transform targetAttack;
    [SerializeField] WorldCrossHairController crosshairController;
    [SerializeField] Transform targetAttackMelee;

    AbilitiesPlayer abilitiesPlayer;
    DiContainer _container;
    bool canAttackMelee = false;
    bool coolDownAttack = true;
    bool coolDown = false;
    private bool meleeMode = false;
    Dictionary<int, Coroutine> cooldowns = new();

    public GameObject CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }

    [Inject]
    void Construct(DiContainer container)
    {
        _container = container;
    }
    private void Start()
    {
        abilitiesPlayer = GetComponent<AbilitiesPlayer>();
    }

    public void Attack(Ashes ashes)
    {
        int slotIndex = abilitiesPlayer.CurrentSlotIndex;

        if (slotIndex < 0)
            return;

        if (!cooldowns.ContainsKey(slotIndex))
            cooldowns.Add(slotIndex, null);

        if (cooldowns[slotIndex] == null)
        {
            cooldowns[slotIndex] = StartCoroutine(CooldownAttack(slotIndex));

            Vector3 direction = (crosshairController.CurrentAimPoint - targetAttack.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            _container.InstantiatePrefab(
                ashes.ElementAttack,
                targetAttack.position,
                rotation,
                null
            );

            StartCoroutine(abilitiesPlayer.CooldownVisual(abilitiesPlayer.CurrentButton, 5f));
            abilitiesPlayer.particulaActual.DesactiveParticule();
        }
    }

    IEnumerator CooldownAttack(int slotIndex)
    {
        yield return new WaitForSeconds(5f);

        cooldowns[slotIndex] = null;

        if (abilitiesPlayer.particulaActual != null)
            abilitiesPlayer.particulaActual.ActivasParticulasLoop();
    }

    public bool IsOnCooldown(Ashes ashes)
    {
        int slotIndex = abilitiesPlayer.CurrentSlotIndex;

        if (!cooldowns.ContainsKey(slotIndex))
            return false;

         return cooldowns[slotIndex] != null;
    }
    public void ToggleMeleeMode()
    {
        meleeMode = !meleeMode;

        if (meleeMode)
        {
            ActivateMelee();
        }
        else
        {
            DeactivateMelee();
        }
    }

    void ActivateMelee()
    {
        if (_currentWeapon != null)
            _currentWeapon.SetActive(true);

        abilitiesPlayer.ClearSelection();
    }

    void DeactivateMelee()
    {
        if (_currentWeapon != null)
            _currentWeapon.SetActive(false);
    }
    internal void AttackMelee()
    {

        Collider[] hitEnemies = Physics.OverlapSphere(targetAttackMelee.position,radiusAttackMelee,layer);
        if (hitEnemies.Length == 0)
        {
            Debug.Log("No golpeó a nadie");
            return;
        }

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("Dentro del for");
            if (enemy.GetComponentInParent<WoodCollision>())
            {
                Debug.Log("wood");
                WoodCollision wood = enemy.GetComponentInParent<WoodCollision>();
                wood.AnimationWoodBroke();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetAttackMelee.position, radiusAttackMelee);
    }
}
