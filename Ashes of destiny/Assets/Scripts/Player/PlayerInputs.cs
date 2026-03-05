using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public bool InputsEnabled = true;
    [SerializeField] InputActionAsset actions;
    public InputAction Jump;
    public InputAction Move;
    public InputAction Sprint;
    public InputAction Pause;
    public InputAction Attack;
    public InputAction interact;
    public InputAction MenuSkills;
    public InputAction Boton1;
    public InputAction Boton2;
    public InputAction Boton3;
    public InputAction Aim;
    public Vector2 Direction;

    private void Awake()
    {
        Move = actions.FindAction("Move");
        Jump = actions.FindAction("Jump");
        Sprint = actions.FindAction("Sprint");
        Pause = actions.FindAction("Pause");
        Attack = actions.FindAction("Attack");
        interact = actions.FindAction("Interact");
        MenuSkills = actions.FindAction("MenuSkills");
        Boton1 = actions.FindAction("Boton1");
        Boton2 = actions.FindAction("Boton2");
        Boton3 = actions.FindAction("Boton3");
        Aim = actions.FindAction("Aim");
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void Update()
    {
        if (!InputsEnabled)
        {
            Direction = Vector2.zero;
            return;
        }

        Direction = Move.ReadValue<Vector2>();
    }

    public void EnableInputs()
    {
        actions.Enable();
    }

    public void DisableInputs()
    {
        actions.Disable();
    }

}
