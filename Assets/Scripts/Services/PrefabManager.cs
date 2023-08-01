using UnityEngine;
using Zenject;

public class PrefabManager
{
    PrefabList prefabList;
    SceneContext sceneContext;

    [Inject]
    public PrefabManager(PrefabList prefabList, SceneContext sceneContext)
    {
        this.prefabList = prefabList;
        this.sceneContext = sceneContext;
    }

    public T GetPrefab<T>(PrefabNameEnum prefabName, Transform parent) where T : Component
    {
        var newGameObject = GetGameObject(prefabName, parent);
        return newGameObject.GetComponent<T>();
    }
    GameObject GetGameObject(PrefabNameEnum prefabName, Transform parent)
    {
        return CreateGameObject(prefabList, prefabName, parent);
    }

    GameObject CreateGameObject(PrefabList list, PrefabNameEnum prefabName, Transform parent)
    {
        var prefab = list.GetByName(prefabName);
        if (prefab == null || prefab.GameObject == null)
        {
            Debug.LogErrorFormat("Trying Instantiate {0} failed", prefabName.ToString());
            return null;
        }

        var gameObject = GameObject.Instantiate(prefab.GameObject, parent);
        sceneContext.Container.InjectGameObject(gameObject);
        return gameObject;
    }

}
