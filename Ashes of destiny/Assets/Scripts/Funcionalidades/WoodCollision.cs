using UnityEngine;

public class WoodCollision : MonoBehaviour
{
    Rigidbody[] rbs;
    Collider[] colliders;
    private bool isBroken = false;

    [SerializeField] int brokenWoodLayer = 8;

    private void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody>(true);
        colliders = GetComponentsInChildren<Collider>(true);
    }

    public void AnimationWoodBroke()
    {
        if (isBroken) return;
        isBroken = true;

        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].isKinematic = false;
            rbs[i].useGravity = true;
            colliders[i].gameObject.layer = brokenWoodLayer;
        }

        Destroy(gameObject, 5f);
    }
}