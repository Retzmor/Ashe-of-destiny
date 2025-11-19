using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Debug.Log(gameObject.name);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * speed;
    }
}
