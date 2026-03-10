using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Zenject;
using static UnityEngine.InputSystem.InputAction;
public class PlayerController : MonoBehaviour
{
    [SerializeField] public PlayerInputs inputs;
    [SerializeField] PlayerMovement movement;
    [SerializeField] AttackPlayer attackPlayer;
    [SerializeField] PlayerCollisions playerCollisions;
    [SerializeField] CameraSwitcher camSwitcher;
    [SerializeField] public CinemachineInputAxisController inputAxisController;
    AbilitiesPlayer abilitiesPlayer;
    AimPlayer aimPlayer;
    [Inject] LevelController levelController;
    [Inject] Inventory inventory;
    [Inject] TutorialManager tutorialManager;
    internal bool isAiming;
    private bool inputsLocked;

    private void Start()
    {
        inputs.Jump.started += Saltar;
        inputs.Sprint.started += SprintPlayer;
        inputs.Sprint.canceled += NoSprintPlayer;
        inputs.Pause.started += PauseGame;
        inputs.Attack.started += AttackPlayer;
        inputs.interact.started += Interact;
        inputs.interact.canceled += FinishInteract;
        inputs.MenuSkills.started += MenuSkills;
        inputs.Boton1.started += Button1;
        inputs.Boton2.started += Button2;
        inputs.Boton3.started += MeleeAttack;
        // inputs.Aim.started += AimButton;
        //inputs.Aim.canceled += AimButton;
        abilitiesPlayer = GetComponent<AbilitiesPlayer>();
        aimPlayer = GetComponent<AimPlayer>();
    }

    public void Saltar(CallbackContext context)
    {
        if (!inputs.InputsEnabled) return;
        if (!context.started) return;

        movement.jump = true;
    }

    public void SprintPlayer(CallbackContext context)
    {
        if (!inputs.InputsEnabled) return;
        if (!context.started) return;
        movement.CanSprint = true;
    }

    public void NoSprintPlayer(CallbackContext context)
    {
        if (!inputs.InputsEnabled) return;
        if (!context.canceled) return;

        movement.CanSprint = false;
    }


    public void PauseGame(CallbackContext context)
    {
        if (!inputs.InputsEnabled) return;
    if (!context.started) return;
        camSwitcher.OpenMenu();
        levelController.PauseGame();
        camSwitcher.DisableCameraInput();
    }

    public void AttackPlayer(CallbackContext context)
    {
        if (!inputs.InputsEnabled) return;
        if (!context.started) return;
        if (inputsLocked) return;
        if (movement.isAiming)
        {
            Ashes ashesActiva = abilitiesPlayer.GetSelectedAshes();

            if (ashesActiva != null)
            {
                attackPlayer.Attack(ashesActiva);
            }
        }
        else
        {
            attackPlayer.AttackMelee();
        }
    }

    public void Interact(CallbackContext context)
    {
        if (!inputs.InputsEnabled) return;
        if (context.started)
            playerCollisions.TryInteract();
    }

    public void FinishInteract(CallbackContext context)
    {
        if (!inputs.InputsEnabled) return;
        if (!context.started) return;
        playerCollisions.CanInteract = false; 
    }

    public void MenuSkills(CallbackContext context)
    {
        if (!inputs.InputsEnabled) return;
        if (!context.started) return;
        levelController.MenuSkill();   
        camSwitcher.OpenMenu();
    }

    public void Button1(CallbackContext context)
    {
        if (!inputs.InputsEnabled) return;
        if (!context.started) return;
        abilitiesPlayer.ButtonOne();
    }

    public void Button2(CallbackContext context)
    {
        if (!inputs.InputsEnabled) return;
        if (!context.started) return;
        abilitiesPlayer.ButtonTwo();
    }
    public void MeleeAttack(CallbackContext context)
    {
        if (!inputs.InputsEnabled) return;
        if (!context.started) return;
        attackPlayer.AttackMelee();
    }
    public void DisableInputs()
    {
        inputs.DisableInputs();
        camSwitcher.DisableCameraInput();
        inputs.enabled = false;
        inputsLocked = true;
    }

    public void EnableInputs()
    {
        inputs.EnableInputs();
        camSwitcher.EnableCameraInput();
        inputs.enabled = true;
        inputsLocked = false;
    }

    public void OnPickAshAnimationEnd()
    {
       EnableInputs();
    }
    //public void AimButton(CallbackCon
    //context)
    //{
    //    
    //}


    private void FixedUpdate()
    {
        movement.Movement(inputs.Direction);
    }
}
