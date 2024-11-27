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
    private AudioClip[] _poemRecitations;

    [SerializeField]
    private AudioClip _poemsCollectedSound;

    private int _fragmentsCount = 0;

    private int _poemFragmentsNeeded = 0;

    public enum Poem
    {
        Okulary = 0,
        WszyscyDlaWszystkich = 1,
    }

    private Poem _poemType = Poem.WszyscyDlaWszystkich;

    public event Action OnFragmentCollected;
    public event Action OnCompletion;

    private void Start()
    {
        _text.text = $"{_fragmentsCount}/{_poemFragmentsNeeded}";
        _poemCompletedOverlay.SetTitle("Wszyscy dla wszystkich");
    }

    public void IncrementFragmentsCount()
    {
        OnFragmentCollected?.Invoke();
        _fragmentsCount++;
        _text.text = $"{_fragmentsCount}/{_poemFragmentsNeeded}";

        if (_fragmentsCount == _poemFragmentsNeeded)
        {
            OnCompletion?.Invoke();
        }
    }

    public void SetFragments(int fragmentsCount)
    {
        _fragmentsCount = fragmentsCount;
        _text.text = $"{_fragmentsCount}/{_poemFragmentsNeeded}";
    }

    public void SetFragmentsNeeded(int fragmentsNeeded)
    {
        _poemFragmentsNeeded = fragmentsNeeded;
        _text.text = $"{_fragmentsCount}/{_poemFragmentsNeeded}";
    }

    public void SetPoemType(Poem poemType)
    {
        _poemType = poemType;

        string poemName = _poemType switch
        {
            Poem.Okulary => "Okulary",
            Poem.WszyscyDlaWszystkich => "Wszyscy dla wszystkich",
            _ => throw new ArgumentOutOfRangeException()
        };

        _poemCompletedOverlay.SetTitle(poemName);
    }

    public void PlayCompletionSequence()
    {
        StartCoroutine(AudioManager.Instance.FadeMusic(1f, false));

        AudioManager.Instance.PlaySound(_poemsCollectedSound);

        StartCoroutine(_poemCompletedOverlay.ShowPoemCompletedOverlay(_poemRecitations[(int)_poemType]));
    }
}