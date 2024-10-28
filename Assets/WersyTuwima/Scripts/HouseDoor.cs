using System.Collections;
using UnityEngine;

public class HouseDoor : InteractableObject
{
    protected override void Interact()
    {
        Debug.Log("Interakcja z drzwiami");
    }
}