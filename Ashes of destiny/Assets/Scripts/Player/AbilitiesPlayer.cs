using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesPlayer : MonoBehaviour
{
    [SerializeField] List<Button> AshesButton = new List<Button>();
    public void AddAbility(Image image)
    {
        for (int i = 0; i < AshesButton.Count; i++)
        {
            Debug.Log(AshesButton[i].image.name);
            if(AshesButton[i].image.sprite == null)
            {
                AshesButton[i].image.sprite = image.sprite;
                return;
            }
        }
    }
}
