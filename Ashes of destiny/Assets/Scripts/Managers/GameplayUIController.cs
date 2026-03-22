using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] TutorialController tutorialController;
    [SerializeField] GameObject panelSkills;
    [SerializeField] GameObject panelGame;
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;
    [SerializeField] CountItems countItems;
    [SerializeField] TextMeshProUGUI ammoTextSlot1;
    [SerializeField] TextMeshProUGUI ammoTextSlot2;
    [SerializeField] Image nextAbilityIcon;
    private Sprite defaultNextIcon;
    int _countAshe;

    public int CountAshe { get => _countAshe; set => _countAshe = value; }
    public TextMeshProUGUI TextMeshPro { get => textMeshPro; set => textMeshPro = value; }

    private void Awake()
    {
        if (nextAbilityIcon != null)
        {
            defaultNextIcon = nextAbilityIcon.sprite;
        }
    }

    public void UpdateCount()
    {
        _countAshe++;
        countItems.CountAshesCurrent = _countAshe;
        TextMeshPro.text = _countAshe.ToString();

        if(CountAshe >= 4)
        {
            if (tutorialController == null) return;
            tutorialController.AshesRecolected();
        }
    }

    public void UpdateAmmoDisplay(int slotIndex, int currentAmmo)
    {
        if (slotIndex == 0)
        {
            if (ammoTextSlot1 != null) ammoTextSlot1.text = currentAmmo.ToString();
        }
        else if (slotIndex == 1)
        {
            if (ammoTextSlot2 != null) ammoTextSlot2.text = currentAmmo.ToString();
        }
    }

    public void UpdateNextPreview(Sprite nextIcon)
    {
        if (nextAbilityIcon == null) return;
        if (nextIcon != null)
        {
            nextAbilityIcon.sprite = nextIcon;
            nextAbilityIcon.color = new Color(1, 1, 1, 0.6f); 
        }
        else
        {
            nextAbilityIcon.sprite = defaultNextIcon;
        }
    }

    public void ClearAmmoDisplay(int slotIndex)
    {
        if (slotIndex == 0 && ammoTextSlot1 != null)
        {
            ammoTextSlot1.text = "";
        }
        else if (slotIndex == 1 && ammoTextSlot2 != null)
        {
            ammoTextSlot2.text = "";
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
