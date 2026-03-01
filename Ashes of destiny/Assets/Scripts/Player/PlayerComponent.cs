using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    Animator _animator;
    Rigidbody _rb;

    public Animator Animator { get => _animator; set => _animator = value; }
    public Rigidbody Rb { get => _rb; set => _rb = value; }

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
    }
}
