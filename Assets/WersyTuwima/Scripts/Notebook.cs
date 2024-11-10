using TMPro;
using UnityEngine;

public class Notebook : MonoBehaviour
{
    [SerializeField]
    [TextArea(3, 10)]
    private string _notebookText;

    private Animator _animator;
    private TextMeshProUGUI _text;

    private bool _isVisible;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _text.text = _notebookText;
    }

    public void Toggle()
    {
        _isVisible = !_isVisible;
        _animator.SetTrigger(_isVisible ? "Show" : "Hide");
    }
}
