using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    void Start()
    {
        healthSlider.value = 1;
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthSlider.value = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("muerte");
        }
    }
}
