using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [Inject] GameManager gameManager;
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

    public void MenuStart()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        gameManager.GameStart();
    }

    public void MenuSkill()
    {
        if(!panelGame.activeSelf)
        {
            panelGame.SetActive(!panelGame.activeSelf);
            for(int i = 0; i < particles.Length; i++)
            {
                particles[i].TryGetComponent<CanvasGroup>(out CanvasGroup canva);
                canvas = canva;
                canvas.alpha = 0;
            }
        }

        else
        {
            //canvas.alpha = 1;
        }

        if (!isActiveMenuSkill)
        {
            panelSkills.SetActive(!panelSkills.activeSelf);
        }
    }
}
