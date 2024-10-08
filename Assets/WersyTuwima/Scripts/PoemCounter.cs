using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoemCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    [SerializeField]
    private PoemCompletedOverlay _poemCompletedOverlay;

    [SerializeField]
    private AudioClip _poemRecitationSound;

    [SerializeField]
    private AudioClip _poemsCollectedSound;

    private int _poemsCount = 0;
    private int _poemsNeeded = 6;

    private void Start()
    {
        _text.text = $"{_poemsCount}/{_poemsNeeded}";
    }

    public void IncrementPoemsCount()
    {
        _poemsCount++;
        _text.text = $"{_poemsCount}/{_poemsNeeded}";

        if (_poemsCount == _poemsNeeded)
        {
            PlayCompletionSequence();
        }
    }

    private void PlayCompletionSequence()
    {
        StartCoroutine(AudioManager.Instance.FadeMusic(1f, false));

        AudioManager.Instance.PlaySound(_poemsCollectedSound);

        StartCoroutine(_poemCompletedOverlay.ShowPoemCompletedOverlay(_poemRecitationSound));
    }
}