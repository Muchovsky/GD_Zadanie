using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class PrefabManager
{
    SceneContext sceneContext;
    List<GameObject> windowsPool = new();
    AsyncOperationHandle<GameObject> opHandle;

    [Inject]
    public PrefabManager(SceneContext sceneContext)
    {
        this.sceneContext = sceneContext;
    }

    public async Task<T> GetPrefabAsync<T>(string prefabName, Transform parent) where T : Component
    {
        if (!windowsPool.FirstOrDefault(x => x.gameObject.name == prefabName))
        {
            opHandle = Addressables.LoadAssetAsync<GameObject>(prefabName);
            await opHandle.Task;
            if (opHandle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject obj = opHandle.Result;

                GameObject gameObject = GameObject.Instantiate(obj, parent);
                sceneContext.Container.InjectGameObject(gameObject);
                if (gameObject.GetComponent<T>() is IWindow)
                {
                    windowsPool.Add(gameObject);
                }
                return gameObject.GetComponent<T>();
            }
            else
            {
                Debug.LogError($"Trying to Instantiate {prefabName} failed");
                return null;
            }
        }
        else
        {
            return GetFromPool(prefabName).GetComponent<T>(); ;
        }
    }

    GameObject GetFromPool(string prefabName)
    {
        return windowsPool.FirstOrDefault(x => x.gameObject.name == prefabName);
    }

    private void OnDestroy()
    {
        Addressables.Release(opHandle);
    }
}