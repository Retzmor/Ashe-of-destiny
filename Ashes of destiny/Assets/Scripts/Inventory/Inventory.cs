using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Inventory : MonoBehaviour
{
    [Inject] GameplayUIController gameplayUIController;
    [InjectOptional] TutorialManager tutorialManager;
    [SerializeField] List<Item> slotsInventory;
    [SerializeField] AbilitiesPlayer abilitiesPlayer;
    [SerializeField] AttackPlayer attackPlayer;
    [SerializeField] Button button1;
    [SerializeField] Button button2;
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
    [SerializeField] CountItems countItem;
    PlayerController playerController;
    bool _tutorialSkip = false;
    bool _canBuyItemFire = false;

    public System.Action<string> OnItemPurchased;
    private int purchaseCount = 0;

    public bool TutorialSkip { get => _tutorialSkip; set => _tutorialSkip = value; }
    public bool CanBuyItemFire { get => _canBuyItemFire; set => _canBuyItemFire = value; }

    private void Start()
    {
        playerController = abilitiesPlayer.gameObject.GetComponent<PlayerController>();
    }

    public void addItemInventory(GameObject objectItem)
    {
        gameplayUIController.UpdateCount();
    }

    public void AsheFireButton(Ability ashesData)
    {
        countItem.TryBuyItemFire();
        if (CanBuyItemFire)
        {
            if (ashesData == null) return;

            abilitiesPlayer.AddAbility(ashesData);
            abilitiesPlayer.ActivateHandParticles(ashesData);
            imageFire.sprite = imageFireActive;

            purchaseCount++;
            OnItemPurchased?.Invoke("Fire");
            VerificarFinalizacionCompraTutorial();
        }
    }

    public void AsheWaterButton(Ability ashesData)
    {
        countItem.TryBuyItemWater();
        if (CanBuyItemFire == true)
        {
            if (ashesData == null)
                return;
            Time.timeScale = 1f;
            //ashesData.DesactiveRock();
            Sprite icon = ashesData.icon;
            abilitiesPlayer.AddAbility(ashesData);
            imageWater.sprite = imageWaterActive;
            gameplayUIController.DesactivePanelSkills();
            gameplayUIController.ActivePanelGame();
            if (_tutorialSkip == false)
            {
                StartCoroutine(TutorialShoot());
            }
        }
    }

    public void AsheRockButton(Ability ashesData)
    {
        countItem.TryBuyItemRock();
        if (CanBuyItemFire == true)
        {
            if (ashesData == null)
                return;
            Time.timeScale = 1f;
            //ashesData.DesactiveRock();
            Sprite icon = ashesData.icon;
            abilitiesPlayer.AddAbility(ashesData);
            imageRock.sprite = imageRockActive;
            gameplayUIController.DesactivePanelSkills();
            gameplayUIController.ActivePanelGame();
            if (_tutorialSkip == false)
            {
                StartCoroutine(TutorialShoot());
            }
        }
    }

    public void AsheAirButton(Ability ashesData)
    {
        countItem.TryBuyItemAir();
        if (CanBuyItemFire) 
        {
            if (ashesData == null) return;

            abilitiesPlayer.AddAbility(ashesData);
            imageAir.sprite = imageAirActive;

            purchaseCount++;
            OnItemPurchased?.Invoke("Air"); 

            VerificarFinalizacionCompraTutorial();
        }
    }

    private void VerificarFinalizacionCompraTutorial()
    {
        if (!_tutorialSkip && purchaseCount >= 2)
        {
            Time.timeScale = 1f;
            imageKaton.sprite = imageKatonActive;
            gameplayUIController.DesactivePanelSkills();
            gameplayUIController.ActivePanelGame();
            StartCoroutine(TutorialShoot());
        }
        else if (_tutorialSkip)
        {
            Time.timeScale = 1f;
            gameplayUIController.DesactivePanelSkills();
            gameplayUIController.ActivePanelGame();
        }
    }
    IEnumerator TutorialShoot()
    {
        yield return new WaitForSeconds(1f);
        if(tutorialManager != null)
        {
            tutorialManager.TutorialShoot();
        }
    }
}
