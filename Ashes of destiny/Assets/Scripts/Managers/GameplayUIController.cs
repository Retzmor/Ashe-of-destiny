using TMPro;
using UnityEngine;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] TutorialController tutorialController;
    int countAshe;

    public void UpdateCount()
    {
        countAshe++;
        textMeshPro.text = countAshe.ToString();

        if(countAshe == 4)
        {
            tutorialController.AshesRecolected();
        }
    }
}
