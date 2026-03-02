using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimCameraController : MonoBehaviour
{
    [SerializeField] Transform yawTarget;
    [SerializeField] Transform pitchTarget;
    [SerializeField] InputActionReference lookInput;
    [SerializeField] InputActionReference switchShouldInput;
    [SerializeField] float mouseSensibility = 0.5f;
    //[SerializeField] float gamepadSensibility = 0.5f;
    [SerializeField] float sensibility = 1.5f;
    [SerializeField] CinemachineThirdPersonFollow aimCamera;
    [SerializeField] float shoulderSwitchSpeed = 5;

    float yaw;
    float pitch;
    float targetCameraSide;

    private void Awake()
    {
        aimCamera = GetComponent<CinemachineThirdPersonFollow>();  
        targetCameraSide = aimCamera.CameraSide;
    }

    void Start()
    {
        Vector3 angles = yawTarget.rotation.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
        lookInput.asset.Enable();
    }

    private void OnEnable()
    {
        switchShouldInput.action.Enable();
        switchShouldInput.action.performed += OnSwitchShoulder;
    }

    private void OnDisable()
    {
        switchShouldInput.action.Disable();
        switchShouldInput.action.performed -= OnSwitchShoulder;
    }

    void Update()
    {

        Vector2 look = lookInput.action.ReadValue<Vector2>();
        
        if(Mouse.current != null && Mouse.current.delta.IsActuated())
        {
            look *= mouseSensibility;
        }

        //else if(Gamepad.current != null && Gamepad.current.rightStick.IsActuated())
        //{                    
        //  look *= gamepadSensibility;                                                         aun no tenemos gamepad para ver si funciona, por lo que saldra despues de la beta
        //}

        yaw += look.x * sensibility;
        pitch -= look.y * sensibility;
        yawTarget.rotation = Quaternion.Euler(0, yaw, 0);
        pitchTarget.localRotation = Quaternion.Euler(pitch, 0, 0);
        aimCamera.CameraSide = Mathf.Lerp(aimCamera.CameraSide, targetCameraSide, Time.deltaTime * shoulderSwitchSpeed);
    }

    public void OnSwitchShoulder(InputAction.CallbackContext context)
    {
        targetCameraSide = aimCamera.CameraSide < 0.5f ? 1f : 0f;
    }

    public void SetYawPitchFromCameraForward(Transform cameraTransform)
    {
        Vector3 flatForward = cameraTransform.forward;
        flatForward.y = 0f;

        if (flatForward.sqrMagnitude < 0.001f)
            return;

        yaw = Quaternion.LookRotation(flatForward).eulerAngles.y;
        pitch = 0f; 

        yawTarget.rotation = Quaternion.Euler(0, yaw, 0);
        pitchTarget.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
