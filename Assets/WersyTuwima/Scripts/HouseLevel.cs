using System;
using UnityEngine;

public class HouseLevel : MonoBehaviour
{
    private Notebook _notebook;
    private PoemCounter _poemCounter;
    private InteractableMirror _interactableMirror;

    private Action ToggleInteractableMirror => () => _interactableMirror.ToggleInteractable();

    private void Start()
    {
        _notebook = GameObject.FindGameObjectWithTag("Notebook").GetComponent<Notebook>();
        _poemCounter = GameObject.FindGameObjectWithTag("PoemCounter").GetComponent<PoemCounter>();
        _interactableMirror = GetComponentInChildren<InteractableMirror>();
        _interactableMirror.PoemCounter = _poemCounter;
    }

    public void Enter()
    {
        _notebook.SetText(Notebook.Note.Okulary);
        _poemCounter.SetFragmentsNeeded(2);
        _poemCounter.OnCompletion += ToggleInteractableMirror;
    }

    public void Exit()
    {
        _notebook.SetText(Notebook.Note.Pusta);
        _poemCounter.SetFragmentsNeeded(0);
        _poemCounter.OnCompletion -= ToggleInteractableMirror;
    }
}
