using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float speed;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject zoneJump;
    [SerializeField] GameObject zoneWalk;
    [SerializeField] float radiusJump;
    [SerializeField] float radiusWalk;
    [SerializeField] LayerMask canJump;
    [SerializeField] LayerMask canWalk;
    [SerializeField] Transform cam;
    [SerializeField] Transform yawTarget;
    private bool _canSprint = false;
    private bool isJumping = false;
    internal bool isAiming;
    bool _canMoving = false;
    Vector3 lookDirection;
    public bool CanSprint { get => _canSprint; set => _canSprint = value; }
    public bool CanMoving { get => _canMoving; set => _canMoving = value; }

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
        if(cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    public void CanMovement()
    {
        rb.isKinematic = false;
    }

    public void Movement(Vector2 direction)
    {
        if (IsGrounded() && _canMoving == true)
        {
            float speed = _canSprint ? 8f : 4f;

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

                Vector3 horizontalVelocity = moveDir * speed;
                rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);

                if (!isAiming)
                {
                    transform.forward = moveDir;
                }
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
               transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            }
            else
            {
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            }

            if (isAiming && yawTarget != null)
            {
                lookDirection = yawTarget.forward;
                lookDirection.y = 0;

                if (lookDirection.sqrMagnitude > 0.01f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
                }
            }
        }
    }

    public void JumpPlayer()
    {
        if(isJumping == true)
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Collider[] canJumpPlayer = Physics.OverlapSphere(zoneJump.transform.position,radiusJump,canJump);

        if(canJumpPlayer.Length > 0)
        {
            isJumping = true;
        }

        else
        {
            isJumping = false;
        }
    }

    bool IsGrounded()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.2f, canWalk))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);

            if (angle < 45f) 
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(zoneJump.transform.position, radiusJump);
        Gizmos.DrawWireSphere(zoneWalk.transform.position, radiusWalk);
    }
}
