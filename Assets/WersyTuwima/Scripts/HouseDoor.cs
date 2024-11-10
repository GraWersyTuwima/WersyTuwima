using System.Collections;
using UnityEngine;

public class HouseDoor : InteractableObject
{
    [SerializeField]
    private bool _isInside = false;

    protected override void Interact()
    {
        if (_isInside)
        {
            Debug.Log("You are inside the house");
        }
        else
        {
            Debug.Log("You are outside the house");
        }
    }
}