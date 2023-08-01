using UnityEngine;

public class LoadingUI : MonoBehaviour, IWindow
{
    [SerializeField] LoadingIndicator loadingIndicator;
    [SerializeField] CanvasGroup canvasGroup;

    void Start()
    {
        loadingIndicator.PlaySequence();
    }

    public void Show()
    {
        canvasGroup.alpha = 1f;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
    }

}
