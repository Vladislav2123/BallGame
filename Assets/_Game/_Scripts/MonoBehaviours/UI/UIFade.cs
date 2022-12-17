using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [SerializeField] private AnimationProperties _fadeAnimation;
    [SerializeField] private Image _fadingImage;

    public void FadeIn()
    {
        SetFade(0);
        PlayFadeAnimation(1);
    }

    public void FadeOut()
    {
        SetFade(1);
        PlayFadeAnimation(0);
    }

    private void PlayFadeAnimation(float fadeValue)
    {
        _fadingImage.DOFade(fadeValue, _fadeAnimation.Duration).SetEase(_fadeAnimation.Ease);
    }

    private void SetFade(float value)
    {
        Color color = _fadingImage.color;
        color.a = value;
        _fadingImage.color = color;
    }
}