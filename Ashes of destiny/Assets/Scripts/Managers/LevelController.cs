using System;
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
    private bool _hasCheckpointActivated = false;
    private Vector3 _lastCheckpointPos;
    [SerializeField] HealthPlayer playerHealth;
    [SerializeField] GameObject panelLose;
    [SerializeField] CanvasGroup fadeCanvasGroup;
    [SerializeField] CanvasGroup checkpointMessageGroup;
    [SerializeField] HealthBoss healthBoss;
    [SerializeField] BossArenaTrigger bossArenaTrigger;
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
        _lastCheckpointPos = player.transform.position;
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

    internal void SetCheckpoint(Vector3 position)
    {
        _lastCheckpointPos = position;
        _hasCheckpointActivated = true;
        ShowCheckpointFeedback();
    }
    public void HandlePlayerDeath()
    {
        if (_hasCheckpointActivated)
        {
            StartCoroutine(DirectRespawnSequence());
        }
        else
        {
            panelLose.SetActive(true);
            UnlockCursor();
        }
    }
    IEnumerator DirectRespawnSequence()
    {
        float timer = 0;
        float fadeDuration = 1; 
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 1;
        yield return new WaitForSecondsRealtime(0.5f);
        RespawnPlayer();
        timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0;
    }

    public void RespawnPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.ResetHealth();
            PlayerComponent playerComponent = playerHealth.GetComponent<PlayerComponent>();
            playerComponent.Animator.Play("Idle");
        }
        player.transform.position = _lastCheckpointPos;
        Time.timeScale = 1f;
        LockCursor();
        bossArenaTrigger.ResetBoss();
        if (player.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true; 
            rb.linearVelocity = Vector3.zero; 
            rb.angularVelocity = Vector3.zero;

            player.transform.position = _lastCheckpointPos;
            Physics.SyncTransforms();
            rb.isKinematic = false;
        }
    }

    public void ShowCheckpointFeedback()
    {
        StopCoroutine(nameof(FadeCheckpointMessage));
        StartCoroutine(FadeCheckpointMessage());
    }

    IEnumerator FadeCheckpointMessage()
    {
        float duration = 0.5f; 
        float waitTime = 2f;   
        float timer = 0;

        // 1. Fade In (Aparecer)
        while (timer < duration)
        {
            timer += Time.deltaTime;
            checkpointMessageGroup.alpha = Mathf.Lerp(0, 1, timer / duration);
            yield return null;
        }
        checkpointMessageGroup.alpha = 1;
        yield return new WaitForSeconds(waitTime);
        timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            checkpointMessageGroup.alpha = Mathf.Lerp(1, 0, timer / duration);
            yield return null;
        }
        checkpointMessageGroup.alpha = 0;
    }
}
