using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementTerrain : MonoBehaviour
{
    [Header("Componentes")]
    Rigidbody rb;
    [SerializeField] PlayerComponent playerComponent;
   // [SerializeField] Particulas particulas;
    [SerializeField] Transform cam;
    [SerializeField] Transform groundCheck;

    [Header("Movimiento")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float rotationSpeed = 10f;
    Vector3 currentMoveDir;
    private bool canSprint = false;
    private bool canMoving = true;

    [Header("Salto")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float groundDistance = 0.3f;
    public bool canJumping = true;
    public bool jump = false;

    [Header("Pendientes")]
    [SerializeField] float maxSlope = 45f; // Máximo ángulo de subida permitido

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (cam == null) cam = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        HandleRotation();
        HandleMovement();
        JumpPlayer();
    }

    void HandleRotation()
    {
        Vector3 moveDir = currentMoveDir;
        moveDir.y = 0;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void HandleMovement()
    {
        if (!canMoving) return;

        // Entrada de ejemplo: usa Input para testeo
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(h, 0f, v);

        if (inputDir.sqrMagnitude < 0.01f)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            //particulas?.DesactiveParticule();
            return;
        }

        // Calcula dirección relativa a la cámara
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0; right.y = 0;
        forward.Normalize(); right.Normalize();

        Vector3 moveDir = (forward * inputDir.z + right * inputDir.x).normalized;
        currentMoveDir = moveDir * (canSprint ? runSpeed : walkSpeed);

        // Verifica pendiente
        if (!CanMoveOnSlope(moveDir)) return;

        // Mueve el Rigidbody
        rb.linearVelocity = new Vector3(currentMoveDir.x, rb.linearVelocity.y, currentMoveDir.z);

        // Animaciones
        if (canSprint)
        {

            //particulas?.ActivasParticulasLoop();
        }
        else
        {

            //particulas?.DesactiveParticule();
        }
    }

    bool CanMoveOnSlope(Vector3 moveDir)
    {
        if (Physics.Raycast(groundCheck.position + Vector3.up * 0.1f, moveDir.normalized, out RaycastHit hitForward, 0.5f))
        {
            float slopeAngle = Vector3.Angle(hitForward.normal, Vector3.up);
            if (slopeAngle > maxSlope)
                return false;
        }

        if (Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit hitDown, groundDistance + 0.1f))
        {
            float slopeAngle = Vector3.Angle(hitDown.normal, Vector3.up);
            if (slopeAngle > maxSlope)
                return false;
        }

        return true;
    }


    void JumpPlayer()
    {
        if (!canJumping || !jump) return;

        if (IsGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(groundCheck.position, Vector3.down, groundDistance);
    }

    public void StopMovement()
    {
        rb.linearVelocity = Vector3.zero;

        //particulas?.DesactiveParticule();
    }

    // Opcionales para testing rápido
    public void SetCanMoving(bool value) => canMoving = value;
    public void SetCanJumping(bool value) => canJumping = value;
    public void SetSprint(bool value) => canSprint = value;
}
