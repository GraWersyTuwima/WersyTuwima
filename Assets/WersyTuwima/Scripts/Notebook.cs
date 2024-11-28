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

    public enum Note
    {
        Pusta,
        Wprowadzenie,
        Okulary,
        Koniec,
    }

    public Dictionary<Note, string> Notes { get; } = new()
    {
        { Note.Pusta, "Tyle tu miejsca!\nWidzê w oddali... Niæ do szycia?\n\"Krawiec szyje ubrania\"..." },
        { Note.Wprowadzenie, "Witaj w grze <i>Wersy Tuwima</i>!\nGrasz jako Aleks – ch³opiec, który pragnie\njak najlepiej zapoznaæ siê z twórczoœci¹\nJuliana Tuwima. Podczas gry bêdziesz\neksplorowa³ miejsca opisane w wierszach\npoety i bra³ udzia³ w grach inspirowanych\njego utworami!\nMo¿esz otwieraæ ten notatnik klikaj¹c we\nwskaŸnik postêpu w prawym górnym rogu.\n\nMi³ej gry!" },
        { Note.Okulary, "Gdzie one mog¹ byæ?\n\nTa stara szafa...\nPianino w domu?\nMyszy tutaj chodz¹...\nTo lustro... wygl¹da dziwnie." },
        { Note.Koniec, "Koniec gry" },
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

    public void SetText(Note note)
    {
        if (Notes.TryGetValue(note, out string noteText))
        {
            _text.text = noteText;
        }
        else
        {
            Debug.LogWarning($"Note with name {note} not found.");
        }
    }
}
