using System;
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

    private int _fragmentsCount = 0;

    private int _poemFragmentsNeeded = 0;

    public event Action OnCompletion;

    private void Start()
    {
        _text.text = $"{_fragmentsCount}/{_poemFragmentsNeeded}";
        _poemCompletedOverlay.SetTitle("Okulary");
    }

    public void IncrementFragmentsCount()
    {
        _fragmentsCount++;
        _text.text = $"{_fragmentsCount}/{_poemFragmentsNeeded}";

        if (_fragmentsCount == _poemFragmentsNeeded)
        {
            OnCompletion?.Invoke();
        }
    }

    public void SetFragmentsNeeded(int fragmentsNeeded)
    {
        _poemFragmentsNeeded = fragmentsNeeded;
        _text.text = $"{_fragmentsCount}/{_poemFragmentsNeeded}";
    }

    public void PlayCompletionSequence()
    {
        StartCoroutine(AudioManager.Instance.FadeMusic(1f, false));

        AudioManager.Instance.PlaySound(_poemsCollectedSound);

        StartCoroutine(_poemCompletedOverlay.ShowPoemCompletedOverlay(_poemRecitationSound));
    }
}