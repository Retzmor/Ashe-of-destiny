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
    [SerializeField] Particulas particulas;
    [SerializeField] Particulas particula2;
    [SerializeField] float comboResetTime = 1.0f;
    AbilitiesPlayer abilitiesPlayer;
    PlayerComponent playerComponent;
    PlayerMovement playerMovement;
    [Inject] private DiContainer _container;
    [Inject] AudioManager audioManager;
    Dictionary<int, Coroutine> cooldowns = new();
    [SerializeField] float meleeCooldown = 0.5f; 
    private bool isMeleeOnCooldown = false;
    private int comboStep = 0;
    private float lastAttackTime;
    private bool attacking = false;

    public GameObject CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }
    private void Start()
    {
        abilitiesPlayer = GetComponent<AbilitiesPlayer>();
        playerComponent = GetComponent<PlayerComponent>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Attack(Ability ability)
    {
        int slotIndex = abilitiesPlayer.CurrentSlotIndex;

        if (slotIndex < 0)
            return;

        if (!cooldowns.ContainsKey(slotIndex))
            cooldowns.Add(slotIndex, null);

        if (cooldowns[slotIndex] == null)
        {
            cooldowns[slotIndex] = StartCoroutine(CooldownAttack(slotIndex,ability.cooldown));
            audioManager.PlaySFX(ability.attackSound, 1f);
            playerComponent.Animator.SetTrigger("Shoot");

            Vector3 targetPoint = crosshairController.CurrentAimPoint;
            Vector3 direction = (targetPoint - targetAttack.position).normalized;

            Vector3 spawnPos = targetAttack.position;

            GameObject bullet = _container.InstantiatePrefab(
                ability.attackPrefab,
                spawnPos,
                Quaternion.LookRotation(direction),
                null
            );

            BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();

            if (bulletMovement != null)
            {
                bulletMovement.SetDirection(direction);
            }


            StartCoroutine(
                abilitiesPlayer.CooldownVisual(
                    abilitiesPlayer.CurrentButton,
                    ability.cooldown
                )
            );

            abilitiesPlayer.particulaActual.DesactiveParticule();
        }
    }


    IEnumerator CooldownAttack(int slotIndex, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        cooldowns[slotIndex] = null;
        if (abilitiesPlayer.CurrentSlotIndex == slotIndex && abilitiesPlayer.particulaActual != null)
            abilitiesPlayer.particulaActual.ActivasParticulasLoop();
    }

    public bool IsOnCooldown(Ability ashes)
    {
        int slotIndex = abilitiesPlayer.CurrentSlotIndex;

        if (!cooldowns.ContainsKey(slotIndex))
            return false;

         return cooldowns[slotIndex] != null;
    }
    internal void AttackMelee()
    {
        if (Time.time - lastAttackTime > comboResetTime)
        {
            comboStep = 0;
        }
        if (isMeleeOnCooldown || attacking) return;
        playerComponent.Animator.SetInteger("Combo", comboStep);
        playerComponent.Animator.SetTrigger("AttackMelee");
        lastAttackTime = Time.time;
        StartCoroutine(MeleeCooldownRoutine());
        comboStep++;
        if (comboStep > 2) comboStep = 0;
    }
    private IEnumerator MeleeCooldownRoutine()
    {
        isMeleeOnCooldown = true;
        playerMovement.SetAttackMultiplier(0.3f);
        yield return new WaitForSeconds(meleeCooldown);
        playerMovement.SetAttackMultiplier(1f);
        isMeleeOnCooldown = false;
    }
    public void MeleeHit()
    {
        int currentHitType = playerComponent.Animator.GetInteger("Combo");
        Collider[] hitEnemies = Physics.OverlapSphere(
            targetAttackMelee.position,
            radiusAttackMelee,
            layer
        );

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out HealthEnemy health))
            {
                float finalDamage = (currentHitType == 2) ? 15f : 5f; 
                float finalKnockback = (currentHitType == 2) ? 15f : 5f;
                health.TakeDamage(5);
                HitStop.Instance.Stop(0.05f);
            }

            if (enemy.GetComponentInParent<WoodCollision>())
            {
                WoodCollision wood = enemy.GetComponentInParent<WoodCollision>();
                wood.AnimationWoodBroke();
            }
        }

        particulas.ActivasParticulas();
        particula2.ActivasParticulas();

        StartCoroutine(ParticuleDesactive());
    }

    public void isAttacking()
    {
        attacking = true;
    }

    public void IsntAttacking()
    {
        attacking = false;
    }

    IEnumerator ParticuleDesactive()
    {
        yield return new WaitForSeconds(0.5f);
        particulas.DesactiveParticule();
        particula2 .DesactiveParticule();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetAttackMelee.position, radiusAttackMelee);
    }
}
