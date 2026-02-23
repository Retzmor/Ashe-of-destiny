using UnityEngine;
using Zenject;

public class CinemachineManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CameraManager>().FromComponentInHierarchy().AsSingle();
    }
}