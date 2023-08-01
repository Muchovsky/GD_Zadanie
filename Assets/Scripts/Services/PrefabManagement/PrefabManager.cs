using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PrefabManager
{
    PrefabList prefabList;
    SceneContext sceneContext;
    Dictionary<PrefabNameEnum, GameObject> windowsPool = new();

    [Inject]
    public PrefabManager(PrefabList prefabList, SceneContext sceneContext)
    {
        this.prefabList = prefabList;
        this.sceneContext = sceneContext;
    }

    public T GetPrefab<T>(PrefabNameEnum prefabName, Transform parent) where T : Component
    {
        var newGameObject = GetGameObject<T>(prefabName, parent);
        return newGameObject.GetComponent<T>();
    }

    GameObject GetGameObject<T>(PrefabNameEnum prefabName, Transform parent) where T : Component
    {
        var prefab = prefabList.GetByName(prefabName);
        if (prefab == null || prefab.GameObject == null)
        {
            Debug.LogErrorFormat("Trying Instantiate {0} failed", prefabName.ToString());
            return null;
        }

        if (!windowsPool.ContainsKey(prefabName))
        {
            var gameObject = GameObject.Instantiate(prefab.GameObject, parent);
            sceneContext.Container.InjectGameObject(gameObject);
            if (gameObject.GetComponent<T>() is IWindow)
            {
                windowsPool.Add(prefabName, gameObject);
            }
            return gameObject;
        }
        else
        {
            return GetFromPool(prefabName);
        }
    }

    GameObject GetFromPool(PrefabNameEnum prefabName)
    {
        return windowsPool.GetValueOrDefault(prefabName);
    }
}
