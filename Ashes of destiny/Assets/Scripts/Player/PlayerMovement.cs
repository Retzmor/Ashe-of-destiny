using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float speed;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject zoneJump;
    [SerializeField] float radiusJump;
    [SerializeField] LayerMask canJump;
    [SerializeField] Transform cam;
    [SerializeField] Transform yawTarget;
    private bool _canSprint = false;
    private bool isJumping = false;
    internal bool isAiming;

    public bool CanSprint { get => _canSprint; set => _canSprint = value; }

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
        float speed = _canSprint ? 5f : 2f;

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
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        if (isAiming && yawTarget != null)
        {
            Vector3 lookDirection = yawTarget.forward;
            lookDirection.y = 0;

            if (lookDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    public void JumpPlayer()
    {
        Debug.Log("Boton presionado ");
        if(isJumping == true)
        {
            Debug.Log("Salto");
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        //transform.rotation = new Quaternion(transform.rotation.x,-cam.rotation.y,transform.rotation.z,transform.rotation.w);

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(zoneJump.transform.position, radiusJump);
    }
}
