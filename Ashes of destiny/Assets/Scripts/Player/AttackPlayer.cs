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
    PlayerComponent playerComponent;
    [Inject] private DiContainer _container;
    Dictionary<int, Coroutine> cooldowns = new();


    public GameObject CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }
    private void Start()
    {
        abilitiesPlayer = GetComponent<AbilitiesPlayer>();
        playerComponent = GetComponent<PlayerComponent>();
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
            playerComponent.Animator.SetTrigger("Shoot");
            Vector3 targetPoint = crosshairController.CurrentAimPoint;
            Vector3 direction = (targetPoint - targetAttack.position).normalized;
            Vector3 spawnPos = targetAttack.position;
            GameObject bullet = _container.InstantiatePrefab(ashes.ElementAttack,spawnPos,Quaternion.LookRotation(direction),null);
            bullet.GetComponent<BulletMovement>().SetDirection(direction);
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
    internal void AttackMelee()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(targetAttackMelee.position,radiusAttackMelee,layer);
        if (hitEnemies.Length == 0)
        {
            playerComponent.Animator.SetTrigger("AttackMelee");
        }

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.GetComponentInParent<WoodCollision>())
            {
                playerComponent.Animator.SetTrigger("AttackMelee");
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
