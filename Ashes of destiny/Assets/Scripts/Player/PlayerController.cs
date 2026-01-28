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
    [SerializeField] Button boton1;
    [SerializeField] Button boton2;
    [SerializeField] private Button botonSeleccionado;

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
        inputs.Boton1.started += Boton1Click;
        inputs.Boton2.started += Boton2Click;
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
       attackPlayer.Attack(botonSeleccionado);
        Debug.Log("ataque");
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

    public void Boton1Click(CallbackContext context)
    {
        inventory.ClickBoton1();
        botonSeleccionado = boton1;
    }

    public void Boton2Click(CallbackContext context)
    {
        inventory.ClickBoton2();
        botonSeleccionado = boton2;
    }

    private void FixedUpdate()
    {
        movement.Movement(inputs.Direction);
    }
}
