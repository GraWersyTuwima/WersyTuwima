using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHole : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _mouseSounds;

    private Image _mouse;

    private void Start()
    {
        _mouse = transform.GetChild(0).GetComponent<Image>();
        _mouse.enabled = false;

        StartCoroutine(MouseSound());
    }

    private IEnumerator MouseSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));

            AudioManager.Instance.PlaySound(_mouseSounds[Random.Range(0, _mouseSounds.Length)]);

            _mouse.enabled = true;
            yield return new WaitForSeconds(0.5f);
            _mouse.enabled = false;
        }
    }

    public void ClickMouse()
    {
        Debug.Log("Clicked on mouse hole");
    }
}
