using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Prefab Definition List")]
public class PrefabList : ScriptableObject
{
    [SerializeField] List<PrefabTouple> list = new List<PrefabTouple>();
    public List<PrefabTouple> PrefabsList => list;

    public PrefabTouple GetByType(PrefabNameEnum name)
    {
        return list.Find(a => a.Name == name);
    }
}
[Serializable]
public class PrefabTouple
{
    [SerializeField] PrefabNameEnum name;
    [SerializeField] GameObject gameObject;
    public PrefabNameEnum Name { get { return name; } }
    public GameObject GameObject { get { return gameObject; } }
}
