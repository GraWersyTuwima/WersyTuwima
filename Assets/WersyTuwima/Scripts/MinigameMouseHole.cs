using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinigameMouseHole : MonoBehaviour
{
    public Image Mouse { get; private set; }

    public event System.Action<bool> OnMouseClick;

    private void Start()
    {
        Mouse = transform.GetChild(0).GetComponent<Image>();
        Mouse.enabled = false;
    }

    public void ClickMouse()
    {
        OnMouseClick?.Invoke(Mouse.enabled);
    }
}
