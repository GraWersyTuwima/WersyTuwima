using UnityEngine;

public class Thread : InteractableObject
{
    [SerializeField]
    private SewingMinigame _sewingMinigame;

    protected override bool IsInteractable => _sewingMinigame != null;

    protected override void Interact()
    {
        _sewingMinigame.Run();
    }
}
