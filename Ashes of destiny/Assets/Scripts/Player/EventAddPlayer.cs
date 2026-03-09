using UnityEngine;

public class EventAddPlayer : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TutorialController tutorialController;
  
    public void EndAnimationTakeAshe()
    {
        playerController.EnableInputs();
        tutorialController.StartPlayer();
    }
}
