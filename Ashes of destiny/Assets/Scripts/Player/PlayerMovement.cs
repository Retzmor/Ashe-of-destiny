using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    PlayerComponent playerComponent;
    float speed;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float jumpForce;
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
    [SerializeField] float aimRotationSpeed = 720f;
    [SerializeField] Particulas particulas;
    private bool _canSprint = false;
    private bool _isJumping = true;
    internal bool isAiming;
    bool _canMoving = true;
    public bool jump = false;
    Vector3 lookDirection;
    Vector3 currentMoveDir;
    public bool canJumping = true;
    [SerializeField] float maxSlopeAngle = 45f;
    public bool TutorialMovementLocked;

    public bool CanSprint { get => _canSprint; set => _canSprint = value; }
    public bool CanMoving { get => _canMoving; set => _canMoving = value; }
    public Rigidbody Rb { get => rb; set => rb = value; }
    public bool IsJumping { get => _isJumping; set => _isJumping = value; }

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
        playerComponent = GetComponent<PlayerComponent>();
        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    private void FixedUpdate()
    {
        if (isAiming)
        {
            Vector3 lookDirection = yawTarget.forward;
            lookDirection.y = 0;
            if(lookDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,Time.deltaTime * 10f);
            }
        }
    }
    public void CanMovement()
    {
        rb.isKinematic = false;
    }

    public void Movement(Vector2 direction)
    {
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
        if (_canMoving)
        {
            float speed = _canSprint ? 10f : 4f;

            if (_canSprint && direction.sqrMagnitude > 0.01f)
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

            Vector3 inputDir = new Vector3(direction.x, 0f, direction.y);

            if (inputDir.sqrMagnitude > 0.01f)
            {
                Vector3 forward = isAiming ? transform.forward : cam.forward;
                Vector3 right = isAiming ? transform.right : cam.right;

                forward.y = 0;
                right.y = 0;

                forward.Normalize();
                right.Normalize();

                Vector3 moveDir = (forward * inputDir.z) + (right * inputDir.x);
                moveDir.Normalize();

                currentMoveDir = moveDir * speed;

                if (Rb.isKinematic) return;

                rb.linearVelocity = new Vector3(
                    currentMoveDir.x,
                    Rb.linearVelocity.y,
                    currentMoveDir.z
                );

                if (!isAiming)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                    transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                rb.linearVelocity = new Vector3(
                    0,
                    Rb.linearVelocity.y,
                    0
                );

                playerComponent.Animator.SetBool("Walk", false);
            }
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
            jump = false;
        }
    }

    public void ApplyJumpForce()
    {
        if (!canJumping) return;
        Vector3 currentVelocity = rb.linearVelocity;

        rb.linearVelocity = new Vector3(
            currentVelocity.x,
            0f,
            currentVelocity.z
        );

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(zoneJump.transform.position, radiusJump);
        Gizmos.DrawWireSphere(zoneWalk.transform.position, radiusWalk);
    }
}
