using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour
{
    [Inject] GameManager gameManager;
    public void ClickGameStart()
    {
        gameManager.GameStart();
    }

    public void ExitGame()
    {
        gameManager.ExitGame();
    }
}
