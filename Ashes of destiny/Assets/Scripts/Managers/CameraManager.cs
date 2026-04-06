using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera cameraThirdPerson;
    [SerializeField] CinemachineCamera cameraAimPerson;
    [SerializeField] CinemachineCamera cameraAsheTutorial;
    [SerializeField] CinemachineCamera cameraPlayerDie;
    [SerializeField] CinemachineBrain mainCamera;
    [SerializeField] CinemachineCamera cameraLvlOne;
    [SerializeField] CinemachineCamera cameraPlayerFall;
    [SerializeField] CameraSwitcher cameraSwitcher;
    [SerializeField] PlayerMovement player;
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

    public void CameraLevelOne()
    {
        mainCamera.DefaultBlend.Time = 5f;
        cameraLvlOne.Priority = 2;
        cameraThirdPerson.Priority= 1;
        StartCoroutine(ChangeCameraLevelOne());
    }

    IEnumerator ChangeCameraLevelOne()
    {
        yield return new WaitForSeconds(5);
        CameraLevelOnePlayer();
    }

    public void CameraLevelOnePlayer()
    {
        mainCamera.DefaultBlend.Time = 5f;
        cameraLvlOne.Priority = 1;
        cameraThirdPerson.Priority = 2;
        player.CanMoving = true;
    }

    public void CameraFall()
    {
        if (cameraSwitcher != null) cameraSwitcher.DisableCameraInput();
        if (player != null) player.CanMoving = false;
        mainCamera.DefaultBlend.Time = 1f;
        cameraPlayerFall.Priority = 100;
        cameraThirdPerson.Priority = 2;
        StartCoroutine(ChangeCameraFallPlayer());
    }

    IEnumerator ChangeCameraFallPlayer()
    {
        yield return new WaitForSeconds(5);
        ChangeCameraFall();
    }

    public void ChangeCameraFall()
    {
        mainCamera.DefaultBlend.Time = 1f;
        cameraPlayerFall.Priority = 1;
        cameraThirdPerson.Priority = 10;
        if (cameraSwitcher != null)
        {
            cameraThirdPerson.ForceCameraPosition(cameraPlayerFall.transform.position, cameraPlayerFall.transform.rotation);
            cameraSwitcher.EnableCameraInput();
        }
        if (player != null) player.CanMoving = true;
    }
}
