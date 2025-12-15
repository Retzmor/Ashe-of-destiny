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
    

    public void addItemInventory(GameObject item)
    {
        for(int i = 0; i < slotsInventory.Count; i++)
        {
            if (slotsInventory[i].transform.childCount == 0)
            {
                item.TryGetComponent(out Weapon weapon);
                item.TryGetComponent(out Image image);
                item.TryGetComponent(out Ashes ashe);
                ashes = ashe;
                ashe.DesactiveRock();
                abilitiesPlayer.AddAbility(image);
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
        Debug.Log("");
        attackPlayer.Bullet = ashes.ElementAttack;
        button2.onClick.Invoke();
    }
}
