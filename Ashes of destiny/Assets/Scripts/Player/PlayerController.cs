using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static UnityEngine.InputSystem.InputAction;
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInputs inputs;
    [SerializeField] PlayerMovement movement;
    [SerializeField] AttackPlayer attackPlayer;
    [SerializeField] PlayerCollisions playerCollisions;
    [SerializeField] CameraSwitcher camSwitcher;
    AbilitiesPlayer abilitiesPlayer;
    AimPlayer aimPlayer;

    [Inject] LevelController levelController;
    [Inject] Inventory inventory;
    [Inject] TutorialManager tutorialManager;
    internal bool isAiming;

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
        inputs.Boton3.started += Button3;
       // inputs.Aim.started += AimButton;
        //inputs.Aim.canceled += AimButton;
        abilitiesPlayer = GetComponent<AbilitiesPlayer>();
        aimPlayer = GetComponent<AimPlayer>();
    }

    public void Saltar(CallbackContext context)
    {
        movement.JumpPlayer();
    }

    public void SprintPlayer(CallbackContext context)
    {
        movement.CanSprint = true;
    }

    public void NoSprintPlayer(CallbackContext context)
    {
        movement.CanSprint = false;
    }

    public void PauseGame(CallbackContext context)
    {
        camSwitcher.OpenMenu();
        levelController.PauseGame();
    }

    public void AttackPlayer(CallbackContext context)
    {
        Ashes ashesActiva = abilitiesPlayer.GetSelectedAshes();
        if (ashesActiva == null)
            return;
        attackPlayer.Attack(ashesActiva);
    }

    public void Interact(CallbackContext context)
    {
        if (context.started)
            playerCollisions.TryInteract();
    }

    public void FinishInteract(CallbackContext context)
    {
        playerCollisions.CanInteract = false; 
    }

    public void MenuSkills(CallbackContext context)
    {
        camSwitcher.OpenMenu();
        levelController.MenuSkill();
        tutorialManager.DesactivePanelTutorial();
    }

    public void Button1(CallbackContext context)
    {
        if (!context.started) return;
        abilitiesPlayer.ButtonOne();
    }

    public void Button2(CallbackContext context)
    {
        if (!context.started) return;
        abilitiesPlayer.ButtonTwo();
    }

    public void Button3(CallbackContext context)
    {
        attackPlayer.ToggleMeleeMode();
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
