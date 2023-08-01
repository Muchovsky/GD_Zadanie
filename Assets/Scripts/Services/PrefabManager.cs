using System;
using System.Collections.Generic;
using UnityEditor;
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

}
