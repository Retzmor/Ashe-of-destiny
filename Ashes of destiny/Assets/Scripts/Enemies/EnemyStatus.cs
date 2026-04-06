using FirstGearGames.SmoothCameraShaker;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] Image lastSkillIcon; 
    [SerializeField] GameObject iconContainer;
    [SerializeField] GameObject fireParticlesPrefab;
    [SerializeField] GameObject fireComboParticles; 
    [SerializeField] GameObject mudComboParticles;
    [SerializeField] ShakeData shakeData;
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
        if (lastElement == element) return;
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
            case "Fire": ; break;
            case "Air":  /* Pushback */ break;
            case "Water": /* Daño + Slow */ break;
            case "Rock": if (TryGetComponent(out EnemyController knock)) knock.ApplyStun(1.5f);
            if (TryGetComponent(out BossController boss)) boss.ApplyStun(1.5f); break;
        }
    }

    private void ApplyKatonCombo()
    {
        if (!gameObject.activeInHierarchy) return;
        if (fireComboParticles != null)
        {
            GameObject p = Instantiate(fireComboParticles, transform.position, Quaternion.identity, transform);
            Debug.Log("Katon Shine Baku Rambu");
            CameraMovement();
            Destroy(p, 5f);
        }
        StopAllCoroutines(); 
        StartCoroutine(BurnRoutine(5f, 20f)); 
        ResetStatus();
    }

    private void ApplyLodoCombo()
    {
        if (!gameObject.activeInHierarchy) return;
        if (mudComboParticles != null)
        {
            GameObject p = Instantiate(mudComboParticles, transform.position, Quaternion.identity, transform);
            Destroy(p, 5f);
        }
        if (TryGetComponent(out EnemyController knock))
        {
            knock.ApplyStun(5f);
            CameraMovement();
        }
        else if (TryGetComponent(out BossController boss))
        {
            boss.ApplyStun(3f); 
            CameraMovement();
        }

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
            //currentFireParticles = Instantiate(fireParticlesPrefab, transform.position, Quaternion.identity, transform);
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (health != null)
            {
                // health.TakeDamage(damagePerSecond * Time.deltaTime, 0);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (currentFireParticles != null)
        {
            //   Destroy(currentFireParticles);
        }
    }

    public void CameraMovement()
    {
        CameraShakerHandler.Shake(shakeData);
    }
}

