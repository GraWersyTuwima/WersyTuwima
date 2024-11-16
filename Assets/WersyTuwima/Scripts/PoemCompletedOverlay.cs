using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PoemCompletedOverlay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI _poemTitle;

    private CanvasGroup _canvasGroup;
    private AudioSource _poemRecitationSource;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void SetTitle(string title)
    {
        _poemTitle.text = $"<voffset=-0.4em>\"</voffset>{title}<voffset=0.1em>\"</voffset>";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_canvasGroup.interactable)
        {
            _canvasGroup.interactable = false;
            if (_poemRecitationSource != null)
            {
                StartCoroutine(AudioManager.Instance.FadeOutSound(_poemRecitationSource, 1f));
            }
            StartCoroutine(AudioManager.Instance.FadeMusic(2f, true));
            StartCoroutine(HidePoemCompletedOverlay());
        }
    }

    public IEnumerator ShowPoemCompletedOverlay(AudioClip poemRecitationSound)
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        yield return Fader.FadeComponent(_canvasGroup, (value) => _canvasGroup.alpha = value, () =>
            {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            },
            duration: 1f, targetValue: 1f);

        yield return new WaitForSeconds(1.5f);

        if (_canvasGroup.interactable)
            _poemRecitationSource = AudioManager.Instance.PlaySound(poemRecitationSound);
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
