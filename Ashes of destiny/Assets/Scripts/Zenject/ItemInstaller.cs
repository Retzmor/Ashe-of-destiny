using UnityEngine;
using Zenject;

public class ItemInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Item>().FromComponentInHierarchy().AsTransient();
    }
}