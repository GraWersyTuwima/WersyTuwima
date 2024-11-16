using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinigameMouseHole : MonoBehaviour
{
    public Image Mouse { get; private set; }

    private void Start()
    {
        Mouse = transform.GetChild(0).GetComponent<Image>();
        Mouse.enabled = false;
    }

    public void ClickMouse()
    {
        Debug.Log("Clicked on mouse hole");
        Mouse.enabled = false;
    }
}
