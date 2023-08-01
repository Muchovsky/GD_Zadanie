using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ApplicationInstaller", menuName = "Installers/ApplicationInstaller")]

public class ApplicationInstaller : ScriptableObjectInstaller<ApplicationInstaller>
{
    [SerializeField] PrefabList prefabList;
    public override void InstallBindings()
    {
        Container.Bind<PrefabManager>().AsSingle();
        Container.Bind<PrefabList>().FromInstance(prefabList).AsSingle();
    }
}
