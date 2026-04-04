using UnityEngine;

public class HealthBoss : MonoBehaviour
{
    BossController controller;
    float _maxHealth = 100;
    float _minHealth;
    float _currentHealth;
    private float _lastSummonHealth;
    private float _summonThreshold = 20f;
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    [SerializeField] Material materialDamage;
    private Material[] originalMaterials;
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float MinHealth { get => _minHealth; set => _minHealth = value; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }

    void Start()
    {
        controller = GetComponent<BossController>();
        _currentHealth = MaxHealth;
        _minHealth = _maxHealth / 2;
        _lastSummonHealth = MaxHealth;
        originalMaterials = meshRenderer.materials;
    }

    public void TakeDamage(float damage)
    {
        controller.Anim.SetTrigger("TakeDamage");
        _currentHealth -= damage;
        if (_currentHealth <= _lastSummonHealth - (MaxHealth * (_summonThreshold / 100f)))
        {
            TriggerSummonPhase();
        }
        if ( _currentHealth < 0 )
        {
            //Death();
        }
    }

    private void TriggerSummonPhase()
    {
        _lastSummonHealth = _currentHealth; 
        controller.Anim.SetTrigger("Invoke"); 
    }

    public void Death()
    {
        gameObject.SetActive(false);
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
        meshRenderer.materials = originalMaterials;
    }
}
