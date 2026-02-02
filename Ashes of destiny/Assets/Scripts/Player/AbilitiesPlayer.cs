using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class AbilitiesPlayer : MonoBehaviour
{
    [SerializeField] private List<Button> AshesButton = new List<Button>();
    [SerializeField] private Ashes[] slotAshes;

    private bool[] slotUsed;
    private int currentSlotIndex = -1;
    public int CurrentSlotIndex => currentSlotIndex;

    private void Start()
    {
        slotAshes = new Ashes[AshesButton.Count];
        slotUsed = new bool[AshesButton.Count];
        UpdateSlotHighlights();
    }

    private void Update()
    {
        for (int i = 0; i < AshesButton.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
            }
        }
    }

    // ============================
    // PARTÍCULAS SOLO AL AGREGAR
    // ============================
    public void AddAbility(Image image, GameObject objectItem)
    {
        for (int i = 0; i < AshesButton.Count; i++)
        {
            if (!slotUsed[i])
            {
                AshesButton[i].image.sprite = image.sprite;
                AshesButton[i].image.color = Color.white;

                if (AshesButton[i].TryGetComponent(out Particulas particulas))
                    particulas.ActivasParticulas();

                slotAshes[i] = objectItem.GetComponent<Ashes>();
                slotUsed[i] = true;
                return;
            }
        }
    }

    // ============================
    // SELECCIÓN SIN PARTÍCULAS
    // ============================
    public void SelectSlot(int index)
    {
        if (index < 0 || index >= AshesButton.Count)
            return;

        if (!slotUsed[index])
            return;

        currentSlotIndex = index;
        UpdateSlotHighlights();

        Debug.Log($"Slot seleccionado: {index + 1}");
    }

    // ============================
    // BORDE / RESALTADO
    // ============================
    private void UpdateSlotHighlights()
    {
        for (int i = 0; i < AshesButton.Count; i++)
        {
            Outline outline = AshesButton[i].GetComponent<Outline>();

            if (outline == null)
                continue;

            outline.enabled = (i == currentSlotIndex && slotUsed[i]);
        }
    }

    // ============================
    // COOLDOWN VISUAL
    // ============================
    public IEnumerator CooldownVisual(Button button, float cooldownTime)
    {
        button.image.color = Color.black;

        float elapsed = 0f;
        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            button.image.color = Color.Lerp(Color.black, Color.white, elapsed / cooldownTime);
            yield return null;
        }

        button.image.color = Color.white;
    }

    public Button GetSelectedButton()
    {
        if (currentSlotIndex < 0 || currentSlotIndex >= AshesButton.Count)
            return null;

        return AshesButton[currentSlotIndex];
    }

    public Ashes GetSelectedAshes()
    {
        if (currentSlotIndex < 0 || currentSlotIndex >= slotAshes.Length)
            return null;

        return slotAshes[currentSlotIndex];
    }
}
