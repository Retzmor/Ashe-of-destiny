using UnityEngine;
using Zenject;

public class RocksAshes : MonoBehaviour
{
    [SerializeField] GameObject ashe;
    [InjectOptional] PlayerCollisions playerCollisions;
    public void DesactiveAshe()
    {
        if(playerCollisions != null)
        {
            //playerCollisions.EndPickAshAnimation();
        }
        ashe.SetActive(false);
    }
}
