using TMPro;
using UnityEngine;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] TutorialController tutorialController;
    [SerializeField] GameObject panelSkills;
    [SerializeField] GameObject panelGame;
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;
    int countAshe;

    public void UpdateCount()
    {
        countAshe++;
        textMeshPro.text = countAshe.ToString();

        if(countAshe >= 4)
        {
            tutorialController.AshesRecolected();
        }
    }

    public void DesactivePanelSkills()
    {
        panelSkills.SetActive(false);
    }

    public void ActivePanelGame()
    {
        panelGame.SetActive(true);
    }

    public void DesactivePanelWin()
    {
        panelWin.SetActive(false);
    }

    public void DesactivePanelLose()
    {
        panelLose.SetActive(false);
    }
}
