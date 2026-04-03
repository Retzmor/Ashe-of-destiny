using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using FirstGearGames.SmoothCameraShaker;

public class HealthPlayer : MonoBehaviour
{
    [Inject] LevelController levelController;
    [InjectOptional] TutorialManager tutorialManager;
    [SerializeField] Image healthImage;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] ShakeData shakeData;
    [SerializeField] float knockbackForce = 6f;
    [SerializeField] GameObject panelLose;
    PlayerMovement playerMovement;
    PlayerComponent playerComponent;
    DamageEffect damageEffect;
    bool isDead = false;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerComponent = GetComponent<PlayerComponent>();
        damageEffect = GetComponent<DamageEffect>();
        healthImage.fillAmount = 1;
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float damage, Vector3 attackerPosition)
    {
        if (isDead) return;
        playerComponent.Animator.SetTrigger("TakeDamage");
        CameraShakerHandler.Shake(shakeData);
        damageEffect.TakeDamageEffect();
        StartCoroutine(KnockbackLock());
        Vector3 knockbackDir = transform.position - attackerPosition;
        playerMovement.ApplyKnockback(knockbackDir, knockbackForce);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthImage.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }


    public void Die()
    {
        isDead = true;
        playerComponent.Animator.SetBool("Death", true);
        cameraManager.CameraDie();
        StopAllCoroutines();
        StartCoroutine(DieCameraPlayer());
        StartCoroutine(StopMovementPlayer());
    }

    IEnumerator DieCameraPlayer()
    {
        yield return new WaitForSeconds(1f);

        if (tutorialManager != null)
        {
            tutorialManager.TutorialLose();
        }
        else
        {
            levelController.HandlePlayerDeath();
        }
    }

    IEnumerator StopMovementPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        playerMovement.Rb.isKinematic = true;
    }

    IEnumerator KnockbackLock()
    {
        playerMovement.CanMoving = false;

        yield return new WaitForSeconds(0.15f);

        playerMovement.CanMoving = true;
    }

    internal void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        if (healthImage != null)
        {
            healthImage.fillAmount = 1f; 
        }
        playerMovement.Rb.isKinematic = false;
        playerMovement.CanMoving = true;
        playerComponent.Animator.SetBool("Death", false);
        playerComponent.Animator.Play("Idle");
        cameraManager.CameraPlayer();
    }
}
