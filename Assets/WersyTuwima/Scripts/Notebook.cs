using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour
{
    [SerializeField] private Image _parentPanel;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _nextButton;

    [SerializeField] private AudioClip _pageTurnSound;

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

        Tuwim1,
        Tuwim2,
    }

    public Dictionary<Note, string> Notes { get; } = new()
    {
        { Note.Pusta, "Tyle tu miejsca!\nWidzê w oddali... Niæ do szycia?\n\"Krawiec szyje ubrania\"..." },
        { Note.Wprowadzenie, "Witaj w grze <i>Wersy Tuwima</i>!\nGrasz jako Aleks – ch³opiec, który pragnie\njak najlepiej zapoznaæ siê z twórczoœci¹\nJuliana Tuwima. Podczas gry bêdziesz\neksplorowa³ miejsca opisane w wierszach\npoety i bra³ udzia³ w grach inspirowanych\njego utworami!\nMo¿esz otwieraæ ten notatnik klikaj¹c we\nwskaŸnik postêpu w prawym górnym rogu.\n\nMi³ej gry!" },
        { Note.Okulary, "Gdzie one mog¹ byæ?\n\nTa stara szafa...\nPianino w domu?\nMyszy tutaj chodz¹...\nTo lustro... wygl¹da dziwnie." },
        { Note.Koniec, "Koniec gry" },

        { Note.Tuwim1, "Julian Tuwim jest uznawany za pierwszego\nnowoczesnego t³umacza poezji w Polsce.\nSzczególnie chêtnie przek³ada³ utwory\npoetyckie z jêzyka rosyjskiego, choæ\nzajmowa³ siê tak¿e t³umaczeniem poezji\nfrancuskiej, ³aciñskiej i niemieckiej." },
        { Note.Tuwim2, "O Julianie Tuwimie " },
    };

    private List<Note> _pages = new()
    {
        Note.Pusta,
    };

    public int CurrentPage { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _parentPanel.raycastTarget = false;

        AddPage(Note.Tuwim1);
        AddPage(Note.Tuwim2);
        UpdateButtons();
    }

    public void Toggle()
    {
        _isVisible = !_isVisible;
        _animator.ResetTrigger(_isVisible ? "Hide" : "Show");
        _animator.SetTrigger(_isVisible ? "Show" : "Hide");

        _parentPanel.raycastTarget = _isVisible;

        OnVisibilityChanged?.Invoke(_isVisible);
    }

    public void Back()
    {
        if (CurrentPage <= 0) return;

        CurrentPage--;
        SetText(Notes[_pages[CurrentPage]]);

        UpdateButtons();
        AudioManager.Instance.PlaySound(_pageTurnSound);
    }

    public void Next()
    {
        if (CurrentPage >= _pages.Count - 1) return;

        CurrentPage++;
        SetText(Notes[_pages[CurrentPage]]);

        UpdateButtons();
        AudioManager.Instance.PlaySound(_pageTurnSound);
    }

    public void SetTaskNote(Note note)
    {
        _pages[0] = note;
        SetText(Notes[note]);
    }

    public void AddPage(Note note)
    {
        _pages.Add(note);
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        _backButton.gameObject.SetActive(CurrentPage > 0);
        _nextButton.gameObject.SetActive(CurrentPage < _pages.Count - 1);
    }

    private void SetText(string text)
    {
        _text.text = $"<line-height=100%>{text}</line-height>";
    }
}
