using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera cameraThirdPerson;
    [SerializeField] CinemachineCamera cameraAimPerson;
    [SerializeField] CinemachineCamera cameraAsheTutorial;
    [SerializeField] CinemachineCamera cameraPlayerDie;
    [SerializeField] CinemachineBrain mainCamera;
    public void CameraAsheTutorial()
    {
        mainCamera.DefaultBlend.Time = 2f;
        cameraAsheTutorial.Priority = 20;
        cameraAimPerson.Priority = 1;
        cameraThirdPerson.Priority = 1;
    }

    public void CameraPlayer()
    {
        mainCamera.DefaultBlend.Time = 2f;
        cameraAsheTutorial.Priority = 1;
        cameraAimPerson.Priority = 2;
        cameraThirdPerson.Priority = 3;
    }

    public void CameraDie()
    {
        mainCamera.DefaultBlend.Time = 2f;
        cameraAsheTutorial.Priority = 1;
        cameraAimPerson.Priority = 1;
        cameraThirdPerson.Priority = 1;
        cameraPlayerDie.Priority = 2;
    }
}
