using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, 5f);
    }
}
