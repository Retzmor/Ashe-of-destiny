using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
public class BossController : MonoBehaviour
{
    [Inject] PlayerCollisions _player;
    NavMeshAgent _agent;
    Rigidbody _rb;
    Animator _anim;
    private bool isStunned = false;
    public float distanceToBasicAttack = 1.5f;
    public float distanceToJumpAttack = 12f;
    [SerializeField] private float explosionRadius = 10f;
    [SerializeField] private float explosionForce;
    [SerializeField] private GameObject explosionParticles;
    public GameObject[] manualMinions;
    public Transform[] spawnPoints;
    public GameObject summonParticles;
    public ParticleSystem particule;
    BossAudio bossAudio;
    public PlayerCollisions Player { get => _player; set => _player = value; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Rigidbody Rb { get => _rb; set => _rb = value; }
    public Animator Anim { get => _anim; set => _anim = value; }

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    public void OnBossLandDamage()
    {
        particule.Play();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        bossAudio.PlayDamage();
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Vector3 pushDir = hitCollider.transform.position - transform.position;
                pushDir.y = 0;

                PlayerMovement pm = hitCollider.GetComponent<PlayerMovement>();
                if (pm != null)
                {
                    pm.ApplyKnockback(pushDir.normalized, explosionForce);
                }
            }
        }
    }

    public void SummonFromPool()
    {
        ShuffleArray(spawnPoints);
        int spawnedCount = 0;

        for (int i = 0; i < manualMinions.Length; i++)
        {
            if (!manualMinions[i].activeInHierarchy)
            {
                Vector3 targetPos = spawnPoints[spawnedCount].position;
                StartCoroutine(SpawnSequence(manualMinions[i], targetPos));
                spawnedCount++;
                if (spawnedCount >= 3) break; 
            }
        }
    }

    private IEnumerator SpawnSequence(GameObject minion, Vector3 position)
    {
        if (summonParticles != null)
            Instantiate(summonParticles, position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        NavMeshAgent agent = minion.GetComponent<NavMeshAgent>();
        if (agent != null) agent.enabled = false;
        minion.transform.position = position;
        minion.SetActive(true);
        yield return new WaitForEndOfFrame(); 
        if (agent != null)
        {
            agent.enabled = true;
            agent.Warp(position);
        }
    }
    void ShuffleArray(Transform[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            Transform temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    internal void ApplyStun(float duration)
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(BossStunCoroutine(duration));
    }

    IEnumerator BossStunCoroutine(float duration)
    {
        isStunned = true;
        // Si usas NavMeshAgent en el Boss:
        if (_agent != null && _agent.isOnNavMesh)
        {
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
        }
        yield return new WaitForSeconds(duration);

        if (_agent != null && _agent.isOnNavMesh)
        {
            _agent.isStopped = false;
        }

        isStunned = false;
    }
}
