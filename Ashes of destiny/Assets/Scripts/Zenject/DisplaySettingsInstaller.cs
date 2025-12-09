using UnityEngine;
using Zenject;

public class DisplaySettingsInstaller : MonoInstaller
{
    [SerializeField] GameObject UIManagerPrefap;
    public override void InstallBindings()
    {
        Container.Bind<DisplaySettingsManager>().FromComponentInNewPrefab(UIManagerPrefap).AsSingle();
    }
}