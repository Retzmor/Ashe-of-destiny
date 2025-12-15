using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> slotsInventory;
    [SerializeField] AbilitiesPlayer abilitiesPlayer;
    GameObject item1;
    GameObject item2;
    GameObject item3;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            for(int i = 0; i < slotsInventory.Count; i++)
            {
                slotsInventory[i].TryGetComponent<Button>(out Button boton);
                //boton.onClick
            }
        }
    }

    public void addItemInventory(GameObject item)
    {
        for(int i = 0; i < slotsInventory.Count; i++)
        {
            if (slotsInventory[i].transform.childCount == 0)
            {
                item.TryGetComponent(out Weapon weapon);
                item.TryGetComponent(out Image image);
                item.TryGetComponent(out Ashes ashe);
                ashe.DesactiveRock();
                abilitiesPlayer.AddAbility(image);
                item.gameObject.AddComponent<Button>();
                break;
            }
        }
    }
}
