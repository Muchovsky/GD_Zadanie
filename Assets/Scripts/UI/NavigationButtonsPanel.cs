using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NavigationButtonsPanel : MonoBehaviour
{
    [SerializeField] Button previousButton;
    [SerializeField] Button nextButton;

    [Inject] PrefabManager prefabManager;
    void Awake()
    {
        SetButtonsAction();
    }
    //TBD if needed
    void OnDestroy()
    {
        ClearButtonsAction();
    }

    void ClearButtonsAction()
    {
        previousButton.onClick.RemoveAllListeners();
        nextButton.onClick.RemoveAllListeners();
    }


    void SetButtonsAction()
    {
        previousButton.onClick.AddListener(() => Test());
        nextButton.onClick.AddListener(() => Debug.Log("ActionNEeded"));
    }

    [ContextMenu("test")]
    void Test()
    {
        prefabManager.GetPrefab<LoadingUI>(PrefabNameEnum.LOADINGSCREENUI,null);
    }



}
