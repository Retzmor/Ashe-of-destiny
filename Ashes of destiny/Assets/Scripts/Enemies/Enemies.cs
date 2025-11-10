using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    protected NavMeshAgent _agent;
    protected Animator _anim;
    protected float health;
    protected float currentHealth;
    protected float maxHealth;
    protected Vector3 startPosition;
    

    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Animator Anim { get => _anim; set => _anim = value; }

    protected void Start()
    {
        TryGetComponent(out _agent);
        TryGetComponent(out _anim);
        startPosition = transform.position;
        currentHealth = health;
    }
}
