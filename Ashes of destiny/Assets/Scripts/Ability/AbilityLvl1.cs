using UnityEngine;

public class AbilityLvl1 : MonoBehaviour
{
    [SerializeField] Ability air, fire;
    private void Start()
    {
        air.isInfinite = false;
        fire.isInfinite = false;
    }
}
