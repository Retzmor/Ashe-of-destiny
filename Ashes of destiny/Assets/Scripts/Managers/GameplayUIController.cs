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
    [SerializeField] CountItems countItems;
    int _countAshe;

    public int CountAshe { get => _countAshe; set => _countAshe = value; }
    public TextMeshProUGUI TextMeshPro { get => textMeshPro; set => textMeshPro = value; }

    public void UpdateCount()
    {
        _countAshe++;
        countItems.CountAshesCurrent = _countAshe;
        TextMeshPro.text = _countAshe.ToString();

        if(CountAshe >= 4)
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
