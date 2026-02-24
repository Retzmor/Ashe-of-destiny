using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class AbilitiesPlayer : MonoBehaviour
{
    [SerializeField] private Button[] AshesButton;
    [SerializeField] private Ashes[] slotAshes;
    [SerializeField] TutorialController controller;

    private bool[] slotUsed;
    private int currentSlotIndex = -1;
    Button _currentButton;
    public int CurrentSlotIndex => currentSlotIndex;
    public Particulas particulaActual;
    AttackPlayer attackPlayer;

    public Button CurrentButton { get => _currentButton; set => _currentButton = value; }

    private void Start()
    {
        slotAshes = new Ashes[AshesButton.Length];
        slotUsed = new bool[AshesButton.Length];
        UpdateSlotHighlights();
        attackPlayer = GetComponent<AttackPlayer>();
    }

    public void ButtonOne()
    {
        SelectSlot(0);
        _currentButton = AshesButton[0];
    }

    public void ButtonTwo()
    {
        SelectSlot(1);
        _currentButton = AshesButton[1];
    }

    public void AddAbility(Image image, GameObject objectItem)
    {
        Ashes ashes = objectItem.GetComponent<Ashes>();
        Weapon weapon = objectItem.GetComponent<Weapon>();
        controller.AshesRecolected(weapon);
        for (int i = 0; i < AshesButton.Length; i++)
        {
            if (!slotUsed[i])
            {
                if (ashes == null || weapon == null)
                {
                    return;
                }


                if (AshesButton[i].TryGetComponent(out Particulas particulasUI))
                {
                    slotAshes[i] = ashes;

                    if (particulasUI.particulas != null)
                        Destroy(particulasUI.particulas.gameObject);

                    ParticleSystem nuevaParticula = Instantiate(
                        ashes.ParticulaPrefab,
                        AshesButton[i].transform
                    );

                    particulasUI.particulas = nuevaParticula;
                }
                AshesButton[i].image.sprite = image.sprite;
                AshesButton[i].image.color = Color.white;
                slotUsed[i] = true;
                return;
            }
        }
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= AshesButton.Length)
            return;

        if (!slotUsed[index])
        {
            ClearSelection();
            return;
        }

        Ashes selectedAshes = slotAshes[index];

        if (attackPlayer.IsOnCooldown(selectedAshes))
            return;

        if (particulaActual != null)
            particulaActual.DesactiveParticule();

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
        for (int i = 0; i < AshesButton.Length; i++)
        {
            Outline outline = AshesButton[i].GetComponent<Outline>();

            if (outline == null)
                continue;

            outline.enabled = (i == currentSlotIndex && slotUsed[i]);
        }
    }

    public void ClearSelection()
    {
        if (particulaActual != null)
            particulaActual.DesactiveParticule();

        currentSlotIndex = -1;
        UpdateSlotHighlights();
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
    public Ashes GetSelectedAshes()
    {
        if (currentSlotIndex < 0 ||
            currentSlotIndex >= slotAshes.Length ||
            !slotUsed[currentSlotIndex])
            return null;

        return slotAshes[currentSlotIndex];
    }

}
