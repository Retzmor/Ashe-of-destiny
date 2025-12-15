using UnityEngine;
using Zenject;

public class InventoryInstaller : MonoInstaller
{
    [SerializeField] GameObject inventory;
    public override void InstallBindings()
    {
        Container.Bind<Inventory>().FromComponentInHierarchy(inventory).AsSingle();
    }
}