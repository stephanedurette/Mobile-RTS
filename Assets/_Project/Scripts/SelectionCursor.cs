using DG.Tweening;
using UnityEngine;

public class SelectionCursor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    float fadeDuration, scaleDuration;
    Color startColor, endColor;
    Vector3 startScale, apexScale, endScale;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        fadeDuration = 1f;
        scaleDuration = 1f;

        startColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        endColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);

        startScale = Vector3.one;
        apexScale = Vector3.one * 1.2f;
        endScale = Vector3.zero;
    }

    private void OnEnable()
    {
        spriteRenderer.color = startColor;
        spriteRenderer.transform.localScale = startScale;

        //Fade
        DOTween.Sequence()
            .Append(DOTween.To(() => spriteRenderer.color, x => spriteRenderer.color = x, endColor, fadeDuration).SetEase(Ease.OutCubic))
            .AppendCallback(() => gameObject.SetActive(false))
            .Play();

        //Scale
        DOTween.Sequence()
            .Append(DOTween.To(() => spriteRenderer.transform.localScale, x => spriteRenderer.transform.localScale = x, apexScale, scaleDuration * .2f).SetEase(Ease.InCubic))
            .Append(DOTween.To(() => spriteRenderer.transform.localScale, x => spriteRenderer.transform.localScale = x, endScale, scaleDuration * .8f).SetEase(Ease.OutCubic))
            .Play();
    }
}
