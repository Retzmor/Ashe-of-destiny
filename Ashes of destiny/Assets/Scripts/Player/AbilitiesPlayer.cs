using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Color = UnityEngine.Color;

public class AbilitiesPlayer : MonoBehaviour
{
    [Inject] AudioManager audioManager;
    [Inject] GameplayUIController gameplayUIController;
    [SerializeField] private Button[] AshesButton;
    [SerializeField] private Ability[] slotAshes;
    [SerializeField] TutorialController controller;
    [SerializeField] Transform[] handsParticule;
    List<ParticleSystem> currentHandParticles = new List<ParticleSystem>();
    private Queue<Ability> waitingAbilities = new Queue<Ability>();
    private bool[] slotUsed;
    private int currentSlotIndex = -1;

    Button _currentButton;
    public int CurrentSlotIndex => currentSlotIndex;
    public Particulas particulaActual;
    AttackPlayer attackPlayer;
    public Button CurrentButton { get => _currentButton; set => _currentButton = value; }
    private void Start()
    {
        slotAshes = new Ability[AshesButton.Length];
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
    public void AddAbility(Ability ability)
    {
        if (ability == null)
            return;
        for (int i = 0; i < 2; i++)
        {
            if (!slotUsed[i])
            {
                FillSlot(i, ability);
                gameplayUIController.UpdateAmmoDisplay(i, ability.currentAmmo);
                return;
            }
        }
        waitingAbilities.Enqueue(ability);
        gameplayUIController.UpdateNextPreview(waitingAbilities.Peek().icon);
        for (int i = 0; i < AshesButton.Length; i++)
        {
            if (!slotUsed[i])
            {
                if (!slotUsed[i])
                {
                    FillSlot(i, ability);
                    return;
                }
                if (AshesButton[i].TryGetComponent(out Particulas particulasUI))
                {
                    slotAshes[i] = ability;

                    if (particulasUI.particulas != null)
                    {
                        Destroy(particulasUI.particulas);
                        particulasUI.particulas = null;
                    }

                    ParticleSystem nuevaParticula = Instantiate(
                        ability.hudParticles,
                        AshesButton[i].transform
                    );

                    particulasUI.particulas = nuevaParticula;
                }

                AshesButton[i].image.sprite = ability.icon;
                AshesButton[i].image.color = Color.white;

                slotUsed[i] = true;
                return;
            }
        }
    }
    public void ActivateHandParticles(Ability ability)
    {
        if (ability == null) return;

        foreach (var p in currentHandParticles)
        {
            if (p != null)
                Destroy(p.gameObject);
        }
        currentHandParticles.Clear();
        foreach (Transform hand in handsParticule)
        {
            ParticleSystem p = Instantiate(
                ability.handParticles,
                hand.position,
                hand.rotation,
                hand
            );
            currentHandParticles.Add(p);
        }
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= AshesButton.Length) return;
        if (!slotUsed[index])
        {
            ClearSelection();
            return;
        }
        Ability selectedAshes = slotAshes[index];
        currentSlotIndex = index;
        audioManager.PlaySFX(selectedAshes.abilitySound, 1f);
       // audioManager.StopLoop();
        audioManager.PlayLoop(selectedAshes.loopSound);
        ActivateHandParticles(selectedAshes);
        UpdateSlotHighlights();
        if (particulaActual != null)
            particulaActual.DesactiveParticule();
        if (AshesButton[index].TryGetComponent(out Particulas nuevasParticulas))
        {
            particulaActual = nuevasParticulas;
            if (!attackPlayer.IsOnCooldown(selectedAshes))
            {
                particulaActual.ActivasParticulasLoop();
            }
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
    public Ability GetSelectedAbility()
    {
        if (currentSlotIndex < 0)
            return null;

        return slotAshes[currentSlotIndex];
    }
    private void FillSlot(int i, Ability ability)
    {
        Ability abilityInstance = Instantiate(ability);
        abilityInstance.currentAmmo = abilityInstance.maxAmmo;
        slotAshes[i] = abilityInstance;
        slotUsed[i] = true;
        gameplayUIController.UpdateAmmoDisplay(i, abilityInstance.currentAmmo);
        slotAshes[i] = ability;
        slotUsed[i] = true;

        if (AshesButton[i].TryGetComponent(out Particulas particulasUI))
        {
            if (particulasUI.particulas != null) Destroy(particulasUI.particulas);

            if (ability.hudParticles != null)
            {
                particulasUI.particulas = Instantiate(ability.hudParticles, AshesButton[i].transform);
            }
        }
        AshesButton[i].image.sprite = ability.icon;
        AshesButton[i].image.color = Color.white;
    }

    public void UseAmmo()
    {
        if (currentSlotIndex == -1) return;

        Ability current = slotAshes[currentSlotIndex];
        if (current == null) return;

        if (current.isInfinite) return;
        current.currentAmmo--;
        gameplayUIController.UpdateAmmoDisplay(currentSlotIndex, current.currentAmmo);
        if (current.currentAmmo <= 0)
        {
            RemoveCurrentAbilityAndRotate();
        }
    }

    public void RemoveCurrentAbilityAndRotate()
    {
        int index = currentSlotIndex;
        Ability abilityFinished = slotAshes[index]; 

        if (abilityFinished != null)
        {
            abilityFinished.Initialize();

            waitingAbilities.Enqueue(abilityFinished);
        }
        if (waitingAbilities.Count > 0)
        {
            Ability next = waitingAbilities.Dequeue();
            FillSlot(index, next);
            gameplayUIController.UpdateAmmoDisplay(index, next.currentAmmo);
            Sprite nextIcon = (waitingAbilities.Count > 0) ? waitingAbilities.Peek().icon : null;
            gameplayUIController.UpdateNextPreview(nextIcon);
        }

        ClearSelection();
    }
}
