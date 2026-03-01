using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthPlayer : MonoBehaviour
{
    [Inject] TutorialManager tutorialManager;
    [SerializeField] Image healthImage;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] CameraManager cameraManager;
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
            Die();
        }
    }

    internal void Die()
    {
        cameraManager.CameraDie();
        StartCoroutine(DieCameraPlayer());
    }

    IEnumerator DieCameraPlayer()
    {
        yield return new WaitForSeconds(3f);
        tutorialManager.TutorialLose();
    }
}
