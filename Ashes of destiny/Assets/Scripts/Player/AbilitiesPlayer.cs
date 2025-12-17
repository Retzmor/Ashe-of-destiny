using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Color = UnityEngine.Color;

public class AbilitiesPlayer : MonoBehaviour
{
    [SerializeField] List<Button> AshesButton = new List<Button>();
    private bool[] slotUsed;

    private void Start()
    {
        slotUsed = new bool[AshesButton.Count];
    }
    public void AddAbility(Image image, GameObject objectItem)
    {
        for (int i = 0; i < AshesButton.Count; i++)
        {
            if (!slotUsed[i])
            {
                AshesButton[i].image.sprite = image.sprite;
                AshesButton[i].image.color = Color.white;
                AshesButton[i].TryGetComponent<Particulas>(out Particulas particulas);
                particulas.ActivasParticulas();
                slotUsed[i] = true;
                return;
            }
        }
    }
}
