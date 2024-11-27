using System;
using UnityEngine;

public class HouseLevel : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _glasses;

    private Notebook _notebook;
    private PoemCounter _poemCounter;
    private InteractableMirror _interactableMirror;

    private int _poemFragments = 0;
    private int _poemFragmentsNeeded = 2;

    private bool _inside = false;

    private Action ToggleInteractableMirror => () =>
    {
        _interactableMirror.ToggleInteractable();
        _glasses.enabled = true;
    };

    private void Start()
    {
        _notebook = GameObject.FindGameObjectWithTag("Notebook").GetComponent<Notebook>();
        _poemCounter = GameObject.FindGameObjectWithTag("PoemCounter").GetComponent<PoemCounter>();
        _interactableMirror = GetComponentInChildren<InteractableMirror>();
        _interactableMirror.PoemCounter = _poemCounter;
        _glasses.enabled = false;

        _poemCounter.OnFragmentCollected += () =>
        {
            if (_inside)
            {
                _poemFragments++;
            }
        };
    }

    public void Enter()
    {
        _notebook.SetText(Notebook.Note.Okulary);
        _poemCounter.SetPoemType(PoemCounter.Poem.Okulary);
        _poemCounter.SetFragments(_poemFragments);
        _poemCounter.SetFragmentsNeeded(_poemFragmentsNeeded);
        _poemCounter.OnCompletion += ToggleInteractableMirror;

        _inside = true;
    }

    public void Exit()
    {
        _notebook.SetText(Notebook.Note.Pusta);
        _poemCounter.SetPoemType(PoemCounter.Poem.WszyscyDlaWszystkich);
        _poemCounter.SetFragments(0);
        _poemCounter.SetFragmentsNeeded(0);
        _poemCounter.OnCompletion -= ToggleInteractableMirror;

        _inside = false;
    }
}
