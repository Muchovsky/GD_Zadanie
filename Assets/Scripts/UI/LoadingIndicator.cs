using DG.Tweening;
using UnityEngine;

public class LoadingIndicator : MonoBehaviour
{
    [SerializeField] float duration = 5f;
    Vector3 endValue = new Vector3(0, 0, 360);
    Sequence loadingSequende;

    void Awake()
    {
        loadingSequende = DOTween.Sequence();
        loadingSequende.Append(transform.DOLocalRotate(endValue, duration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetRelative(true)).SetLoops(-1, LoopType.Restart);
    }

    void OnEnable()
    {
        PlaySequence();
    }

    void OnDisable()
    {
        PauseSequence();
    }

    void OnDestroy()
    {
        loadingSequende.Kill();
    }

    public void PauseSequence()
    {
        loadingSequende.Pause();
    }

    public void PlaySequence()
    {
        loadingSequende.Play();
    }
}