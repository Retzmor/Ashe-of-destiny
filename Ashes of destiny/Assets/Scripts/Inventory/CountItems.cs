using System.Collections;
using TMPro;
using UnityEngine;

public class CountItems : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameplayUIController gameplayUIController;
    int _countAshesCurrent = 0;
    bool _canBuyItem;
    int _canBuyItemFire = 4;

    public int CountAshesCurrent { get => _countAshesCurrent; set => _countAshesCurrent = value; }
    public bool CanBuyItem { get => _canBuyItem; set => _canBuyItem = value; }

    public void TryBuyItemFire()
    {
        if(_countAshesCurrent == _canBuyItemFire)
        {
            inventory.CanBuyItemFire = true;
            _countAshesCurrent = 0;
            gameplayUIController.CountAshe = 0;
            gameplayUIController.TextMeshPro.text = gameplayUIController.CountAshe.ToString();
            StartCoroutine(coolDownClick());
        }
    }

    IEnumerator coolDownClick()
    {
        yield return new WaitForEndOfFrame();
        inventory.CanBuyItemFire = false;
    }
}
