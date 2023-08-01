using System;
using System.Collections.Generic;
using UnityEngine;
using static DataItem;

[CreateAssetMenu(menuName = "Config/Item Sprite List")]
public class ItemSpriteList : ScriptableObject
{
    [SerializeField] List<SpriteTouple> list = new List<SpriteTouple>();
    public List<SpriteTouple> PrefabsList => list;

    public SpriteTouple GetByType(CategoryType type)
    {
        return list.Find(x => x.Type == type);
    }
}
[Serializable]
public class SpriteTouple
{
    [SerializeField] CategoryType type;
    [SerializeField] Sprite sprite;
    public CategoryType Type { get { return type; } }
    public Sprite Sprite { get { return sprite; } }
}
