using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBoss : MonoBehaviour
{
    BossController controller;
    BossAudio bossAudio;
    float _maxHealth = 100;
    float _minHealth;
    public float _currentHealth;
    private float _lastSummonHealth;
    private float _summonThreshold = 20f;
    [Inject] private DiContainer _container;
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    [SerializeField] Material materialDamage;
    [SerializeField] Image healthImage;
    [SerializeField] GameObject specialAshPrefab;
    [SerializeField] GameObject ashe;
    float meleeDamageMultiplier = 0.1f;
    private Material[] originalMaterials;
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float MinHealth { get => _minHealth; set => _minHealth = value; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }

    void Awake() 
    {
        controller = GetComponent<BossController>();
        _currentHealth = MaxHealth;
        _minHealth = _maxHealth / 2;
        _lastSummonHealth = MaxHealth;
        if (meshRenderer == null) meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (meshRenderer != null)
        {
            originalMaterials = meshRenderer.sharedMaterials; 
        }
        else
        {
        }
    }

    private void TriggerSummonPhase()
    {
        _lastSummonHealth = _currentHealth; 
        controller.Anim.SetTrigger("Invoke"); 
    }

    public void TakeDamage(float damage,bool isMelee = false)
    {
        if (_currentHealth <= 0) return;
        float finalDamage = isMelee ? damage * meleeDamageMultiplier : damage;
        controller.Anim.SetTrigger("TakeDamage");
        bossAudio.PlayFootstep();
        _currentHealth -= finalDamage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, MaxHealth);
        healthImage.fillAmount = _currentHealth / MaxHealth;
        if (_currentHealth <= _lastSummonHealth - (MaxHealth * (_summonThreshold / 100f)))
        {
            TriggerSummonPhase();
        }
        if (_currentHealth <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        bossAudio.PlayDeath();
        controller.Agent.isStopped = true;
        controller.Agent.speed = 0;
        controller.Anim.SetBool("Death", true);
        HealthEnemy[] enemies = GameObject.FindObjectsByType<HealthEnemy>(FindObjectsSortMode.None);
      foreach (HealthEnemy enemy in enemies)
        {
            enemy.TakeDamage(10000f, 1f);
        }
        ashe.SetActive(true);
        if (TryGetComponent(out Collider col)) col.enabled = false;
    }

    public void ChangeMaterial()
    {
        Material[] tempMaterials = new Material[meshRenderer.materials.Length];
        for (int i = 0; i < tempMaterials.Length; i++)
        {
            tempMaterials[i] = materialDamage;
        }
        meshRenderer.materials = tempMaterials;
    }

    public void SetNormalMaterial()
    {
        if(gameObject.activeSelf)
        meshRenderer.materials = originalMaterials;
    }

    public void ResetHealth()
    {

        _currentHealth = MaxHealth;
        _lastSummonHealth = MaxHealth; 
        healthImage.fillAmount = 1;

        if (TryGetComponent(out Collider col)) col.enabled = true;
        SetNormalMaterial();

        controller.Anim.SetBool("Death", false);
        controller.Anim.Play("Idle");
        controller.ResetBossController();
        if (ashe != null) ashe.SetActive(false);
    }
}
