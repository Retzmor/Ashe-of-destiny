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
    Material materialCurrent;
    Animator animator;
    EnemyHealthBar healthBar;
    EnemyKnockback enemyKnockback;
    EnemyAudio enemyAudio;
    bool dead = false;
    private Coroutine materialCoroutine;
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
            enemyKnockback.Push(playerCollisions.transform.position, knockbackForce);
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
        meshRenderer.material = materialDamage;
    }
    public void BackMaterial()
    {
        meshRenderer.material = materialEnemy;
        animator.Play("Patrol");
    }

    public void Death()
    {
        if (materialCoroutine != null) StopCoroutine(materialCoroutine);
        BackMaterial();
        if (tutorialManager != null)
        {

            levelController.WinTutorial();
            agent.isStopped = true;
            agent.enabled = false;
            if (TryGetComponent(out Rigidbody rb))
            {
                rb.linearVelocity = Vector3.zero; 
                rb.isKinematic = true;
                rb.useGravity = false;
            }
            if (TryGetComponent(out Collider col)) col.enabled = false;
            animator.SetBool("Death", true);
            enemyAudio.DamageDeath();
            Destroy(gameObject, 3f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Viento"))
        {
            enemyKnockback.Push(-transform.forward, 200);
        }
    }
}
