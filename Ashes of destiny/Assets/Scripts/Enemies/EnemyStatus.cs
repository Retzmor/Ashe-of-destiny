using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] Image lastSkillIcon; 
    [SerializeField] GameObject iconContainer;
    [SerializeField] GameObject fireParticlesPrefab;

    private string lastElement = "";
    private float comboTimer = 0f;
    [SerializeField] float maxComboTime = 4f;
    private GameObject currentFireParticles;

    void Start()
    {
        if (iconContainer != null) iconContainer.SetActive(false);
    }

    void Update()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                ResetStatus();
            }
        }
    }
    public void ApplyElement(string element, Sprite icon)
    {
        if ((lastElement == "Fire" && element == "Air") || (lastElement == "Air" && element == "Fire"))
        {
            ApplyKatonCombo();
            return;
        }
        if ((lastElement == "Water" && element == "Rock") || (lastElement == "Rock" && element == "Water"))
        {
            ApplyLodoCombo();
            return;
        }
        lastElement = element;
        comboTimer = maxComboTime;
        if (lastSkillIcon != null)
        {
            lastSkillIcon.sprite = icon;
            iconContainer.SetActive(true);
        }
        ApplyBaseEffect(element);
    }

    private void ApplyBaseEffect(string element)
    {
        switch (element)
        {
            case "Fire": StartCoroutine(BurnRoutine(2f, 5f)); break;
            case "Air":  /* Pushback */ break;
            case "Water": /* Daño + Slow */ break;
            case "Rock": if (TryGetComponent(out EnemyKnockback knock)) knock.Stun(1.5f); break;
        }
    }

    private void ApplyKatonCombo()
    {
        Debug.Log("¡COMBO KATON APLICADO!");
        // Aquí disparas el empuje fuerte + fuego 5s
        ResetStatus();
    }

    private void ApplyLodoCombo()
    {
        Debug.Log("¡COMBO LODO APLICADO!");
        // Idea para Lodo: El enemigo se queda pegado al suelo (Root) y recibe más daño
        ResetStatus();
    }

    public void ResetStatus()
    {
        lastElement = "";
        comboTimer = 0;
        if (iconContainer != null) iconContainer.SetActive(false);
    }

    public IEnumerator BurnRoutine(float duration, float damagePerSecond)
    {
        HealthEnemy health = GetComponent<HealthEnemy>();

       if (fireParticlesPrefab != null && currentFireParticles == null)
        {
            currentFireParticles = Instantiate(fireParticlesPrefab, transform.position, Quaternion.identity, transform);
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (health != null)
            {
                health.TakeDamage(damagePerSecond * Time.deltaTime, 0);
            }

            elapsed += Time.deltaTime;
            yield return null; 
        }

        if (currentFireParticles != null)
        {
           Destroy(currentFireParticles);
        }
    }
}

