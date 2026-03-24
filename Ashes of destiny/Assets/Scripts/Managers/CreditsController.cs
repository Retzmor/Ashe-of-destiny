using System.Collections;
using UnityEngine;
using Zenject;

public class CreditsController : MonoBehaviour
{
    [Inject] GameManager gameManager;

    private void Start()
    {
        StartCoroutine(ContinueMenuStart());
    }

    IEnumerator ContinueMenuStart()
    {
        yield return new WaitForSeconds(5f);
        gameManager.TutorialStart();
    }
}
