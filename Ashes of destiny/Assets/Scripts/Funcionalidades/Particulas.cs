using Unity.Android.Gradle;
using UnityEngine;

public class Particulas : MonoBehaviour
{
    [SerializeField] ParticleSystem particulas;

    private void Start()
    {
        particulas.Stop();
    }
    public void ActivasParticulas()
    {
        particulas.Play();
    }

    public void ActivasParticulasLoop()
    {
        var main = particulas.main;
        particulas.Play();
        main.loop = true;
    }

    public void DesactiveParticule()
    {
        var main = particulas.main;
        particulas.Play();
        main.loop = false;
        particulas.Stop();
    }
}
