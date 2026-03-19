using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Zenject;
using Zenject.SpaceFighter;
using static UnityEngine.InputSystem.InputAction;
public class PlayerController : MonoBehaviour
{
    [SerializeField] public PlayerInputs inputs;
    [SerializeField] PlayerMovement movement;
    [SerializeField] AttackPlayer attackPlayer;
    [SerializeField] PlayerCollisions playerCollisions;
    [SerializeField] CameraSwitcher camSwitcher;
    [SerializeField] public CinemachineInputAxisController inputAxisController;
    [SerializeField] CameraSwitcher cameraSwitcher;
    AbilitiesPlayer abilitiesPlayer;
    AimPlayer aimPlayer;
    PlayerComponent playerComponent;
    [Inject] LevelController levelController;
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
        playerComponent = GetComponent<PlayerComponent>();

    }

    private void OnDisable()
    {
        inputs.Jump.started -= Saltar;
        inputs.Sprint.started -= SprintPlayer;
        inputs.Sprint.canceled -= NoSprintPlayer;
        inputs.Pause.started -= PauseGame;
        inputs.Attack.started -= AttackPlayer;
        inputs.interact.started -= Interact;
        inputs.interact.canceled -= FinishInteract;
        inputs.MenuSkills.started -= MenuSkills;
        inputs.Boton1.started -= Button1;
        inputs.Boton2.started -= Button2;
        inputs.Boton3.started -= MeleeAttack;
    }

    public void Saltar(CallbackContext context)
    {
        if (!inputs.InputsEnabled) return;
        if (!context.started) return;
        if (!movement.canJumping) return;
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
            Ability abilityActiva = abilitiesPlayer.GetSelectedAbility();

            if (abilityActiva != null)
            {
                attackPlayer.Attack(abilityActiva);
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
        if (!context.started) return;
        if (!levelController.CanOpenMenus) return;

        levelController.MenuSkill();
        camSwitcher.OpenMenu();
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
    public void MeleeAttack(CallbackContext context)
    {
        Debug.Log($"Input recibido. Fase: {context.phase}");
        if (!inputs.InputsEnabled)
        {
            Debug.LogWarning("Input bloqueado por la variable InputsEnabled");
            return;
        }
        if (context.started)
        {
            Debug.Log("¡GOLPE DETECTADO EXITOSAMENTE!");
            attackPlayer.AttackMelee();
        }
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

    public void StopPlayer()
    {
        StartCoroutine(SlowStop());
    }

    IEnumerator SlowStop()
    {
        movement.CanMoving = false;

        while (playerComponent.Rb.linearVelocity.magnitude > 0.1f)
        {
            playerComponent.Rb.linearVelocity *= 0.6f;
            yield return new WaitForFixedUpdate();
        }

        playerComponent.Rb.linearVelocity = Vector3.zero;
        movement.GetComponent<PlayerComponent>().Animator.SetBool("Run", false);
        movement.GetComponent<PlayerComponent>().Animator.SetBool("Walk", false);

        playerComponent.Rb.isKinematic = true;
        cameraSwitcher.inputAxisController.enabled = false;
    }

    public void StartPlayer()
    {
        playerComponent.Rb.isKinematic = false;
        movement.CanMoving = true;
        cameraSwitcher.inputAxisController.enabled = true;
    }

    private void FixedUpdate()
    {
        movement.Movement(inputs.Direction);
    }
}
