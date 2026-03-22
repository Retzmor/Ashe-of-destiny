using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Inventory : MonoBehaviour
{
    [Inject] GameplayUIController gameplayUIController;
    [InjectOptional] TutorialManager tutorialManager;

    [SerializeField] AbilitiesPlayer abilitiesPlayer;
    [SerializeField] CountItems countItem;
    [SerializeField] Ability fireAbility;
    [SerializeField] Ability waterAbility;
    [SerializeField] Ability rockAbility;
    [SerializeField] Ability airAbility;
    [SerializeField] Image imageFire;
    [SerializeField] Image imageWater;
    [SerializeField] Image imageRock;
    [SerializeField] Image imageAir;
    [SerializeField] Image imageKaton;
    [SerializeField] Sprite imageFireActive;
    [SerializeField] Sprite imageWaterActive;
    [SerializeField] Sprite imageRockActive;
    [SerializeField] Sprite imageAirActive;
    [SerializeField] Sprite imageKatonActive;

    public System.Action<string> OnItemPurchased;

    private int purchaseCount = 0;
    private bool _tutorialSkip = false;
    public bool CanBuyItemFire { get; set; }
    public bool CanBuyItemWater { get; set; }
    public bool CanBuyItemRock { get; set; }
    public bool CanBuyItemAir { get; set; }
    public bool TutorialSkip { get => _tutorialSkip; set => _tutorialSkip = value; }
    public void BuyFire() { BuyAbility(fireAbility, "Fire"); }
    public void BuyWater() { BuyAbility(waterAbility, "Water"); }
    public void BuyRock() { BuyAbility(rockAbility, "Rock"); }
    public void BuyAir() { BuyAbility(airAbility, "Air"); }

    private void BuyAbility(Ability ashesData, string type)
    {
        if (ashesData == null) return;

        CheckFunds(type);

        if (GetPurchasePermission(type))
        {
            abilitiesPlayer.AddAbility(ashesData);
            UpdateSkillPanelImage(type);
            purchaseCount++;
            OnItemPurchased?.Invoke(type);
            VerificarFinalizacionCompraTutorial();
        }
    }

    public void addItemInventory(GameObject objectItem)
    {
        if (gameplayUIController != null) gameplayUIController.UpdateCount();
    }

    private void CheckFunds(string type)
    {
        switch (type)
        {
            case "Fire": countItem.TryBuyItemFire(); break;
            case "Water": countItem.TryBuyItemWater(); break;
            case "Rock": countItem.TryBuyItemRock(); break;
            case "Air": countItem.TryBuyItemAir(); break;
        }
    }

    private bool GetPurchasePermission(string type)
    {
        return type switch
        {
            "Fire" => CanBuyItemFire,
            "Water" => CanBuyItemWater,
            "Rock" => CanBuyItemRock,
            "Air" => CanBuyItemAir,
            _ => false
        };
    }

    private void VerificarFinalizacionCompraTutorial()
    {
        if (!_tutorialSkip && purchaseCount >= 2)
        {
            Time.timeScale = 1f;
            gameplayUIController.DesactivePanelSkills();
            gameplayUIController.ActivePanelGame();
            StartCoroutine(TutorialShoot());
        }
    }

    private void UpdateSkillPanelImage(string type)
    {
        switch (type)
        {
            case "Fire":
                if (imageFire != null) imageFire.sprite = imageFireActive;
                break;
            case "Water":
                if (imageWater != null) imageWater.sprite = imageWaterActive;
                break;
            case "Rock":
                if (imageRock != null) imageRock.sprite = imageRockActive;
                break;
            case "Air":
                if (imageAir != null) imageAir.sprite = imageAirActive;
                break;
        }
    }

    IEnumerator TutorialShoot()
    {
        yield return new WaitForSeconds(1f);
        if (tutorialManager != null) tutorialManager.TutorialShoot();
    }
}