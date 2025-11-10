using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Notif : MonoBehaviour
{
    [SerializeField] private GameObject notifBox;
    [SerializeField] private float bounceHeight = 50f;
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float stayDuration = 1.5f;

    private RectTransform rectTransform;
    private Image image;

    void OnEnable()
    {
        notifBox.SetActive(false);
        rectTransform = notifBox.GetComponent<RectTransform>();
        image = notifBox.GetComponent<Image>();
    }

    public void StartAnimation()
    {
        notifBox.SetActive(true);

        Color startColor = image.color;
        startColor.a = 0f;
        image.color = startColor;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0);

        Sequence notifSeq = DOTween.Sequence();

        notifSeq.Append(image.DOFade(1f, fadeDuration))
                .Join(rectTransform.DOAnchorPosY(bounceHeight, fadeDuration).SetEase(Ease.OutBack))
                .AppendInterval(stayDuration)
                .Append(image.DOFade(0f, fadeDuration))
                .OnComplete(() => notifBox.SetActive(false));
    }
}
