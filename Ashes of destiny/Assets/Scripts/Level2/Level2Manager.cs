using System.Collections;
using UnityEngine;
using Zenject;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] objectives;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;

    [Inject] private LevelController levelController;

    private void Start()
    {
        StartLevel();
    }

    private void StartLevel()
    {
        playerController.DisableInputs();
        playerMovement.CanMoving = false;
        playerMovement.canJumping = false;
        levelController.UnlockCursor();
        foreach (GameObject enemy in enemies)
            enemy.SetActive(true);

        foreach (GameObject obj in objectives)
            obj.SetActive(true);
        EnablePlayer();
    }

    public void EnablePlayer()
    {
        playerController.EnableInputs();
        playerMovement.CanMoving = true;
        playerMovement.canJumping = true;
        levelController.LockCursor();
    }

    public void DisablePlayer()
    {
        playerController.DisableInputs();
        playerMovement.CanMoving = false;
        playerMovement.canJumping = false;
        levelController.UnlockCursor();
    }

    public void WinLevel()
    {
        DisablePlayer();
        panelWin.SetActive(true);
    }

    public void LoseLevel()
    {
        DisablePlayer();
        panelLose.SetActive(true);
    }
    public void CompleteObjective(GameObject obj)
    {
        obj.SetActive(false);
        bool allDone = true;
        foreach (GameObject o in objectives)
        {
            if (o.activeSelf)
            {
                allDone = false;
                break;
            }
        }

        if (allDone)
            WinLevel();
    }
}

