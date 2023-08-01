using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ApplicationInstaller", menuName = "Installers/ApplicationInstaller")]

public class ApplicationInstaller : ScriptableObjectInstaller<ApplicationInstaller>
{
    [SerializeField] PrefabList prefabList;
    [SerializeField] ItemSpriteList itemSpriteList;
    public override void InstallBindings()
    {
        Container.Bind<PrefabManager>().AsSingle();
        Container.Bind<PrefabList>().FromInstance(prefabList).AsSingle();
        Container.Bind<ItemSpriteList>().FromInstance(itemSpriteList).AsSingle();
    }
}
