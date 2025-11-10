using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [Inject] GameManager gameManager;
    [SerializeField] GameObject panelPause;
    
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
}
