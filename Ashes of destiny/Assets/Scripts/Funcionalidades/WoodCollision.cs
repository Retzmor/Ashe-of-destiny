using UnityEngine;

public class WoodCollision : MonoBehaviour
{
    Rigidbody[] rb;
    private void Start()
    {
        rb = GetComponentsInChildren<Rigidbody>();
    }

    public void AnimationWoodBroke()
    {
        Debug.Log("Animacion madera");

        foreach (Rigidbody rb in rb)
        {
            rb.isKinematic = false;
        }
        Destroy(gameObject, 5f);
    }
}
