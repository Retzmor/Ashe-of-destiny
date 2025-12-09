using UnityEngine;

public class Ashes : MonoBehaviour
{
    [SerializeField] Animator rock;

    private void Start()
    {
        rock.SetBool("Take", false);
    }
    public void DesactiveRock()
    {
        Debug.Log("Animacion");
        rock.SetBool("Take", true);
    }
}
