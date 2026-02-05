using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] Image healthImage;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    void Start()
    {
        healthImage.fillAmount = 1;
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthImage.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("muerte");
        }
    }
}
