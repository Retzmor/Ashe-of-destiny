using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [Inject] GameManager gameManager;
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject panelSkills;
    [SerializeField] GameObject panelGame;

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
        panelGame.SetActive(!panelGame.activeSelf);
        panelSkills.SetActive(!panelSkills.activeSelf);
    }
}
