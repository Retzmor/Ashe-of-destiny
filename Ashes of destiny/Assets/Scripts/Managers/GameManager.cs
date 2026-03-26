using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] AudioManager audioManager;
    public void GameStart()
    {
        EventBus.Clear();
        Time.timeScale = 1;
        EventBus.GameStart?.Invoke();
        audioManager.StopMusic();
        SceneManager.LoadScene("IntroScene");
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

    public void TutorialStart()
    {
        EventBus.Clear();
        Time.timeScale = 1;
        EventBus.GameStart?.Invoke();
        SceneManager.LoadScene("Game");
    }

    public void Level1Start()
    {
        EventBus.Clear();
        Time.timeScale = 1;
        EventBus.GameStart?.Invoke();
        SceneManager.LoadScene("Level 1");
    }

    public void StartCredits()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Creditos");
    }

    public void StartCinematic()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("NIvel CInematica");
    }
}
