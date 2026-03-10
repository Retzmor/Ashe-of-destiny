using UnityEngine;

public class Ashes : MonoBehaviour
{
    [SerializeField] public Animator rock;
    [SerializeField] GameObject _elementAttack;
    [SerializeField] private ParticleSystem particulaPrefab;
    public ParticleSystem ParticulaPrefab => particulaPrefab;

    public GameObject ElementAttack { get => _elementAttack; set => _elementAttack = value; }

    private void Start()
    {
        rock.SetBool("Take", false);
    }
    public void DesactiveRock()
    {
        rock.SetBool("Take", true);
    }
}
