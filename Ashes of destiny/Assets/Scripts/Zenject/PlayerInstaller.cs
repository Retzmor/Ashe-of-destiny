using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerCollisions>().FromComponentInHierarchy().AsSingle();
    }
}