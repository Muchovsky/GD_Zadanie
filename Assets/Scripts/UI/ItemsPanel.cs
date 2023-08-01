using System.Collections.Generic;
using UnityEngine;

public class ItemsPanel : MonoBehaviour
{
    [SerializeField] Item itemPrefab;
    List<Item> items;
    public void Init(List<DataItem> items)
    {
        CreateAttributes(items);
    }
    void CreateAttributes(List<DataItem> items)
    {
        PopulateList(items.Count);
        InitItems(items);
    }
    void PopulateList(int count)
    {
        while (items.Count < count)
        {
            var prefab = Instantiate(itemPrefab, transform);
            items.Add(prefab);
        }
    }
    void InitItems(List<DataItem> dataItems)
    {
        foreach (var item in items)
        {
            item.Init(0, dataItems[0]); //TODO
        }
    }
}
