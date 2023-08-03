using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NavigationButtonsPanel : MonoBehaviour
{
    [SerializeField] Button previousButton;
    [SerializeField] Button nextButton;
    [Inject] SignalBus signalBus;

    int firstTab;
    int maxTabs;
    int currentTab = 1;
    public void Init(int firstTab, int maxTabs)
    {
        this.firstTab = firstTab;
        this.maxTabs = maxTabs;
        SetButtonsAction();
        SetButtonsStatus();
    }

    void Awake()
    {
        signalBus.Subscribe<GameUISignals.TabChanged>(OnTabChanged);
    }
    void OnDisable()
    {
        signalBus.Unsubscribe<GameUISignals.TabChanged>(OnTabChanged);
    }
    void OnTabChanged(GameUISignals.TabChanged signalData)
    {
        currentTab = signalData.CurrentTab;
        SetButtonsStatus();
    }
    void OnDestroy()
    {
        ClearButtonsAction();
    }
    void SetButtonsStatus()
    {
        if (currentTab == firstTab)
        {
            previousButton.interactable = false;
            return;
        }
        if (currentTab == maxTabs)
        {
            nextButton.interactable = false;
            return;
        }
        previousButton.interactable = true;
        nextButton.interactable = true;
    }
    void SetButtonsAction()
    {
        previousButton.onClick.AddListener(() => signalBus.Fire(new ButtonClickedSignal.PreviousButtonClicked()));
        nextButton.onClick.AddListener(() => signalBus.Fire(new ButtonClickedSignal.NextButtonClicked()));
    }
    void ClearButtonsAction()
    {
        previousButton.onClick.RemoveAllListeners();
        nextButton.onClick.RemoveAllListeners();
    }

}