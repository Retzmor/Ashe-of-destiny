using UnityEngine;
using Zenject;

public class ToolTipManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ToolTipManager>().FromComponentInHierarchy().AsSingle();
    }
}