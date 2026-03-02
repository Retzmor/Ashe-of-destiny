using Unity.Cinemachine;
using UnityEngine;

public class AimPlayer : MonoBehaviour
{
    [SerializeField] CinemachineCamera camMain;
    [SerializeField] CinemachineCamera camAim;
    public void AimActive()
    {
        camAim.gameObject.SetActive(true);
        camMain.gameObject.SetActive(false);
    }

    public void AimDesactive()
    {
        camMain.gameObject.SetActive(true);
        camAim.gameObject.SetActive(false);
    }
}
