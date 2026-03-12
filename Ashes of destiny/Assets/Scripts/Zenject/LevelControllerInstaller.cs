using UnityEngine;
using Zenject;

public class LevelControllerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LevelController>().FromComponentInHierarchy().AsSingle();
    }
}