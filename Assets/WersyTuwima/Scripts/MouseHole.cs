using UnityEngine;

public class MouseHole : InteractableObject
{
    [SerializeField]
    private MouseMinigame _mouseMinigame;

    protected override void Interact()
    {
        _mouseMinigame.Interact();
    }
}
