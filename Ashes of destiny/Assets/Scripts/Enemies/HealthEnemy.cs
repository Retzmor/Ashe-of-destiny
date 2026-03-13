using UnityEngine;
using Zenject;

public class HealthEnemy : MonoBehaviour
{
    [InjectOptional] TutorialManager tutorialManager;

    [SerializeField] float healthMax;
    [SerializeField] float currentHealth;
    Animator animator;

    EnemyHealthBar healthBar;

    EnemyMovement enemyMovement;

    void Start()
    {
        currentHealth = healthMax;
        enemyMovement = GetComponent<EnemyMovement>();
        healthBar = GetComponent<EnemyHealthBar>();
        animator = GetComponent<Animator>();
        healthBar.SetMaxHealth(healthMax);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        enemyMovement.TakeDamageEffect();
        animator.SetTrigger("TakeDamage");
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        if(tutorialManager != null)
        {
            tutorialManager.TutorialWin();
            //animator.SetBool("Death", true);
            Destroy(gameObject);
        }
    }
}
