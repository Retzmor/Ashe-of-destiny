using UnityEngine;
using Zenject;

public class HealthEnemy : MonoBehaviour
{
    [Inject] TutorialManager tutorialManager;   
    [SerializeField] float healthMax;
    [SerializeField] float healthMin;
    [SerializeField] float currentHealth;
    [SerializeField] Material material;
    EnemyMovement enemyMovement;
    void Start()
    {
        currentHealth = healthMax;
        enemyMovement = GetComponent<EnemyMovement>();
        material = GetComponent<Material>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        enemyMovement.TakeDamageEffect();
        //aqui iria la animacion de recibir daño
        if (currentHealth < healthMin)
        {
            // aqui deberia curarse o tirar alguna habilidad poderosa
            //material.color = Color.green; ;
        }

        if(currentHealth <= 0)
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
