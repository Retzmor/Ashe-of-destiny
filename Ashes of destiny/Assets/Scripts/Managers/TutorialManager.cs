using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] GameObject goalMovement;
    [SerializeField] GameObject treeObstacule;
    [SerializeField] GameObject goalMovement2;

    public delegate void PlayerMovementDelegate(bool activarRb);
    public static event PlayerMovementDelegate playerMovementDelegate;

    private int enemigosDerrotados = 0;

    void Start()
    {
        IniciarTutorial();
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
        textDetail.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI rectTransformTextSize);
        Vector2 vectorXY = new(-520, 300);
        panelTutorialRectTransform.anchoredPosition = vectorXY;
        Vector2 vectorWH = new(885, 300);
        panelTutorialRectTransform.sizeDelta=  vectorWH;
        //panelTutorialRectTransform.anchoredPosition = new Vector2(0,0);    NO RECUERDO PA QUE ERA AJAJAJ
        panelTutorialRectTransform.sizeDelta = new Vector2(700, 200);
        rectTransformTextSize.fontSize = 25;

        string[] dialogoMovimiento = {
            "Te podras mover con las teclas AWSD.",
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
        controller.ResetPositionPlayer();
        controller.StopPlayer();
        goalMovement.SetActive(false);
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
        controller.ResetPositionPlayer();
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
        controller.ResetPositionPlayer();
        controller.StopPlayer();
        string[] dialogoMovimiento = {"Con las teclas: Q, E y R.",};
        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            controller.StartPlayer();
            TutorialShoot();
        };
    }
    
    public void TutorialShoot()
    {
        controller.ResetPositionPlayer();
        controller.StartPlayer();
        string[] dialogoMovimiento = {"Con el click derecho del mouse apuntas, con el izquirdo disparas, debes tener una habilidad seleccionada para disparar",};
        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            controller.StartPlayer();
            TutorialFinish();
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
}

