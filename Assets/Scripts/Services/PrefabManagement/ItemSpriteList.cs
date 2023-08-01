using System;
using System.Collections.Generic;
using UnityEngine;
using static DataItem;

[CreateAssetMenu(menuName = "Config/Prefab Definition List")]
public class items : ScriptableObject
{
    [SerializeField] List<SpriteTouple> list = new List<SpriteTouple>();
    public List<SpriteTouple> PrefabsList => list;

    public SpriteTouple GetByName(CategoryType name)
    {
        return list.Find(a => a.Name == name);
    }
}
[Serializable]
public class SpriteTouple
{
    [SerializeField] CategoryType name;
    [SerializeField] GameObject gameObject;
    public CategoryType Name { get { return name; } }
    public GameObject GameObject { get { return gameObject; } }
}
