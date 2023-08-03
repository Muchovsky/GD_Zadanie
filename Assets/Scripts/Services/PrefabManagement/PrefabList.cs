using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Prefab Definition List")]
public class PrefabList : ScriptableObject
{
    public List<PrefabTouple> PrefabsList => list;
    [SerializeField] List<PrefabTouple> list = new List<PrefabTouple>();
    public PrefabTouple GetByName(PrefabNameEnum name)
    {
        return list.Find(a => a.Name == name);
    }
}

[Serializable]
public class PrefabTouple
{
    public GameObject GameObject { get { return gameObject; } }
    public PrefabNameEnum Name { get { return name; } }
    [SerializeField] GameObject gameObject;
    [SerializeField] PrefabNameEnum name;
}