using TMPro;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    [SerializeField] Canvas parentCanvas;
    [SerializeField] RectTransform toolTipTransform;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text details;
    [SerializeField] CanvasGroup toolTipCanvasGroup;

    bool isShowing;

    private void Awake()
    {
        toolTipTransform.gameObject.SetActive(false);
        toolTipCanvasGroup.alpha = 0;
        isShowing = false;
    }

    private void Update()
    {
        if (!isShowing)
            return;

        if (toolTipCanvasGroup.alpha < 1)
        {
            toolTipCanvasGroup.alpha += Time.unscaledDeltaTime * 2;
        }

        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform,Input.mousePosition,parentCanvas.worldCamera,out mousePos);
        toolTipTransform.localPosition = mousePos;
    }

    public void Show(string titleText, string detailText)
    {
        title.text = titleText;
        details.text = detailText;

        toolTipCanvasGroup.alpha = 0;
        toolTipTransform.gameObject.SetActive(true);
        isShowing = true;
    }

    public void Hide()
    {
        toolTipTransform.gameObject.SetActive(false);
        isShowing = false;
    }
}

