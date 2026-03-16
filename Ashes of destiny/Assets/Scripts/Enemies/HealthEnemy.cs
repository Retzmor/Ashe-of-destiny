using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class HealthEnemy : MonoBehaviour
{
    [InjectOptional] TutorialManager tutorialManager;

    [SerializeField] float healthMax;
    [SerializeField] float currentHealth;
    [SerializeField] Material materialEnemy;
    [SerializeField] Material materialGray;
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    [SerializeField] NavMeshAgent agent;
    Material materialCurrent;
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
        materialCurrent = materialEnemy;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //enemyMovement.TakeDamageEffect();
        animator.SetTrigger("TakeDamage");
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void ChangeMaterial()
    {
        StartCoroutine(WaitForColor());
    }
    IEnumerator WaitForColor()
    {
        meshRenderer.material = materialGray;
        yield return new WaitForSeconds(0.2f);
        meshRenderer.material = materialEnemy;
    }


    public void Death()
    {
        if(tutorialManager != null)
        {
            tutorialManager.TutorialWin();
            agent.isStopped = true;
            animator.SetBool("Death", true);
            Destroy(gameObject, 3f);
        }
    }
}
