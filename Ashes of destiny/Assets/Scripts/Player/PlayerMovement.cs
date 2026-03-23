using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    PlayerComponent playerComponent;
    PlayerAudio playerAudio;
    AttackPlayer playerAttack;
    float speed;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float jumpForce;
    float _speedWalk = 4;
    float _speedRun = 10;
    [SerializeField] GameObject zoneJump;
    [SerializeField] GameObject zoneWalk;
    [SerializeField] float radiusJump;
    [SerializeField] float radiusWalk;
    [SerializeField] LayerMask canJump;
    [SerializeField] LayerMask canWalk;
    [SerializeField] Transform cam;
    [SerializeField] Transform yawTarget;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] Transform pivot;
    [SerializeField] Particulas particulas;
    [SerializeField] float acceleration = 18f;
    [SerializeField] float deceleration = 12f;
    [SerializeField] float fallMultiplier = 3f;
    private float attackMultiplier = 1f;
    private bool _canSprint = false;
    private bool _isJumping = true;
    internal bool isAiming;
    bool _canMoving = true;
    float _speed;
    public bool jump = false;
    Vector3 lookDirection;
    Vector3 currentMoveDir;
    public bool canJumping = true;
    public bool TutorialMovementLocked;
    bool isKnockback;

    public bool CanSprint { get => _canSprint; set => _canSprint = value; }
    public bool CanMoving { get => _canMoving; set => _canMoving = value; }
    public Rigidbody Rb { get => rb; set => rb = value; }
    public bool IsJumping { get => _isJumping; set => _isJumping = value; }
    public float SpeedWalk { get => _speedWalk; set => _speedWalk = value; }
    public float SpeedRun { get => _speedRun; set => _speedRun = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public void SetAttackMultiplier(float value) => attackMultiplier = value;

    private void OnEnable()
    {
        EventBus.GameStart += CanMovement;
    }
    private void OnDisable()
    {
        EventBus.GameStart -= CanMovement;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<PlayerAudio>();
        playerAttack = GetComponent<AttackPlayer>();
        playerComponent = GetComponent<PlayerComponent>();
        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }
    private void FixedUpdate()
    {
        if (!CanMoving)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
        if (isAiming)
        {
            Vector3 lookDirection = yawTarget.forward;
            lookDirection.y = 0;
            if (lookDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        BetterFall();
    }
    public void CanMovement()
    {
        rb.isKinematic = false;
    }
    public void Movement(Vector2 direction)
    {
        if (isKnockback)
            return;

        if (TutorialMovementLocked)
        {
            StopMovement();
            return;
        }

        if (!_canMoving)
        {
            StopMovement();
            return;
        }

        float baseSpeed = _canSprint ? _speedRun : _speedWalk;
        speed = baseSpeed * attackMultiplier;

        Vector3 inputDir = new Vector3(direction.x, 0f, direction.y);

        Vector3 forward = isAiming ? transform.forward : cam.forward;
        Vector3 right = isAiming ? transform.right : cam.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = (forward * inputDir.z) + (right * inputDir.x);
        moveDir.Normalize();

        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = moveDir * speed;

        if (inputDir.sqrMagnitude > 0.01f)
        {
            if (_canSprint)
            {
                playerComponent.Animator.SetBool("Run", true);
                playerComponent.Animator.SetBool("Walk", false);
                particulas.ActivasParticulasLoop();
            }
            else
            {
                playerComponent.Animator.SetBool("Run", false);
                playerComponent.Animator.SetBool("Walk", true);
                particulas.DesactiveParticule();
            }
            RaycastHit slopeHit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, moveDir, out slopeHit, 0.7f))
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                if (angle > 45f)
                {
                    targetVelocity = Vector3.zero;
                }
            }
            rb.linearVelocity = Vector3.Lerp(
                currentVelocity,
                new Vector3(targetVelocity.x, currentVelocity.y, targetVelocity.z),
                Time.deltaTime * acceleration
            );
            if (!isAiming)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            rb.linearVelocity = Vector3.Lerp(currentVelocity, new Vector3(0, currentVelocity.y, 0), Time.deltaTime * deceleration);
            playerComponent.Animator.SetBool("Run", false);
            playerComponent.Animator.SetBool("Walk", false);
            particulas.DesactiveParticule();
        }
        JumpPlayer();
    }

    public void JumpPlayer()
    {
        if (!canJumping) return;
        if (!jump) return;

        if (IsGrounded())
        {
            playerComponent.Animator.SetTrigger("Jump");
            playerAudio.PlayJump();
            jump = false;
        }
    }

    public void ApplyJumpForce()
    {
        if (!canJumping) return;
        Vector3 currentVelocity = rb.linearVelocity;

        rb.linearVelocity = new Vector3(currentVelocity.x, 0f, currentVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        
    }
    bool IsGrounded()
    {
        return Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, canWalk);
    }

    public void StopMovement()
    {
        rb.linearVelocity = Vector3.zero;
        playerComponent.Animator.SetBool("Run", false);
        playerComponent.Animator.SetBool("Walk", false);
    }

    void BetterFall()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    public void SetAttackSpeedMultiplier(float value)
    {
        attackMultiplier = value;
    }


    public void ApplyKnockback(Vector3 direction, float force)
    {
        isKnockback = true;
        direction.y = 0;
        direction.Normalize();
        rb.AddForce(direction * force, ForceMode.Impulse);
        StartCoroutine(KnockbackRoutine());
    }
    IEnumerator KnockbackRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        isKnockback = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(zoneJump.transform.position, radiusJump);
        Gizmos.DrawWireSphere(zoneWalk.transform.position, radiusWalk);
    }

    internal void ExternalJump(float jumpForce)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }
}
