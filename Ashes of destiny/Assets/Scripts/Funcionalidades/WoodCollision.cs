using UnityEngine;

public class WoodCollision : MonoBehaviour
{
    Animator[] animators;
    private void Start()
    {
        animators = GetComponentsInChildren<Animator>();
    }

    public void AnimationWoodBroke()
    {
        Debug.Log("Animacion madera");

        foreach (Animator anim in animators)
        {
            anim.SetBool("Broke", true);
        }

        Destroy(gameObject, 5f);
    }
}
