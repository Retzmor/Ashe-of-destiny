using System.Collections;
using TMPro;
using UnityEngine;

public class CountItems : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameplayUIController gameplayUIController;
    int _countAshesCurrent = 0;
    bool _canBuyItem;
    [SerializeField] int _canBuyItemFire;
    [SerializeField] int _canBuyItemWater;
    [SerializeField] int _canBuyItemRock;
    [SerializeField] int _canBuyItemAir;

    public int CountAshesCurrent { get => _countAshesCurrent; set => _countAshesCurrent = value; }
    public bool CanBuyItem { get => _canBuyItem; set => _canBuyItem = value; }

    public void TryBuyItemFire()
    {
        Debug.Log(_countAshesCurrent);
        if(_countAshesCurrent >= _canBuyItemFire)
        {
            inventory.CanBuyItemFire = true;
            _countAshesCurrent -= _canBuyItemFire;
            gameplayUIController.CountAshe = _countAshesCurrent;
            gameplayUIController.TextMeshPro.text = gameplayUIController.CountAshe.ToString();
            StartCoroutine(coolDownClick());
        }
    }

    public void TryBuyItemWater()
    {
        if (_countAshesCurrent >= _canBuyItemWater)
        {
            inventory.CanBuyItemWater = true;
            _countAshesCurrent -= _canBuyItemWater;
            gameplayUIController.CountAshe = _countAshesCurrent;
            gameplayUIController.TextMeshPro.text = gameplayUIController.CountAshe.ToString();
            StartCoroutine(coolDownClick());
        }
    }

    public void TryBuyItemRock()
    {
        if (_countAshesCurrent >= _canBuyItemRock)
        {
            inventory.CanBuyItemRock = true;
            _countAshesCurrent -= _canBuyItemRock;
            gameplayUIController.CountAshe = _countAshesCurrent;
            gameplayUIController.TextMeshPro.text = gameplayUIController.CountAshe.ToString();
            StartCoroutine(coolDownClick());
        }
    }

    public void TryBuyItemAir()
    {
        if (_countAshesCurrent >= _canBuyItemAir)
        {
            inventory.CanBuyItemAir = true;
            _countAshesCurrent -= _canBuyItemAir;
            gameplayUIController.CountAshe = _countAshesCurrent;
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
