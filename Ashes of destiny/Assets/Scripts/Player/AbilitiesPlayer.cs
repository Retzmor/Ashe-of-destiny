using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Color = UnityEngine.Color;

public class AbilitiesPlayer : MonoBehaviour
{
    [Inject] Item item;
    [SerializeField] List<Button> AshesButton = new List<Button>();
    public void AddAbility(Image image, GameObject objectItem)
    {
        for (int i = 0; i < AshesButton.Count; i++)
        {
            if(AshesButton[i].image.sprite != null && item.isUsed == false)
            {
                AshesButton[i].image.sprite = image.sprite;
                AshesButton[i].image.color = Color.white;
                objectItem.TryGetComponent<Item>(out Item item);
                return;
            }
        }
    }
}
