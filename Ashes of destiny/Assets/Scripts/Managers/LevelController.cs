using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Zenject;

public class LevelController : MonoBehaviour
{
    [SerializeField] PlayerInputs inputs;
    [InjectOptional] TutorialManager tutorialManager;
    private bool _hasWon = false;
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
    [SerializeField] PlayerMovement player;
    public bool CanOpenMenus = true;
    int _countWin;

    CanvasGroup canvas;

    bool isActiveMenuSkill;
    bool canMenuSkill;

    public int CountWin { get => _countWin; set => _countWin = value; }

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
        StartCoroutine(UnlockCursorNextFrame());
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
        CloseAllMenus();
        Time.timeScale = 1;
        UnlockCursor();
        gameManager.TutorialStart();
    }

    public void StartLevelOne()
    {
        CloseAllMenus();
        gameManager.Level1Start();
    }

    public void StartCinematic()
    {
        gameManager.StartCinematic();
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
        if (panelPause) panelPause.SetActive(false);
        if (panelSkills) panelSkills.SetActive(false);
        if (panelOptions) panelOptions.SetActive(false);
        pauseState = PauseSubState.Main;
        for (int i = 0; i < particles.Length; i++)
        {
            if (particles[i] && particles[i].TryGetComponent(out CanvasGroup canva))
            {
                canva.alpha = 1;
            }
        }
        Time.timeScale = 1f;
        if (camSwitcher) camSwitcher.EnableCameraInput();
        LockCursor();
    }

    public void WinTutorial()
    {
        if (_hasWon) return;
        _countWin++;
        if(_countWin >= 3)
        {
            _hasWon = true;
            tutorialManager.TutorialWin();
        }
    }
    IEnumerator UnlockCursorNextFrame()
    {
        yield return null;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
