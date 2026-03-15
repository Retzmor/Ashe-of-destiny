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
    private Rigidbody _rb;

    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Animator Anim { get => _anim; set => _anim = value; }
    protected Rigidbody Rb { get => _rb; set => _rb = value; }

    protected void Start()
    {
        TryGetComponent(out _agent);
        TryGetComponent(out _anim);
        TryGetComponent(out _rb);
        startPosition = transform.position;
        currentHealth = health;
        _rb.isKinematic = true;
        _rb.useGravity = false;
    }
}
