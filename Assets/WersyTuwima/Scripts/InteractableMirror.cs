using UnityEngine;

public class InteractableMirror : InteractableObject
{
    private bool _isInteractable;
    protected override bool IsInteractable => _isInteractable;

    public PoemCounter PoemCounter { get; set; }

    protected override void Interact()
    {
        PoemCounter.PlayCompletionSequence();
        _isInteractable = false;
    }

    public void ToggleInteractable()
    {
        _isInteractable = !_isInteractable;
    }
}
