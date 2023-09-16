using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ApplicationInstaller", menuName = "Installers/ApplicationInstaller")]
public class ApplicationInstaller : ScriptableObjectInstaller<ApplicationInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<PrefabManager>().AsSingle().NonLazy();
        Container.Bind<ConnectionMock>().AsSingle();
        ButtonClickedSignal.DeclareSignals(Container);
        GameUISignals.DeclareSignals(Container);
    }
}