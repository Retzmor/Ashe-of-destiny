using UnityEngine;

public class Particulas : MonoBehaviour
{
    [SerializeField] ParticleSystem particulas;
    public void ActivasParticulas()
    {
        particulas.Play();
        Debug.Log("sjfhksaj");
    }

    private void Start()
    {
        particulas.Stop();
    }
}
