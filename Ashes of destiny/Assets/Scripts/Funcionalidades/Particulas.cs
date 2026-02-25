using UnityEngine;

public class Particulas : MonoBehaviour
{
    public ParticleSystem particulas; 

    public void ActivasParticulas()
    {
        if (particulas == null) return;
        particulas.Play();
    }

    public void ActivasParticulasLoop()
    {
        if (particulas == null) return;

        var main = particulas.main;
        main.loop = true;
        particulas.Play();
    }

    public void DesactiveParticule()
    {
        if (particulas == null) return;

        var main = particulas.main;
        main.loop = false;
        particulas.Stop();
    }
}
