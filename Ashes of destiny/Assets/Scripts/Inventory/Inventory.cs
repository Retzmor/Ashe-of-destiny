using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Inventory : MonoBehaviour
{
    [Inject] GameplayUIController gameplayUIController;
    [Inject] TutorialManager tutorialManager;
    [SerializeField] List<Item> slotsInventory;
    [SerializeField] AbilitiesPlayer abilitiesPlayer;
    [SerializeField] AttackPlayer attackPlayer;
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Image imageFire;
    [SerializeField] Image imageWater;
    [SerializeField] Image imageRock;
    [SerializeField] Image imageAir;
    [SerializeField] Sprite imageFireActive;
    [SerializeField] Sprite imageWaterActive;
    [SerializeField] Sprite imageRockActive;
    [SerializeField] Sprite imageAirActive;
    [SerializeField] CountItems countItem;
    bool _tutorialSkip = false;
    bool _canBuyItemFire = false;


    public bool TutorialSkip { get => _tutorialSkip; set => _tutorialSkip = value; }
    public bool CanBuyItemFire { get => _canBuyItemFire; set => _canBuyItemFire = value; }

    public void addItemInventory(GameObject objectItem)
    {
        gameplayUIController.UpdateCount();
    }

    public void AsheFireButton(Ashes ashesData)
    {
        countItem.TryBuyItemFire();
        Debug.Log(CanBuyItemFire);
        if (CanBuyItemFire == true)
        {
            if (ashesData == null)
                return;
            Time.timeScale = 1f;
            ashesData.DesactiveRock();
            Sprite icon = ashesData.GetComponent<Image>().sprite;
            abilitiesPlayer.AddAbility(ashesData, icon);
            imageFire.sprite = imageFireActive;
            gameplayUIController.DesactivePanelSkills();
            gameplayUIController.ActivePanelGame();
            if (_tutorialSkip == false)
            {
                StartCoroutine(TutorialShoot());
            }
        }
    }

    public void AsheWaterButton(Ashes ashesData)
    {
        countItem.TryBuyItemWater();
        if (CanBuyItemFire == true)
        {
            if (ashesData == null)
                return;
            Time.timeScale = 1f;
            ashesData.DesactiveRock();
            Sprite icon = ashesData.GetComponent<Image>().sprite;
            abilitiesPlayer.AddAbility(ashesData, icon);
            imageWater.sprite = imageWaterActive;
            gameplayUIController.DesactivePanelSkills();
            gameplayUIController.ActivePanelGame();
            if (_tutorialSkip == false)
            {
                StartCoroutine(TutorialShoot());
            }
        }
    }

    public void AsheRockButton(Ashes ashesData)
    {
        countItem.TryBuyItemRock();
        if (CanBuyItemFire == true)
        {
            if (ashesData == null)
                return;
            Time.timeScale = 1f;
            ashesData.DesactiveRock();
            Sprite icon = ashesData.GetComponent<Image>().sprite;
            abilitiesPlayer.AddAbility(ashesData, icon);
            imageAir.sprite = imageAirActive;
            gameplayUIController.DesactivePanelSkills();
            gameplayUIController.ActivePanelGame();
            if (_tutorialSkip == false)
            {
                StartCoroutine(TutorialShoot());
            }
        }
    }

    public void AsheAirButton(Ashes ashesData)
    {
        countItem.TryBuyItemRock();
        if (CanBuyItemFire == true)
        {
            if (ashesData == null)
                return;
            Time.timeScale = 1f;
            ashesData.DesactiveRock();
            Sprite icon = ashesData.GetComponent<Image>().sprite;
            abilitiesPlayer.AddAbility(ashesData, icon);
            imageWater.sprite = imageWaterActive;
            gameplayUIController.DesactivePanelSkills();
            gameplayUIController.ActivePanelGame();
            if (_tutorialSkip == false)
            {
                StartCoroutine(TutorialShoot());   
            }
        }
    }

    IEnumerator TutorialShoot()
    {
        yield return new WaitForSeconds(1f);
        tutorialManager.TutorialShoot();
    }
}
