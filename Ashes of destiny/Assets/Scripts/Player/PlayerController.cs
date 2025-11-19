using UnityEngine;
using Zenject;
using static UnityEngine.InputSystem.InputAction;
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInputs inputs;
    [SerializeField] PlayerMovement movement;
    [SerializeField] AttackPlayer attackPlayer;
    [SerializeField] PlayerCollisions playerCollisions;

    [Inject] LevelController levelController;

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
       attackPlayer.Attack();
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

    private void FixedUpdate()
    {
        movement.Movement(inputs.Direction);
    }
}
