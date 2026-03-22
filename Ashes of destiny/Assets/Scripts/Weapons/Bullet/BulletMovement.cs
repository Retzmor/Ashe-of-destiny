using System.Collections;
using UnityEngine;
using Zenject;

public class BulletMovement : MonoBehaviour
{
    Rigidbody rb;
    Collider colliderBullet;

    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float rotationSpeed = 800f;
    [SerializeField] float forceImpulse;
    ParticleSystem particle;
    private Ability _myAbilityData;
    bool alreadyDamage = false;

    [Inject] PlayerCollisions player;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        particle = GetComponentInChildren<ParticleSystem>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
    void Start()
    {
        colliderBullet = GetComponent<Collider>();

        foreach (Collider col in player.GetComponentsInChildren<Collider>())
        {
            Physics.IgnoreCollision(colliderBullet, col);
        }


        Destroy(gameObject, 3f);
    }

    public void SetDirection(Vector3 dir)
    {
        dir.Normalize();
        transform.forward = dir;
        rb.linearVelocity = dir * speed;
        particle.Play(true);
    }

    public void SetupBullet(Ability data)
    {
        _myAbilityData = data;
        // Opcional: Puedes ajustar el daño o la velocidad aquí basado en el ScriptableObject
        // this.damage = data.damage; 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (alreadyDamage) return;

        if (other.CompareTag("Enemy"))
        {
            alreadyDamage = true;

            if (other.TryGetComponent(out HealthEnemy healthEnemy))
            {
                healthEnemy.TakeDamage(damage, forceImpulse);
            }

            if (_myAbilityData != null && other.TryGetComponent(out EnemyStatus status))
            {
                // Le pasamos el nombre ("Fire", "Water", etc.) y su icono
                status.ApplyElement(_myAbilityData.abilityName, _myAbilityData.icon);
            }
            ApplyUniqueElementEffect(other.gameObject);
        }
    }

    private void ApplyUniqueElementEffect(GameObject enemy)
    {
        if (_myAbilityData == null) return;

        switch (_myAbilityData.abilityName)
        {
            case "Fire":
                // Quema básica: 2 segundos de daño extra
                if (enemy.TryGetComponent(out EnemyStatus stFire))
                    stFire.StartCoroutine(stFire.BurnRoutine(2f, 5f));
                break;

            case "Water":
                if (enemy.TryGetComponent(out UnityEngine.AI.NavMeshAgent agent))
                    StartCoroutine(SlowRoutine(agent, 2f));
                break;

            case "Rock":
                // Stun: El enemigo se queda quieto
                if (enemy.TryGetComponent(out EnemyKnockback stRock))
                    stRock.Stun(1.5f); // Necesitarías crear esta función en tu script de Knockback
                break;
        }
    }

    IEnumerator SlowRoutine(UnityEngine.AI.NavMeshAgent agent, float duration)
    {
        float originalSpeed = agent.speed;
        agent.speed *= 0.5f; // Reduce a la mitad
        yield return new WaitForSeconds(duration);
        if (agent != null) agent.speed = originalSpeed;
    }
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
    }
}