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
    AbilitiesPlayer abilitiesPlayer;

    [Inject] LevelController levelController;
    [Inject] Inventory inventory;

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
        abilitiesPlayer = GetComponent<AbilitiesPlayer>();
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
        playerCollisions.CanInteract = true;
    }

    public void FinishInteract(CallbackContext context)
    {
        playerCollisions.CanInteract = false; 
    }

    public void MenuSkills(CallbackContext context)
    {
        levelController.MenuSkill();
    }

    public void Button1(CallbackContext context) 
    {
        abilitiesPlayer.ButtonOne();

    }

    public void Button2(CallbackContext context) 
    {
        abilitiesPlayer.ButtonTwo();
    }


    private void FixedUpdate()
    {
        movement.Movement(inputs.Direction);
    }
}
