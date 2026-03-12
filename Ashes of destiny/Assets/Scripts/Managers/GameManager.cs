using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void GameStart()
    {
        EventBus.Clear();
        Time.timeScale = 1;
        EventBus.GameStart?.Invoke();
        SceneManager.LoadScene("Game");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void Despausar()
    {
        Time.timeScale = 1;
    }

    public void Level1Start()
    {
        EventBus.Clear();
        Time.timeScale = 1;
        EventBus.GameStart?.Invoke();
        SceneManager.LoadScene("Level 1");
    }
}
