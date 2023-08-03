using DG.Tweening;
using UnityEngine;

public class LoadingIndicator : MonoBehaviour
{
    [SerializeField] float duration = 5f;
    Sequence loadingSequende;
    Vector3 endValue = new Vector3(0, 0, 360);

    void Awake()
    {
        loadingSequende = DOTween.Sequence();
        loadingSequende.Append(transform.DOLocalRotate(endValue, duration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetRelative(true)).SetLoops(-1, LoopType.Restart);
    }

    public void PlaySequence()
    {
        loadingSequende.Play();
    }

    public void PauseSequence()
    {
        loadingSequende.Pause();
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
}
