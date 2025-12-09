using UnityEngine;

public class BaseUIPanel : MonoBehaviour, IUIPanel
{
    Animator anim;
    public virtual void Start()
    {
        TryGetComponent(out anim);
    }

    public virtual void Show()
    {
        anim?.Play("Show");
    }
    public virtual void Hide()
    {
        anim?.Play("Hide");
    }

    public float GetAnimationDuration()
    {
        return anim ? anim.GetCurrentAnimatorStateInfo(0).length : 0f;
    }
}

