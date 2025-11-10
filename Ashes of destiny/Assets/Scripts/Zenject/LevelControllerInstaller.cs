using UnityEngine;
using Zenject;

public class LevelControllerInstaller : MonoInstaller
{
    [SerializeField] GameObject levelController;
    public override void InstallBindings()
    {
        Container.Bind<LevelController>().FromComponentInHierarchy(levelController).AsSingle();
    }
}