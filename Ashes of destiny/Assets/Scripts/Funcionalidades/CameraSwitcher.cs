using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] CinemachineCamera freelookCam;
    [SerializeField] CinemachineCamera aimCam;
    [SerializeField] public CinemachineInputAxisController inputAxisController;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject crossHairUI;
    [SerializeField] PlayerMovement playerController;
    [SerializeField] PlayerControls input;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] CinemachineBrain cinemachineBrain;


    InputAction aimAction;
    bool isAiming = false;
    Transform yawTarget;
    Transform pitchTarget;


    AimCameraController aimCamController;
    void Start()
    {
        aimCamController = aimCam.GetComponent<AimCameraController>();
        inputAxisController = freelookCam.GetComponent<CinemachineInputAxisController>();
        input = new PlayerControls();
        input.Enable();
        aimAction = input.Player.Aim;
        crossHairUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool aimPressed = aimAction.IsPressed();
        playerController.isAiming = aimPressed;

        if(!isAiming && aimPressed)
        {
            EnterAimMode();
        }

        else if (isAiming && !aimPressed)
        {
            ExitAimMode();
        }
    }

    private void ExitAimMode()
    {
        cinemachineBrain.DefaultBlend.Time = 0.5f;
        crossHairUI?.SetActive(false);
        isAiming = false;
        SnapFreelookBehindPlayer();
        aimCam.Priority = 10;
        freelookCam.Priority = 20;
        inputAxisController.enabled = true;

    }

    private void SnapFreelookBehindPlayer()
    {
        CinemachineOrbitalFollow orbitalFollow = freelookCam.GetComponent<CinemachineOrbitalFollow>();
        Vector3 forward = aimCam.transform.forward;
        float angle = Mathf.Atan2(forward.x, forward.z) * Mathf.Rad2Deg;
        orbitalFollow.HorizontalAxis.Value = angle;
    }

    private void SnapAimCameraToPlayerForward()
    {
        aimCamController.SetYawPitchFromCameraForward(freelookCam.transform);
    }

    private void EnterAimMode()
    {
        cinemachineBrain.DefaultBlend.Time = 0.5f;
        crossHairUI.gameObject.SetActive(true);
        isAiming = true;
        SnapAimCameraToPlayerForward(); 
        aimCam.Priority = 20;
        freelookCam.Priority = 10;
        inputAxisController.enabled = false;
    }

    public void OpenMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMenu()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
