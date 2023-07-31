using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] LoadingIndicator loadingIndicator;

    void Start()
    {
        loadingIndicator.PlaySequence();
    }
}
