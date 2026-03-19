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
    bool dead = false;

    void Start()
    {
        currentHealth = healthMax;
        healthBar = GetComponent<EnemyHealthBar>();
        animator = GetComponent<Animator>();
        healthBar.SetMaxHealth(healthMax);
        enemyKnockback = GetComponent<EnemyKnockback>();
    }

    public void TakeDamage(float damage, float knockbackForce)
    {
        if (dead == true) return;
        currentHealth -= damage;
        animator.SetTrigger("TakeDamage");
        healthBar.SetHealth(currentHealth);

        if (enemyKnockback != null)
        {
            enemyKnockback.Push(playerCollisions.transform.position, knockbackForce);
        }

        if (currentHealth <= 0)
        {
            dead = true;
            Death();
        }
    }
    public void ChangeMaterial()
    {
        meshRenderer.material = materialDamage;
    }
    public void BackMaterial()
    {
        meshRenderer.material = materialEnemy;
    }

    public void Death()
    {
        if(tutorialManager != null)
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
            Destroy(gameObject, 3f);
        }
    }
}
