using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Inventory : MonoBehaviour
{
    [Inject] GameplayUIController gameplayUIController;
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

    public void AsheFireButton(GameObject objectItem)
    {
        for (int i = 0; i < slotsInventory.Count; i++)
        {
            if (slotsInventory[i].transform.childCount == 0)
            {
                objectItem.TryGetComponent(out Weapon weapon);
                objectItem.TryGetComponent(out Image image);
                objectItem.TryGetComponent(out Ashes ashe);
                ashes = ashe;
                ashe.DesactiveRock();
                abilitiesPlayer.AddAbility(image, objectItem);
                gameplayUIController.DesactivePanelSkills();
                gameplayUIController.ActivePanelGame();
                break;
            }
        }
    }
}
