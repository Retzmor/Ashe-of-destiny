using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;


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
    }   
    
    
    public void TutorialJump()
    {

    }
    
    public void TutorialWeapons()
    {

    }
    
    public void TutorialShoot()
    {

    }
    
    public void TutorialFinish()
    {

    }
    
    public void PanelAsheTutorial()
    {
        cameraManager.CameraAsheTutorial();
    }

    //public void EnemigoDerrotado()
    //{
    //    enemigosDerrotados++;
    //    Debug.Log("Enemigos derrotados: " + enemigosDerrotados);
    //
    //    if (enemigosDerrotados == 1)
    //    {
    //        string[] dialogo = {
    //            "¡Muy bien!",
    //            "Ahora enfréntate a los siguientes dos enemigos."                              //este es el segundo texto
    //        };
    //
    //        dialogueManager.SetDialogue(dialogo);
    //        dialogueManager.OnDialogueEnd = () =>
    //        {
    //            StartCoroutine(EnfocarYActivarSiguientesEnemigos());
    //        };
    //    }
    //
    //    if (enemigosDerrotados == 3)
    //    {
    //
    //        string[] final = {
    //            "¡Has completado el tutorial!",                                 //este es el ultimo
    //            "Estás listo para la batalla."
    //        };
    //
    //        dialogueManager.SetDialogue(final);
    //
    //        dialogueManager.OnDialogueEnd = () =>
    //        {
    //            panelFelicidades.gameObject.SetActive(true);
    //        };
    //    }
    //}

    private IEnumerator ActivarConRetraso(GameObject enemigo, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        enemigo.SetActive(true);
    }

    private IEnumerator EnfocarYActivarSiguientesEnemigos()
    {
      //  cameraManager.EnfocarCamara(1, 2.5f);
        yield return new WaitForSeconds(3f);
        //enemigo2.SetActive(true);

       // cameraManager.EnfocarCamara(2, 2.5f);
        yield return new WaitForSeconds(3f);
        //enemigo3.SetActive(true);
    }
}

