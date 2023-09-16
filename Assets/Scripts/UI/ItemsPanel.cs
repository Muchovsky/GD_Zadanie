using System.Collections.Generic;
using System.Threading.Tasks;
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

    void OnDisable()
    {
        signalBus.Unsubscribe<GameUISignals.TabChanged>(OnTabChanged);
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

    async void CreateAttributes(List<DataItem> items)
    {
        var populateTask = PopulateList(items.Count);
        await populateTask;
        HideExcessItems(items.Count);
        var initTask = InitItems(items);
        await initTask;
    }

    async Task PopulateList(int count)
    {
        while (items.Count < count)
        {
            Item prefab = await prefabManager.GetPrefabAsync<Item>("Item", transform);
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

    async Task InitItems(List<DataItem> dataItems)
    {
        for (int i = 0; i < dataItems.Count; i++)
        {
            await items[i].Init(CalculateIndex(i), dataItems[i]);
            items[i].gameObject.SetActive(true);
        }
    }

    int CalculateIndex(int i)
    {
        return i + ((currentTab - 1) * maxNumberOfItemsInTab) + 1;
    }
}