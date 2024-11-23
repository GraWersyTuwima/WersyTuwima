using System;
using System.Collections.Generic;
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

    public event Action<bool> OnVisibilityChanged;

    public Dictionary<string, string> Notes { get; } = new()
    {
        { "Pusta", "Hmmm... Co jeszcze mo¿na tu zrobiæ?" },
        { "Wprowadzenie", "Witaj w grze Wersy Tuwima!\nGrasz jako Aleks - ch³opiec, który chce\nzapoznaæ siê z jak najwiêksz¹ liczb¹\ntwórczoœci Juliana Tuwima.\nBêdziesz eksplorowa³ miejsca z wierszy\ni bawi³ siê w gry nawi¹zuj¹ce do nich!\n\nMo¿esz otwieraæ ten notatnik klikaj¹c w\nlicznik postêpu w prawym górnym rogu.\n\nMi³ej gry!" },
        { "Okulary", "Gdzie one mog¹ byæ?\n\nTa stara szafa...\nPianino w domu?\nMyszy tutaj chodz¹...\nTo lustro... wygl¹da dziwnie." },
        { "Koniec", "Koniec gry" },
    };

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

        OnVisibilityChanged?.Invoke(_isVisible);
    }

    public void SetText(string noteName)
    {
        if (Notes.TryGetValue(noteName, out string note))
        {
            _text.text = note;
        }
        else
        {
            Debug.LogWarning($"Note with name {noteName} not found.");
        }
    }
}
