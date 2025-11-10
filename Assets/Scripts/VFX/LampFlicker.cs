using UnityEngine;
using DG.Tweening;

public class LampFlicker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float minInterval = 0.1f;
    [SerializeField] private float maxInterval = 0.5f;

    private Tween flickerTween;

    void Start()
    {
        StartFlicker();
    }

    void OnDisable()
    {
        StopFlicker();
    }

    public void StartFlicker()
    {
        FlickerLoop(); // Start the flicker loop
    }

    public void StopFlicker()
    {
        flickerTween?.Kill();
        spriteRenderer.DOFade(1f, 0.1f); // Ensure it's fully visible when stopped
    }

    private void FlickerLoop()
    {
        float nextInterval = Random.Range(minInterval, maxInterval);
        float targetAlpha = spriteRenderer.color.a == 1f ? 0.8f : 1f;

        flickerTween = spriteRenderer.DOFade(targetAlpha, 0.05f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(nextInterval, FlickerLoop);
        });
    }
}
