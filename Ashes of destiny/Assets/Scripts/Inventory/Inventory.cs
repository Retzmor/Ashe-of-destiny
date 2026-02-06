using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> slotsInventory;
    [SerializeField] AbilitiesPlayer abilitiesPlayer;
    [SerializeField] AttackPlayer attackPlayer;
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    Ashes ashes;
    Image currentImage;
    

    public void addItemInventory(GameObject objectItem)
    {
        for(int i = 0; i < slotsInventory.Count; i++)
        {
            if (slotsInventory[i].transform.childCount == 0)
            {
                objectItem.TryGetComponent(out Weapon weapon);
                objectItem.TryGetComponent(out Image image);
                objectItem.TryGetComponent(out Ashes ashe);
                ashes = ashe;
                ashe.DesactiveRock();
                abilitiesPlayer.AddAbility(image, objectItem);
                break;
            }
        }
    }
}
