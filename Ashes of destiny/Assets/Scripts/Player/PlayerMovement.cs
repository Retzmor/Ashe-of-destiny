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
    private bool _canSprint = false;
    private bool isJumping = false;

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
        speed = _canSprint ? 5f : 2f;

        // Mapear correctamente los ejes
        Vector3 inputDir = new Vector3(direction.x, 0f, direction.y);

        if (inputDir.sqrMagnitude < 0.01f)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }

        // Direcciones de la cámara
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        // OJO: aquí el orden importa
        Vector3 moveDir = (camForward * inputDir.z) + (camRight * inputDir.x);

        // Normalizar
        moveDir.Normalize();

        // Aplicar velocidad
        Vector3 horizontalVelocity = moveDir * speed;
        rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);

        // Rotar al jugador hacia la dirección de movimiento
        transform.forward = moveDir;
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
