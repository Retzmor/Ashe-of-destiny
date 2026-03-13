using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DamageEffect : MonoBehaviour
{
    public Volume volume;
    Vignette vignette;

    void Start()
    {
        volume.profile.TryGet(out vignette);
    }

    public void TakeDamageEffect()
    {
        StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash()
    {
        vignette.intensity.value = 0.2f;

        yield return new WaitForSeconds(0.15f);

        vignette.intensity.value = 0f;
    }
}
