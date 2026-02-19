using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ToolTipDetaill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Inject]
    ToolTipManager toolTipManager;

    [SerializeField] string titleText;
    [SerializeField] string descriptionText;
    [SerializeField] float toolTipDelay = 0.5f;

    float timer;
    bool hasMouse;
    bool tooltipShown;

    private void OnEnable()
    {
        hasMouse = false;
        tooltipShown = false;
        timer = 0;
    }

    private void Update()
    {
        if (!hasMouse || tooltipShown)
            return;

        timer += Time.unscaledDeltaTime;

        if (timer >= toolTipDelay)
        {
            toolTipManager.Show(titleText, descriptionText);
            tooltipShown = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        timer = 0;
        hasMouse = true;
        tooltipShown = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hasMouse = false;
        tooltipShown = false;
        toolTipManager.Hide();
    }
}
