using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using Zenject;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private ScriptDialogue dialogueManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] TutorialController controller;
    [SerializeField] AudioClip musicaTutorial;
    [SerializeField] RectTransform panelTutorialRectTransform;
    [SerializeField] GameObject panelAshe;
    [SerializeField] GameObject panelGame;
    [SerializeField] GameObject buttonContinue;
    [SerializeField] GameObject textTitle;
    [SerializeField] RectTransform textDetail;
    [SerializeField] TextMeshProUGUI textDetailTextMesh;
    [SerializeField] GameObject imageMovemente;
    [SerializeField] GameObject imagejump;
    [SerializeField] GameObject imageF;
    [SerializeField] GameObject imageShift;
    [SerializeField] GameObject imageTab;
    [SerializeField] GameObject goalMovement;
    [SerializeField] GameObject goalMovement2;
    [SerializeField] GameObject countAshe;
    [SerializeField] GameObject[] enemy;
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject arrowFire;
    [SerializeField] GameObject arrowAir;
    [SerializeField] GameObject goalAttackMelee;
    [SerializeField] Animator animArrow;
    [SerializeField] GameObject MouseRight;
    [SerializeField] GameObject MouseLeft;
    [SerializeField] PlayerController playerController;
    [SerializeField] Image imageE;
    [SerializeField] Image imageQ;
    [SerializeField] PlayerMovement playerMovement;
    public bool BlockPlayerInput;
    [Inject] LevelController levelController;
    void Start()
    {
        IniciarTutorial();
        levelController.UnlockCursor();
        // AudioManager.Instance.PlayMusic(musicaTutorial);
    }
    public void IniciarTutorial()
    {
        playerMovement.canJumping = false;
        levelController.UnlockCursor();
        controller.StopPlayer();
        playerController.DisableInputs();
        controller.DesactiveTextSpace();
        levelController.CanOpenMenus = false;
        controller.ArrowDisable();
        string[] dialogoInicial = {
            "Bienvenido a este mundo,",                                     //Alejandr@s, si van a añadir algo al texto, hacerlo en esas comillas, este es el primer cuadro que se muestras
            "aquí te vamos explicar a como utilizar tus poderes y moverte por estos mundos.",
            "para empezar con la aventura debemos saber como nos moveremos por el mundo"
        };

        dialogueManager.SetDialogue(dialogoInicial);
        dialogueManager.OnDialogueEnd = () =>
        {
            TutorialMovement();
            levelController.CanOpenMenus = true;
            levelController.LockCursor();
        };
    }
    public void TutorialMovement()
    {
        playerMovement.CanMoving = false;
        controller.StopPlayer();
        controller.ActiveTextSpace();
        levelController.CanOpenMenus = false;
        panelTutorialRectTransform.gameObject.SetActive(true);
        buttonContinue.SetActive(false);
        textTitle.SetActive(false);
        imageMovemente.SetActive(true);
        imageShift.SetActive(true);
        playerController.DisableInputs();
        textDetail.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI rectTransformTextSize);
        Vector2 vectorXY = new(-550, 300);
        panelTutorialRectTransform.anchoredPosition = vectorXY;
        Vector2 vectorWH = new(885, 400);
        panelTutorialRectTransform.sizeDelta=  vectorWH;
        //panelTutorialRectTransform.anchoredPosition = new Vector2(0,0);    NO RECUERDO PA QUE ERA AJAJAJ
        panelTutorialRectTransform.sizeDelta = new Vector2(900, 350);
        rectTransformTextSize.fontSize = 40;

        string[] dialogoMovimiento = {
            "Te podras mover con las teclas AWSD,",
            "y si mantienes presionado Shift, correras.",
        };

        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            playerMovement.CanMoving = true;
            goalMovement.SetActive(true);
            controller.StartPlayer();
            playerController.EnableInputs();
            levelController.CanOpenMenus = true;
            controller.DesactiveTextSpace();
            controller.StartPlayer();
        };
    }   
    
    public void TutorialJump()
    {
        controller.ActiveTextSpace();
        playerController.DisableInputs();
        levelController.CanOpenMenus = false;
        controller.StopPlayer();
        goalMovement.SetActive(false);
        imageShift.SetActive(false);
        imageMovemente.SetActive(false);
        imagejump.SetActive(true);
        string[] dialogoMovimiento = {
            "Saltaras con la tecla espacio.",
        };
        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            playerMovement.canJumping = true;
            controller.StartPlayer();
            goalMovement2.SetActive(true);
            levelController.CanOpenMenus = true;
            playerController.EnableInputs();
            controller.DesactiveTextSpace();
        };
    }
    public void TutorialMelee()
    {
        controller.ActiveTextSpace();
        playerController.DisableInputs();
        levelController.CanOpenMenus = false;
        controller.StopPlayer();
        goalAttackMelee.SetActive(true);
        goalMovement2.SetActive(false);
        imagejump.SetActive(false);
        MouseRight.SetActive(true);
        string[] dialogoMovimiento = {
        "Con el click derecho del mouse podras golpearas",
        "rompe las tablas que bloquean tu camino!!"};
        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            levelController.CanOpenMenus = true;
            playerController.EnableInputs();
            controller.DesactiveTextSpace();
            controller.StartPlayer();
        };
    }
    public void TutorialAshes()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerController.DisableInputs();
        levelController.CanOpenMenus = false;
        MouseRight.SetActive(false);
        goalAttackMelee.SetActive(false);
        countAshe.SetActive(true);
        controller.StopPlayer();
        goalMovement2.SetActive(false);
        imagejump.SetActive(false);
        panelAshe.SetActive(true);
        panelTutorialRectTransform.gameObject.SetActive(false);
        cameraManager.CameraAsheTutorial();
        dialogueManager.OnDialogueEnd = () =>
        {
            levelController.LockCursor();
            controller.StartPlayer();
            panelAshe.SetActive(false);
            panelGame.SetActive(true);
            cameraManager.CameraPlayer();
            levelController.CanOpenMenus = true;
            playerController.EnableInputs();
        };
    }
    public void TutorialWeapons()
    {
        playerMovement.TutorialMovementLocked = true;
        BlockPlayerInput = true;
        levelController.CanOpenMenus = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        controller.StopPlayer();
        playerMovement.CanMoving = false;
        playerMovement.StopMovement();
        imageTab.SetActive(true);
        panelTutorialRectTransform.gameObject.SetActive(true);
        panelGame.SetActive(false);
        arrowFire.SetActive(true);
        inventory.OnItemPurchased += HandleTutorialPurchases;
        StartCoroutine(WaitAnimator());
        controller.ArrowActive();
        string[] dialogoMovimiento = { "Presiona la tecla Tab" };
        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.RequireKey(Key.Tab);
    }

    IEnumerator WaitAnimator()
    {
        yield return new WaitForSeconds(1);
        animArrow.SetBool("Arrow", true);
    }
    public void TutorialShoot()
    {
        playerMovement.TutorialMovementLocked = false;
        controller.ArrowDisable();
        playerMovement.canJumping = false;
        playerMovement.CanMoving = false;
        levelController.LockCursor();
        controller.ActiveTextSpace();
        imageTab.SetActive(false);
        imageE.gameObject.SetActive(true);
        imageQ.gameObject.SetActive(true);
        controller.StopPlayer();
        string[] dialogoMovimiento = { 
        "seleccione las habilidades con las teclas Q y E",
        "se mostrara la seleccionada de manera visual",};
        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].SetActive(true);
            }
            TutorialAim();
        };
    }
    public void TutorialAim()
    {
        controller.ActiveTextSpace();
        playerController.DisableInputs();
        imageE.gameObject.SetActive(false);
        imageQ.gameObject.SetActive(false);
        imageTab.SetActive(false);
        controller.StopPlayer();
        MouseRight.SetActive(true);
        MouseLeft.SetActive(true);
        string[] dialogoMovimiento = {
        "Manteniendo el click derecho del mouse, apuntas,",
        " y con el izquierdo, dispara",};
        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            StartCoroutine(EnablePlayerDelayed());
        };
    }
    IEnumerator EnablePlayerDelayed()
    {
        yield return null;
        playerController.EnableInputs();
        playerMovement.CanMoving = true;
        playerMovement.jump = true;
        playerMovement.canJumping = true;
        controller.StartPlayer();
        controller.DesactiveTextSpace();
    }
    public void TutorialWin()
    {
        EnableCursor();
        panelWin.SetActive(true);
        controller.StopPlayer();
    }

    public void TutorialLose()
    {
        EnableCursor();
        panelLose.SetActive(true);
        controller.StopPlayer();
    }

    public void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void HandleTutorialPurchases(string skillName)
    {
        if (skillName == "Fire")
        {
            arrowFire.SetActive(false);
            arrowAir.SetActive(true);
        }
        else if (skillName == "Air")
        {
            if (arrowAir != null) arrowAir.SetActive(false);
           inventory.OnItemPurchased -= HandleTutorialPurchases; 
        }
    }
}

