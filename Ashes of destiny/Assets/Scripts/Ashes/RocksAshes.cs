using UnityEngine;
using Zenject;

public class RocksAshes : MonoBehaviour
{
    [SerializeField] GameObject ashe;
    [Inject] PlayerCollisions playerCollisions;
    public void DesactiveAshe()
    {
        playerCollisions.EndPickAshAnimation();
        ashe.SetActive(false);
    }
}
