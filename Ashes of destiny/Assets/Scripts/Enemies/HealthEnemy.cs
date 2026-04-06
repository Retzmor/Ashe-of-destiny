using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class HealthEnemy : MonoBehaviour
{
    [InjectOptional] TutorialManager tutorialManager;
    [InjectOptional] LevelController levelController;
    [Inject] PlayerCollisions playerCollisions;
    [SerializeField] float healthMax;
    [SerializeField] float currentHealth;
    [SerializeField] Material materialEnemy;
    [SerializeField] Material materialDamage;
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] CombatCount combat;
    Material materialCurrent;
    Animator animator;
    EnemyHealthBar healthBar;
    EnemyKnockback enemyKnockback;
    EnemyAudio enemyAudio;
    bool dead = false;
    private Coroutine materialCoroutine;
    int _enemyDeath = 0;
    void Start()
    {
        currentHealth = healthMax;
        healthBar = GetComponent<EnemyHealthBar>();
        animator = GetComponent<Animator>();
        healthBar.SetMaxHealth(healthMax);
        enemyKnockback = GetComponent<EnemyKnockback>();
        enemyAudio = GetComponent<EnemyAudio>();
    }
    public void TakeDamage(float damage, float knockbackForce)
    {
        if (dead) return;
        animator.SetTrigger("TakeDamage");
        enemyAudio.DamageHit();
        if (materialCoroutine != null) StopCoroutine(materialCoroutine);
        ChangeMaterial();
        materialCoroutine = StartCoroutine(ChangeMaterialCorutine());

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (enemyKnockback != null)
        {
            Vector3 dir = (transform.position - playerCollisions.transform.position).normalized;
            enemyKnockback.Push(dir, knockbackForce);
        }

        if (currentHealth <= 0)
        {
            dead = true;
            if (materialCoroutine != null) StopCoroutine(materialCoroutine);
            Death();
        }
    }
    IEnumerator ChangeMaterialCorutine()
    {
        yield return null;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        BackMaterial();
        materialCoroutine = null; 
    }
    public void ChangeMaterial()
    {
        Material[] materials = meshRenderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = materialDamage;
        }

        meshRenderer.materials = materials;
    }

    public void BackMaterial()
    {
        Material[] materials = meshRenderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = materialEnemy;
        }

        meshRenderer.materials = materials;
        animator.Play("Patrol");
    }

    public void Death()
    {
        if (materialCoroutine != null) StopCoroutine(materialCoroutine);
        BackMaterial();
        if (tutorialManager != null && levelController != null)
        {
            levelController.WinTutorial();
        }

        if(combat != null)
        {
            combat.RegisterEnemyDeath();
        }
        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        if (TryGetComponent(out Collider col)) col.enabled = false;
        EnemyStatus status = GetComponent<EnemyStatus>();
        if (status != null)
        {
            status.StopAllCoroutines();
            status.ResetStatus();
        }
        animator.SetBool("Death", true);
        enemyAudio.DamageDeath();
        StartCoroutine(AnimationDeath());
    }

    IEnumerator AnimationDeath()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

    public void ResetEnemy()
    {
        dead = false;
        currentHealth = healthMax;

        if (meshRenderer != null) BackMaterial();
        if (animator != null)
        {
            animator.SetBool("Death", false);
            animator.Play("Patrol"); // O tu animación inicial
        }

        if (agent != null)
        {
            agent.enabled = true;
            agent.isStopped = false;
        }

        if (TryGetComponent(out Collider col)) col.enabled = true;

        if (TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(healthMax);
            healthBar.SetHealth(currentHealth);
        }
    }
}
