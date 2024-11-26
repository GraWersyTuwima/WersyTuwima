using UnityEngine;

public class Helmet : InteractableObject
{
    private PoemSpawner _poemSpawner;

    private bool _hasPlayed = false;

    private void Start()
    {
        _poemSpawner = GetComponentInChildren<PoemSpawner>();
    }

    protected override void Interact()
    {
        if (!_hasPlayed)
        {
            _hasPlayed = true;
            RunMinigame();
        }
    }

    private void RunMinigame()
    {
        InteractableObject.AnyInteractionsEnabled = false;
        AlexController.Instance.CanMove = false;
        
    }
}
