using UnityEngine;
using Zenject;

public class GameplayUIInstaller : MonoInstaller
{
    [SerializeField] GameObject gamePlayUI;
    public override void InstallBindings()
    {
        Container.Bind<GameplayUIController>().FromComponentInHierarchy().AsSingle();
    }
}