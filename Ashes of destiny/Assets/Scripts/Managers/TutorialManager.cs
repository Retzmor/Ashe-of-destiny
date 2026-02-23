using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject panelFelicidades;
    [SerializeField] private ScriptDialogue dialogueManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] AudioClip musicaTutorial;
    [SerializeField] GameObject panelTutorial;
    [SerializeField] GameObject panelJuego;
    [SerializeField] GameObject buttonSkip;
    [SerializeField] GameObject buttonContinue;
    [SerializeField] GameObject textTitle;
    [SerializeField] GameObject textDetail;
    [SerializeField] GameObject imageMovemente;
    [SerializeField] GameObject goalMovement;
    [SerializeField] GameObject treeObstacule;
    [SerializeField] GameObject goalMovement2;
    [SerializeField] PlayerMovement player;

    Vector3 playerPosition;



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
        playerPosition = player.transform.position;
        string[] dialogoInicial = {
            "Bienvenido a este mundo,",                                     //Alejandr@s, si van a añadir algo al texto, hacerlo en esas comillas, este es el primer cuadro que se muestras
            "aquí te vamos explicar a como utilizar tus poderes y moverte por estos mundos.",
            "para empezar con la aventura debemos saber como nos moveremos por el mundo"
        };

        dialogueManager.SetDialogue(dialogoInicial);
        dialogueManager.OnDialogueEnd = () =>
        {
            // cameraManager.EnfocarCamara(0, 3f);
            TutorialMovement();
        };
    }


    public void TutorialMovement()
    {
        panelTutorial.SetActive(true);
        buttonContinue.SetActive(false);
        textTitle.SetActive(false);
        buttonSkip.SetActive(false);
        imageMovemente.SetActive(true);
        panelTutorial.TryGetComponent<RectTransform>(out RectTransform rectTransform);
        textDetail.TryGetComponent<RectTransform>(out RectTransform rectTransformText);
        textDetail.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI rectTransformTextSize);
        Vector2 vectorXY = new(-520, 300);
        rectTransform.anchoredPosition = vectorXY;
        Vector2 vectorWH = new(885, 300);
        rectTransform.sizeDelta=  vectorWH;
        rectTransformText.anchoredPosition = new Vector2(0,0);
        rectTransformText.sizeDelta = new Vector2(700, 200);
        rectTransformTextSize.fontSize = 25;

        string[] dialogoMovimiento = {
            "Te podras mover con las teclas AWSD.",
        };

        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            // cameraManager.EnfocarCamara(0, 3f);
            goalMovement.SetActive(true);
            player.CanMoving = true;
        };
    }   
    
    
    public void TutorialJump()
    {
        goalMovement.SetActive(false);
        player.transform.position = playerPosition;
        string[] dialogoMovimiento = {
            "Saltaras con la tecla espacio.",
        };

        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            // cameraManager.EnfocarCamara(0, 3f);
            goalMovement2.SetActive(true);
            treeObstacule.SetActive(true);
        };
    }
    

    public void TutorialRunning()
    {
        player.transform.position = playerPosition;
        treeObstacule.SetActive(false);
        goalMovement2.SetActive(false);
        string[] dialogoMovimiento = {
            "correras con la tecla shift + awsd puedes correr.",
        };

        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            // cameraManager.EnfocarCamara(0, 3f);
            TutorialWeapons();
        };
    }

    //panel cenizas
    public void TutorialWeapons()
    {
        string[] dialogoMovimiento = {
            "Con las teclas: Q, E y R.",
        };

        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            // cameraManager.EnfocarCamara(0, 3f);
            TutorialShoot();
        };
    }
    
    public void TutorialShoot()
    {
        string[] dialogoMovimiento = {
            "Con el click derecho del mouse apuntas, con el izquirdo disparas, debes tener una habilidad seleccionada para disparar",
        };

        dialogueManager.SetDialogue(dialogoMovimiento);
        dialogueManager.OnDialogueEnd = () =>
        {
            // cameraManager.EnfocarCamara(0, 3f);
            TutorialFinish();
        };
    }
    
    public void TutorialFinish()
    {
        panelTutorial.SetActive(true);
        panelTutorial.TryGetComponent<RectTransform>(out RectTransform rectTransform);
        Vector2 vectorXY = new(0, 0);
        rectTransform.anchoredPosition = vectorXY;
        Vector2 vectorWH = new(1920, 900);
        rectTransform.sizeDelta = vectorWH;
    }
    public void PanelAsheTutorial() 
    { 
        cameraManager.CameraAsheTutorial();
    }
}

