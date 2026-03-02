using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    [SerializeField] GameObject buttonSkip;
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
    [SerializeField] GameObject treeObstacule;
    [SerializeField] GameObject goalMovement2;
    [SerializeField] GameObject countAshe;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject arrowAnimation;
    Animator animatorArrow;

    public delegate void PlayerMovementDelegate(bool activarRb);
    public static event PlayerMovementDelegate playerMovementDelegate;

    private int enemigosDerrotados = 0;

    void Start()
    {
        IniciarTutorial();
        arrowAnimation.TryGetComponent<Animator>(out Animator anim);
        animatorArrow = anim;
        // AudioManager.Instance.PlayMusic(musicaTutorial);
    }

    public void IniciarTutorial()
    {
        controller.StopPlayer();
        string[] dialogoInicial = {
            "Bienvenido a este mundo,",                                     //Alejandr@s, si van a añadir algo al texto, hacerlo en esas comillas, este es el primer cuadro que se muestras
            "aquí te vamos explicar a como utilizar tus poderes y moverte por estos mundos.",
            "para empezar con la aventura debemos saber como nos moveremos por el mundo"
        };

        dialogueManager.SetDialogue(dialogoInicial);
        dialogueManager.OnDialogueEnd = () =>
        {
            TutorialMovement();
        };
    }
    public void TutorialMovement()
    {
        panelTutorialRectTransform.gameObject.SetActive(true);
        buttonContinue.SetActive(false);
        textTitle.SetActive(false);
        buttonSkip.SetActive(false);
        imageMovemente.SetActive(true);
        imageShift.SetActive(true);
        textDetail.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI rectTransformTextSize);
        Vector2 vectorXY = new(-520, 300);
        panelTutorialRectTransform.anchoredPosition = vectorXY;
        Vector2 vectorWH = new(885, 300);
        panelTutorialRectTransform.sizeDelta=  vectorWH;
        //panelTutorialRectTransform.anchoredPosition = new Vector2(0,0);    NO RECUERDO PA QUE ERA AJAJAJ
        panelTutorialRectTransform.sizeDelta = new Vector2(700, 200);
        rectTransformTextSize.fontSize = 25;

        string[] dialogoMovimiento = {
            "Te podras mover con las teclas AWSD, y si mantienes presionado Shift, correras.",
        };

        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            controller.StartPlayer();
            goalMovement.SetActive(true);
        };
    }   
    
    public void TutorialJump()
    {
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
            controller.StartPlayer();
            goalMovement2.SetActive(true);
            treeObstacule.SetActive(true);
        };
    }
    public void TutorialAshes()
    {
        countAshe.SetActive(true);
        controller.StopPlayer();
        treeObstacule.SetActive(false);
        goalMovement2.SetActive(false);
        imagejump.SetActive(false);
        panelAshe.SetActive(true);
        panelTutorialRectTransform.gameObject.SetActive(false);
        cameraManager.CameraAsheTutorial();
        dialogueManager.OnDialogueEnd = () =>
        {
            controller.StartPlayer();
            panelAshe.SetActive(false);
            panelGame.SetActive(true);
            cameraManager.CameraPlayer();
        };
    }
    public void TutorialWeapons()
    {
        controller.StopPlayer();
        imageTab.SetActive(true);
        panelTutorialRectTransform.gameObject.SetActive(true);
        panelGame.SetActive(false);
        arrowAnimation.SetActive(true);
        StartCoroutine(WaitForTheNextFrame());
        string[] dialogoMovimiento = {"Presiona la tecla Tab",};
        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            controller.StartPlayer();
            //TutorialShoot();
        };
    }

    IEnumerator WaitForTheNextFrame()
    {
        yield return new WaitForSeconds(1);
        animatorArrow.SetTrigger("Play");
    }
    
    public void TutorialShoot()
    {
        imageTab.SetActive(false);
        controller.StartPlayer();
        string[] dialogoMovimiento = { 
        "para seleccionar las habilidades se usan las teclas Q y E, se mostrara la seleccionada de manera visual",};
        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            enemy.SetActive(true);
            TutorialAim();
        };
    }

    public void TutorialAim()
    {
        imageTab.SetActive(false);
        controller.StartPlayer();
        string[] dialogoMovimiento = {
        "Manteniendo el click derecho del mouse, apuntas, y con el izquierdo, dispara",};
        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            controller.StartPlayer();
        };
    }

    public void TutorialFinish()
    {
        panelTutorialRectTransform.gameObject.SetActive(true);
        Vector2 vectorXY = new(0, 0);
        panelTutorialRectTransform.anchoredPosition = vectorXY;
        Vector2 vectorWH = new(1920, 900);
        panelTutorialRectTransform.sizeDelta = vectorWH;
    }

    public void DesactivePanelTutorial()
    {
        panelTutorialRectTransform.gameObject.SetActive(false);
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

    public void NoTutorial()
    {
        countAshe.SetActive(true);
        enemy.SetActive(true);
        panelTutorialRectTransform.gameObject.SetActive(false);
        panelGame.SetActive(true);
        controller.SkipTutorial = true;
        inventory.TutorialSkip = true;
        controller.StartPlayer();
    }
}

