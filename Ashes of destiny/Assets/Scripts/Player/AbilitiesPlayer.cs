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
    Button _currentButton;
    public int CurrentSlotIndex => currentSlotIndex;
    public Particulas particulaActual;

    public Button CurrentButton { get => _currentButton; set => _currentButton = value; }

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
                _currentButton = AshesButton[i];
            }
        }
    }

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
                particulaActual = particulas;

                slotAshes[i] = objectItem.GetComponent<Ashes>();
                slotUsed[i] = true;
                return;
            }
        }
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= AshesButton.Count)
            return;

        if (!slotUsed[index])
            return;

        if (particulaActual != null)
        {
            particulaActual.DesactiveParticule();
        }

        currentSlotIndex = index;
        UpdateSlotHighlights();

        if (AshesButton[index].TryGetComponent(out Particulas nuevasParticulas))
        {
            particulaActual = nuevasParticulas;
            particulaActual.ActivasParticulasLoop();
        }
    }

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
