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

    CanvasGroup canvas;



    bool isActiveMenuSkill;
    bool canMenuSkill;

    public void PauseGame()
    {
        panelPause.SetActive(true);
        gameManager.PauseGame();
    }

    public void DespausarGame()
    {
        panelPause.SetActive(false);
        gameManager.Despausar();
    }

    public void MenuStart()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1;
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
            canvas.alpha = 1;
        }

        if (!isActiveMenuSkill)
        {
            panelSkills.SetActive(!panelSkills.activeSelf);
        }
    }
}
