using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class BossController : MonoBehaviour
{
    [Inject] PlayerCollisions _player;
    NavMeshAgent _agent;
    Rigidbody _rb;
    Animator _anim;
    public float distanceToBasicAttack = 1.5f;
    public float distanceToJumpAttack = 12f;

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
}
