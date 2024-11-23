using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour
{
    [SerializeField]
    private Image _parentPanel;

    private Animator _animator;
    private TextMeshProUGUI _text;

    private bool _isVisible;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _parentPanel.raycastTarget = false;
    }

    public void Toggle()
    {
        _isVisible = !_isVisible;
        _animator.ResetTrigger(_isVisible ? "Hide" : "Show");
        _animator.SetTrigger(_isVisible ? "Show" : "Hide");

        _parentPanel.raycastTarget = _isVisible;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
