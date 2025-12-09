using UnityEngine;

public class RocksAshes : MonoBehaviour
{
    [SerializeField] GameObject ashe;
    public void DesactiveAshe()
    {
        Debug.Log("Desactivar");
        ashe.SetActive(false);
    }
}
