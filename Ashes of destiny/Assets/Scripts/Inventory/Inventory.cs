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

    Ashes ashes;



    public void addItemInventory(GameObject objectItem)
    {
        gameplayUIController.UpdateCount();
        
    }

    public void AsheFireButton(Ashes ashesData)
    {
        if (ashesData == null)
            return;
        Time.timeScale = 1f;

        ashesData.DesactiveRock(); 

        Sprite icon = ashesData.GetComponent<Image>().sprite;

        abilitiesPlayer.AddAbility(ashesData, icon);

        gameplayUIController.DesactivePanelSkills();
        gameplayUIController.ActivePanelGame();
        StartCoroutine(TutorialShoot());

    }

    IEnumerator TutorialShoot()
    {
        yield return new WaitForSeconds(1f);
        tutorialManager.TutorialShoot();
    }
}
