using System;
using UnityEngine;

public class WszyscyDlaWszystkichLevel : MonoBehaviour
{
    private PoemCounter _poemCounter;

    public int PoemFragments { get; private set; } = 0;
    public int PoemFragmentsNeeded { get; private set; } = 1;

    public bool Outside { get; set; } = true;

    public Action RecitationAction => () =>
    {
        _poemCounter.PlayCompletionSequence();
    };

    private void Start()
    {
        _poemCounter = GameObject.FindGameObjectWithTag("PoemCounter").GetComponent<PoemCounter>();

        _poemCounter.OnFragmentCollected += () =>
        {
            if (Outside)
            {
                PoemFragments++;
            }
        };

        AttachEvent();
    }

    public void AttachEvent()
    {
        _poemCounter.OnCompletion += RecitationAction;
    }

    public void DetachEvent()
    {
        _poemCounter.OnCompletion -= RecitationAction;
    }
}
