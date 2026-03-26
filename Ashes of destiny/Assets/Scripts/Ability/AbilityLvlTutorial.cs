using UnityEngine;

public class AbilityLvlTutorial : MonoBehaviour
{
    [SerializeField] Ability air, fire;
    private void Start()
    {
        air.isInfinite = true;
        fire.isInfinite = true;
    }
}
