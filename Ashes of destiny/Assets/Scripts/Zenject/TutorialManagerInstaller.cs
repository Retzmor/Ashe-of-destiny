using UnityEngine;
using Zenject;

public class TutorialManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TutorialManager>().FromComponentInHierarchy().AsSingle();
    }
}