using UnityEngine;
using Zenject;

public class BulletMovement : MonoBehaviour
{
    Rigidbody rb;
    Collider colliderBullet;
    Collider colliderPlayer;
    [SerializeField] float speed;

    [Inject] PlayerCollisions player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliderBullet = GetComponent<Collider>();
        colliderPlayer = player.GetComponent<Collider>();
        rb.useGravity = false;
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, 5f);
        foreach (Collider col in player.GetComponentsInChildren<Collider>())
        {
            Physics.IgnoreCollision(colliderBullet, col);
        }
    }
}
