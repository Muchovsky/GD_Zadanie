using System.Threading.Tasks;
using UnityEngine;
using Zenject;


public class GameUI : MonoBehaviour
{
    [SerializeField] ItemsPanel itemsPanel;
    [SerializeField] NavigationButtonsPanel navigationButtonsPanel;
    [Inject] ConnectionMock connectionMock = new ConnectionMock();
    [Inject] PrefabManager prefabManager;
    [Inject] SignalBus signalBus;
    LoadingUI loadingScreen;
    int currentTab = 1;
    int maxTabs;
    int totlalNumberOfItems;
    const int maxNumberOfItemsInTab = 5;

    void Awake()
    {
        signalBus.Subscribe<ButtonClickedSignal.PreviousButtonClicked>(OnPreviousButtonClicked);
        signalBus.Subscribe<ButtonClickedSignal.NextButtonClicked>(OnNextButtonClicked);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<ButtonClickedSignal.PreviousButtonClicked>(OnPreviousButtonClicked);
        signalBus.Unsubscribe<ButtonClickedSignal.NextButtonClicked>(OnNextButtonClicked);
    }


    private async void Start()
    {
        loadingScreen = prefabManager.GetPrefab<LoadingUI>(PrefabNameEnum.LOADINGSCREENUI, null);
        loadingScreen.Show();
        totlalNumberOfItems = await connectionMock.RequestNumberOfItems();
        GetNumberOfTabs(totlalNumberOfItems);
        var itemList = await connectionMock.RequestItems(GetIndex(), CountNumberOfItemsInTab());
        itemsPanel.Init(itemList);
        navigationButtonsPanel.Init(currentTab, maxTabs);
        loadingScreen.Hide();
    }

    void GetNumberOfTabs(int numberOfItems)
    {
        float value = (float)numberOfItems / maxNumberOfItemsInTab;
        maxTabs = Mathf.CeilToInt(value);
        Debug.Log(maxTabs);
        Debug.Log(numberOfItems);
    }

    int CountNumberOfItemsInTab()
    {
        int x = totlalNumberOfItems - (currentTab * maxNumberOfItemsInTab);
        return x > 0 ? maxNumberOfItemsInTab : GetLastTabItemCoint();
    }

    int GetLastTabItemCoint()
    {
        return maxNumberOfItemsInTab - ((currentTab * maxNumberOfItemsInTab) - totlalNumberOfItems);
    }

    int GetIndex()
    {
        return currentTab - 1;
    }

    void ChangeTabValue(bool next)
    {
        currentTab += next ? 1 : -1;
        signalBus.Fire(new GameUISignals.TabChanged { CurrentTab = currentTab });
    }

    private async void OnNextButtonClicked()
    {
        ChangeTabValue(true);
        await LoadNextTab();
    }

    private async void OnPreviousButtonClicked()
    {
        ChangeTabValue(false);
        await LoadNextTab();
    }

    private async Task LoadNextTab()
    {
        loadingScreen.Show();
        var itemList = await connectionMock.RequestItems(GetIndex(), CountNumberOfItemsInTab());
        itemsPanel.Init(itemList);
        loadingScreen.Hide();
    }
}
