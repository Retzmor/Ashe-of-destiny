using UnityEngine;
using Zenject;

public class HealthEnemy : MonoBehaviour
{
    [Inject] TutorialManager tutorialManager;

    [SerializeField] float healthMax;
    [SerializeField] float currentHealth;

    EnemyHealthBar healthBar;

    EnemyMovement enemyMovement;

    void Start()
    {
        currentHealth = healthMax;
        enemyMovement = GetComponent<EnemyMovement>();
        healthBar = GetComponent<EnemyHealthBar>();

        healthBar.SetMaxHealth(healthMax);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        enemyMovement.TakeDamageEffect();

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        tutorialManager.TutorialWin();
        Destroy(gameObject);
    }
}
