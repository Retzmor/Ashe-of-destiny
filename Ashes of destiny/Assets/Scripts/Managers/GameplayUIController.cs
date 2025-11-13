using TMPro;
using UnityEngine;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    int countAshe;

    public void UpdateCount()
    {
        countAshe++;
        textMeshPro.text = countAshe.ToString();
    }
}
