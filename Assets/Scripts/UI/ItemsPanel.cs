using System.Collections.Generic;
using UnityEngine;

public class ItemsPanel : MonoBehaviour
{
    [SerializeField] Item itemPrefab;
    List<Item> items;
    public void Init(List<Item> items)
    {
        CreateAttributes(items);
    }

    void CreateAttributes(List<Item> items)
    {
        PopulateList(items.Count);
    }
    void PopulateList(int count)
    {
        while (items.Count < count)
        {
            var prefab = Instantiate(itemPrefab, transform);
            items.Add(prefab);
        }
    }
}
