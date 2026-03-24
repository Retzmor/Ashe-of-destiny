using System.Collections;
using UnityEngine;
using Zenject;

public class CreditsController : MonoBehaviour
{
    [Inject] LevelController levelController;

    private void Start()
    {
        StartCoroutine(ContinueMenuStart());
    }

    IEnumerator ContinueMenuStart()
    {
        yield return new WaitForSeconds(5f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        levelController.MenuStart();
    }
}
