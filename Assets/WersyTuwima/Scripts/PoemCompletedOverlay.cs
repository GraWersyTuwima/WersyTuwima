using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PoemCompletedOverlay : MonoBehaviour, IPointerClickHandler
{
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_canvasGroup.interactable)
        {
            _canvasGroup.interactable = false;
            AudioManager.Instance.StopSoundWithFade(0.5f);
            StartCoroutine(AudioManager.Instance.FadeInMusic(1f));
            StartCoroutine(HidePoemCompletedOverlay());
        }
    }

    public IEnumerator ShowPoemCompletedOverlay(AudioClip poemRecitationSound)
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            _canvasGroup.alpha = alpha;
            yield return null;
        }

        _canvasGroup.interactable = true;
        AudioManager.Instance.PlaySound(poemRecitationSound);
    }

    private IEnumerator HidePoemCompletedOverlay()
    {
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            _canvasGroup.alpha = alpha;
            yield return null;
        }
    }
}
