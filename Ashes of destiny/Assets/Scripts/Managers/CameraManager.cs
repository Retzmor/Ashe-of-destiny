using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera cameraThirdPerson;
    [SerializeField] CinemachineCamera cameraAimPerson;
    [SerializeField] CinemachineCamera cameraAsheTutorial;
    [SerializeField] CinemachineBrain mainCamera;
    public void CameraAsheTutorial()
    {
        mainCamera.DefaultBlend.Time = 1.5f;
        cameraAsheTutorial.Priority = 20;
        cameraAimPerson.Priority = 1;
        cameraThirdPerson.Priority = 1;
    }
}
