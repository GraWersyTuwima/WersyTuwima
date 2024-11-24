using UnityEngine;

public class HouseLevel : MonoBehaviour
{
    private Notebook _notebook;
    private PoemCounter _poemCounter;

    private void Start()
    {
        _notebook = GameObject.FindGameObjectWithTag("Notebook").GetComponent<Notebook>();
        _poemCounter = GameObject.FindGameObjectWithTag("PoemCounter").GetComponent<PoemCounter>();
    }

    public void Enter()
    {
        _notebook.SetText(Notebook.Note.Okulary);
        _poemCounter.SetFragmentsNeeded(2);
        _poemCounter.OnCompletion += _poemCounter.PlayCompletionSequence;
    }

    public void Exit()
    {
        _notebook.SetText(Notebook.Note.Pusta);
        _poemCounter.SetFragmentsNeeded(0);
        _poemCounter.OnCompletion -= _poemCounter.PlayCompletionSequence;
    }
}
