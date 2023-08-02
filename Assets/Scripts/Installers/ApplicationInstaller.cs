using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ApplicationInstaller", menuName = "Installers/ApplicationInstaller")]

public class ApplicationInstaller : ScriptableObjectInstaller<ApplicationInstaller>
{
    [SerializeField] PrefabList prefabList;
    [SerializeField] ItemSpriteList itemSpriteList;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<PrefabManager>().AsSingle().NonLazy();
        Container.Bind<PrefabList>().FromInstance(prefabList).AsSingle();
        Container.Bind<ItemSpriteList>().FromInstance(itemSpriteList).AsSingle();
        Container.Bind<ConnectionMock>().AsSingle();
        ButtonClickedSignal.DeclareSignals(Container);
        GameUISignals.DeclareSignals(Container);
    }
}
