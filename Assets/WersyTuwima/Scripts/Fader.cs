using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Fader
{
    public static IEnumerator FadeComponent<T>(T component, Action<float> setValueAction, Action onComplete, float duration = 1.5f, float targetValue = 0f)
    {
        float elapsedTime = 0f;
        float startValue = component switch
        {
            CanvasGroup canvasGroup => canvasGroup.alpha,
            AudioSource audioSource => audioSource.volume,
            Image image => image.color.a,
            _ => throw new ArgumentException($"Unsupported component type: {typeof(T)}")
        };

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            setValueAction(currentValue);
            yield return null;
        }

        setValueAction(targetValue);
        onComplete?.Invoke();
    }
}