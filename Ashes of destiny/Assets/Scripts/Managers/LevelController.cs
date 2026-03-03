using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
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

    MenuState currentMenu = MenuState.None;

    [Inject] GameManager gameManager;
    [Inject] DisplaySettingsManager displaySettingsManager;
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject panelSkills;
    [SerializeField] GameObject panelGame;
    [SerializeField] Particulas[] particles;
    [SerializeField] Animator animator;

    CanvasGroup canvas;

    bool isActiveMenuSkill;
    bool canMenuSkill;
    public void PauseGame()
    {
        if (currentMenu == MenuState.Pause)
        {
            CloseAllMenus();
            currentMenu = MenuState.None;
            return;
        }

        CloseAllMenus();

        panelPause.SetActive(true);
        panelOptions.SetActive(false);

        Time.timeScale = 0;

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
        gameManager.GameStart();
        Time.timeScale = 1;
    }

    public void MenuSkill()
    {
        if (currentMenu == MenuState.Skills)
        {
            CloseAllMenus();
            currentMenu = MenuState.None;
            return;
        }

        CloseAllMenus();

        panelSkills.SetActive(true);
        Time.timeScale = 0f;

        for (int i = 0; i < particles.Length; i++)
        {
            if (particles[i].TryGetComponent<CanvasGroup>(out CanvasGroup canva))
            {
                canva.alpha = 0;
            }
        }

        currentMenu = MenuState.Skills;
    }
    void CloseAllMenus()
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
    }

}
