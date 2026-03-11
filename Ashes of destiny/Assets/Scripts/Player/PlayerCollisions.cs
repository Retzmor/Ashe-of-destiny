using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerCollisions : MonoBehaviour
{
    [Inject] Inventory inventory;
    [SerializeField] Animator animator;
    [SerializeField] TutorialController controller;
    bool _canInteract = false;
    AttackPlayer attackPlayer;
    AbilitiesPlayer abilitiesPlayer;
    HealthPlayer healthPlayer;
    PlayerComponent playerComponent;
    PlayerController playerController;
    PlayerMovement playerMovement;


    private void Start()
    {
        attackPlayer = GetComponent<AttackPlayer>();
        abilitiesPlayer = GetComponent<AbilitiesPlayer>();
        healthPlayer = GetComponent<HealthPlayer>();
        playerComponent = GetComponent<PlayerComponent>();
        playerController = GetComponent<PlayerController>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public bool CanInteract { get => _canInteract; set => _canInteract = value; }
    GameObject currentItem;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ceniza"))
        {
            currentItem = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ceniza"))
        {
            currentItem = null;
        }
    }

    public void TryInteract()
    {
        if (currentItem == null) return;
        PlayPickAshAnimation();
        inventory.addItemInventory(currentItem);
        attackPlayer.CurrentWeapon = currentItem;
        Ashes anim = currentItem.GetComponentInChildren<Ashes>();
        anim.rock.SetTrigger("Take");
        currentItem = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("KillZone"))
        {
            healthPlayer.Die();
        }
    }

    public void PlayPickAshAnimation()
    {
        if(controller != null)
        {
            controller.StopPlayer();
        }
        playerController.DisableInputs();
        animator.SetTrigger("Take");
    }
}
