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
                Debug.Log(ashe.gameObject.name);
                ashe.DesactiveRock();
                abilitiesPlayer.AddAbility(image, objectItem);
                break;
            }
        }
    }

    public void ClickBoton1()
    {
        attackPlayer.Bullet = ashes.ElementAttack;
        button1.onClick.Invoke();
    }

    public void ClickBoton2()
    {
        attackPlayer.Bullet = ashes.ElementAttack;
        button2.onClick.Invoke();
    }
}
