using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
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
        Debug.Log("Take bullet enemy");

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
        Destroy(gameObject);
    }
}
