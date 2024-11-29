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
        Tuwim3,
    }

    public Dictionary<Note, string> Notes { get; } = new()
    {
        { Note.Pusta, "Tyle tu miejsca!\nWidz� w oddali... Ni� do szycia?\n\"Krawiec szyje ubrania\"..." },
        { Note.Wprowadzenie, "Witaj w grze <i>Wersy Tuwima</i>!\nGrasz jako Aleks � ch�opiec, kt�ry pragnie\njak najlepiej zapozna� si� z tw�rczo�ci�\nJuliana Tuwima. Podczas gry b�dziesz\neksplorowa� miejsca opisane w wierszach\npoety i bra� udzia� w grach inspirowanych\njego utworami!\nMo�esz otwiera� ten notatnik klikaj�c we\nwska�nik post�pu w prawym g�rnym rogu.\n\nMi�ej gry!" },
        { Note.Okulary, "Gdzie one mog� by�?\n\nTa stara szafa...\nPianino w domu?\nMyszy tutaj chodz�...\nTo lustro... wygl�da dziwnie." },
        { Note.Koniec, "Koniec gry" },

        { Note.Tuwim1, "Julian Tuwim jest uznawany za pierwszego\nnowoczesnego t�umacza poezji w Polsce.\nSzczeg�lnie ch�tnie przek�ada� utwory\npoetyckie z j�zyka rosyjskiego, cho�\nzajmowa� si� tak�e t�umaczeniem poezji\nfrancuskiej, �aci�skiej i niemieckiej." },
        { Note.Tuwim2, "Julian Tuwim mia� niezwykle r�norodne\nzainteresowania, kt�re obejmowa�y nie\ntylko literatur�, ale tak�e zagadnienia\ndemonologiczne i kolekcjonerstwo.\nFascynowa� si� diab�ami i demonami,\no kt�rych zgromadzi� tysi�ce ksi��ek.\nWyda� nawet dwie publikacje na ten temat,\nw kt�rych przedstawia� ciekawostki o\ndiab�ach w polskich wierzeniach." },
        { Note.Tuwim3, "Julian Tuwim by� znany ze swoich\nnietypowych kolekcji i fascynacji. Jedn� z\njego najbardziej osobliwych pasji by�y\nszczury. Poeta zgromadzi� imponuj�c�\nbibliotek� ksi��ek po�wi�conych tym\ngryzoniom i planowa� napisanie na ich temat\nmonografii oraz dramatu. Cho� te ambitne\nplany nigdy nie zosta�y zrealizowane,\npowsta� jeden wiersz inspirowany\nszczurami." },
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
