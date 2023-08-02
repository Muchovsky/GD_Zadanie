using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class ItemsPanel : MonoBehaviour
{
    List<Item> items = new List<Item>();
    [Inject] PrefabManager prefabManager;
    [Inject] SignalBus signalBus;
    int currentTab = 1;
    const int maxNumberOfItemsInTab = 5;

    void Awake()
    {
        signalBus.Subscribe<GameUISignals.TabChanged>(OnTabChanged);

    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<GameUISignals.TabChanged>(OnTabChanged);

    }

    public void Init(List<DataItem> items)
    {
        CreateAttributes(items);
    }

    public void Init(IList<DataItem> items)
    {
        List<DataItem> itemsList = new List<DataItem>(items);
        CreateAttributes(itemsList);
    }

    void OnTabChanged(GameUISignals.TabChanged signalData)
    {
        currentTab = signalData.CurrentTab;
    }

    void CreateAttributes(List<DataItem> items)
    {
        PopulateList(items.Count);
        HideExcessItems(items.Count);
        InitItems(items);
    }
    void PopulateList(int count)
    {
        while (items.Count < count)
        {
            var prefab = prefabManager.GetPrefab<Item>(PrefabNameEnum.ITEM, transform);
            items.Add(prefab);
        }
    }
    void HideExcessItems(int count)
    {
        for (int i = count; i < items.Count; i++)
        {
            items[i].gameObject.SetActive(false);
        }
    }
    void InitItems(List<DataItem> dataItems)
    {
        for (int i = 0; i < dataItems.Count; i++)
        {
            items[i].Init(CalculateIndex(i), dataItems[i]);
            items[i].gameObject.SetActive(true);
        }
    }

    int CalculateIndex(int i)
    {
        return i + ((currentTab - 1) * maxNumberOfItemsInTab) + 1;
    }

}
