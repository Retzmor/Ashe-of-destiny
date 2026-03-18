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
        if (isMeleeOnCooldown || attacking) return;
        if (Time.time - lastAttackTime > comboResetTime) comboStep = 0;
        int hitIndex = comboStep;
        playerComponent.Animator.SetInteger("Combo", comboStep);
        playerComponent.Animator.SetTrigger("AttackMelee");
        lastAttackTime = Time.time;
        float customCD = (hitIndex == 2) ? 0.8f : 0.1f;
        StartCoroutine(MeleeCooldownRoutine(customCD));
        comboStep++;
        if (comboStep > 2) comboStep = 0;
    }

    private IEnumerator MeleeCooldownRoutine(float delay)
    {
        isMeleeOnCooldown = true;
        if (comboStep == 2) playerMovement.SetAttackMultiplier(0.2f);
        yield return new WaitForSeconds(delay);
        playerMovement.SetAttackMultiplier(1f);
        isMeleeOnCooldown = false;
    }
    public void MeleeHit()
    {
        int currentHitType = playerComponent.Animator.GetInteger("Combo");
        float damage = 5f;
        float force = 2f;
        float stopDuration = 0.05f;
        if (currentHitType == 2) 
        {
            damage = 15f;
            force = 12f;       
            stopDuration = 0.12f; 
        }

        Collider[] hitEnemies = Physics.OverlapSphere(
            targetAttackMelee.position,
            radiusAttackMelee,
            layer
        );

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out HealthEnemy health))
            {
                health.TakeDamage(damage, force);
                health.ChangeMaterial();
                HitStop.Instance.Stop(stopDuration);
            }

            if (enemy.GetComponentInParent<WoodCollision>())
            {
                enemy.GetComponentInParent<WoodCollision>().AnimationWoodBroke();
            }
        }

        if (currentHitType == 2)
        {
            particula2.ActivasParticulas();
        }
        else
        {
            particulas.ActivasParticulas();
        }

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
