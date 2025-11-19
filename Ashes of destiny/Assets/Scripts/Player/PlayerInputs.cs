using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] InputActionAsset actions;
    public InputAction Jump;
    public InputAction Move;
    public InputAction Sprint;
    public InputAction Pause;
    public InputAction Attack;
    public InputAction interact;
    public InputAction MenuSkills;
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
        
    }
    private void Update()
    {
        Direction = Move.ReadValue<Vector2>();
    }
}
