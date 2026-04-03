using System.Collections;
using UnityEngine;

public class JumpTrampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private string animationName = "CaidaNivel";
    [SerializeField] private float minAirTime = 0.5f;

    [Header("Aterrizaje de Precisión")]
    [SerializeField] private Transform targetLandingPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            PlayerComponent pc = other.GetComponent<PlayerComponent>();

            if (pm != null && pc != null && targetLandingPoint != null)
            {
                StopAllCoroutines(); // Evita que se solapen saltos
                StartCoroutine(PrecisionJumpSequence(pm, pc));
            }
        }
    }

    private IEnumerator PrecisionJumpSequence(PlayerMovement player, PlayerComponent component)
    {
        player.IsTrampolineJumping = true;
        player.CanMoving = false;
        component.Animator.CrossFade(animationName, 0.1f);
        float gravity = Mathf.Abs(Physics.gravity.y);
        float timeToLand = (2f * jumpForce) / gravity;

        Vector3 startPos = player.transform.position;
        Vector3 targetPos = targetLandingPoint.position;
        Vector3 diff = targetPos - startPos;
        diff.y = 0;
        Vector3 horizontalVelocity = diff / timeToLand;

        player.Rb.linearVelocity = new Vector3(horizontalVelocity.x, jumpForce, horizontalVelocity.z);

        float elapsed = 0;
        bool grounded = false;

        // --- EL VUELO (CON TRIPLE CANDADO) ---
        while (!grounded)
        {
            elapsed += Time.deltaTime;

            // 1. Calculamos distancia horizontal al objetivo (ignorando la altura)
            float distanceToTarget = Vector2.Distance(
                new Vector2(player.transform.position.x, player.transform.position.z),
                new Vector2(targetPos.x, targetPos.z)
            );

            // 2. ¿PODEMOS ATERRIZAR? 
            // Solo preguntamos si ya pasó el tiempo mínimo Y estamos bajando (y < 0)
            if (elapsed > minAirTime && player.Rb.linearVelocity.y < 0)
            {
                // Solo tiramos el rayo si estamos a menos de 2 metros del objetivo horizontal
                // O si la distancia al suelo es muy, muy corta
                if (distanceToTarget < 2.0f)
                {
                    grounded = Physics.Raycast(player.transform.position + Vector3.up * 0.5f, Vector3.down, 1.2f);
                }
            }

            // SEGURIDAD: Si se queda trabado por más de 5 segundos, soltamos el control
            if (elapsed > 5f) grounded = true;

            yield return null;
        }

        // --- ATERRIZAJE ---
        player.IsTrampolineJumping = false;
        player.CanMoving = true;
        player.Rb.linearVelocity = Vector3.zero;

        // Lo ponemos justo en el punto X y Z del target
        player.transform.position = new Vector3(targetPos.x, player.transform.position.y, targetPos.z);

        component.Animator.CrossFade("Idle", 0.2f);
    }
}