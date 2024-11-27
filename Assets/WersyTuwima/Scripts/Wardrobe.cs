using UnityEngine;

public class Wardrobe : InteractableObject
{
    protected override bool IsInteractable => false;

    protected override void Interact()
    {
        Debug.Log("Wardrobe");
    }
}
