using UnityEngine;
using Zenject;

public class SkipCinematic : MonoBehaviour
{
    [Inject] GameManager gameManager;

    public void StartLevel1()
    {
        gameManager.Level1Start();
    }

    void Awake() 
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
