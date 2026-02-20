using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
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
        panelPause.SetActive(true);
        animator.Play("animacion pausa");
        Time.timeScale = 0;

       // gameManager.PauseGame();
    }

    public void DespausarGame()
    {
        panelPause.SetActive(false);
        gameManager.Despausar();
        Time.timeScale = 1;
    }

    public void ScreenFull()
    {
        displaySettingsManager.GetFullScreen();
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
        bool isActive = panelSkills.activeSelf;

        if (!isActive)
        {
            panelSkills.SetActive(true);
            Time.timeScale = 0f;

            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].TryGetComponent<CanvasGroup>(out CanvasGroup canva))
                {
                    canva.alpha = 0;
                }
            }
        }
        else
        {
            panelSkills.SetActive(false);
            Time.timeScale = 1f;

            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].TryGetComponent<CanvasGroup>(out CanvasGroup canva))
                {
                    canva.alpha = 1;
                }
            }
        }
    }

}
