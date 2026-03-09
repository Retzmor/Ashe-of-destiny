using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Zenject;

public class LevelController : MonoBehaviour
{
    [SerializeField] PlayerInputs inputs;
    enum MenuState
    {
        None,
        Pause,
        Skills
    }

    enum PauseSubState
    {
        Main,
        Options
    }

    PauseSubState pauseState;

    [SerializeField] GameObject panelOptions;
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject panelSkills;
    [SerializeField] GameObject panelGame;

    MenuState currentMenu = MenuState.None;

    [Inject] GameManager gameManager;
    [Inject] DisplaySettingsManager displaySettingsManager;

    [SerializeField] Particulas[] particles;
    [SerializeField] Animator animator;
    [SerializeField] CameraSwitcher camSwitcher;
    public bool CanOpenMenus = true;

    CanvasGroup canvas;

    bool isActiveMenuSkill;
    bool canMenuSkill;

    public void Start()
    {
        LockCursor();
    }

    public void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void PauseGame()
    {
        if (!CanOpenMenus) return;

        if (currentMenu == MenuState.Pause)
        {
            DespausarGame();
            return;
        }

        CloseAllMenus();

        panelPause.SetActive(true);
        panelOptions.SetActive(false);

        Time.timeScale = 0;

        inputs.DisableInputs();           
        camSwitcher.DisableCameraInput(); 
        UnlockCursor();

        pauseState = PauseSubState.Main;
        currentMenu = MenuState.Pause;
    }


    public void OpenOptions()
    {
        if (currentMenu != MenuState.Pause) return;
        panelPause.SetActive(false);
        panelOptions.SetActive(true);
        pauseState = PauseSubState.Options;
    }

    public void CloseOptions()
    {
        panelOptions.SetActive(false);
        panelPause.SetActive(true);

        pauseState = PauseSubState.Main;
    }

    public void DespausarGame()
    {
        CloseAllMenus();
        currentMenu = MenuState.None;
        gameManager.Despausar();
        inputs.EnableInputs();           
        camSwitcher.EnableCameraInput();
    }

    public void ScreenFull()
    {
        bool isFull = displaySettingsManager.GetFullScreen();
        displaySettingsManager.SetFullScreen(!isFull);
    }

    public void MenuStart()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        UnlockCursor();
        gameManager.GameStart();
        Time.timeScale = 1;
    }

    public void MenuSkill()
    {
        if (!CanOpenMenus) return;

        if (currentMenu == MenuState.Skills)
        {
            CloseAllMenus();
            currentMenu = MenuState.None;
            return;
        }
        CloseAllMenus();
        panelSkills.SetActive(true);
        Time.timeScale = 0f;
        UnlockCursor();
        for (int i = 0; i < particles.Length; i++)
        {
            if (particles[i].TryGetComponent<CanvasGroup>(out CanvasGroup canva))
            {
                canva.alpha = 0;
            }
        }
        currentMenu = MenuState.Skills;
    }
    public void CloseAllMenus()
    {
        panelPause.SetActive(false);
        panelSkills.SetActive(false);
        panelOptions.SetActive(false);
        pauseState = PauseSubState.Main;
        for (int i = 0; i < particles.Length; i++)
        {
            if (particles[i].TryGetComponent<CanvasGroup>(out CanvasGroup canva))
            {
                canva.alpha = 1;
            }
        }
        Time.timeScale = 1f;
        camSwitcher.EnableCameraInput();
        LockCursor();
    }
}
