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
    [SerializeField] Ashes asheWater;
    [SerializeField] Ashes asheFire;
    [SerializeField] Image imageFire;
    [SerializeField] CountItems countItem;
    bool _tutorialSkip = false;
    bool _canBuyItemFire = false;

    Ashes ashes;

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
            //imageFire.color = Color.white;
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
