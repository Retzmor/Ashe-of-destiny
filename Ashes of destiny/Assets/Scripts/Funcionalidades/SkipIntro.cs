using UnityEngine;
using Zenject;

public class SkipIntro : MonoBehaviour
{
    [Inject] GameManager gameManager;
    public void SkipIntroButton()
    {
        gameManager.TutorialStart();
    }
}
