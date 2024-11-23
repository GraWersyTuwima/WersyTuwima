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
        { "Pusta", "Hmmm... Co jeszcze mo�na tu zrobi�?" },
        { "Wprowadzenie", "Witaj w grze Wersy Tuwima!\nGrasz jako Aleks - ch�opiec, kt�ry chce\nzapozna� si� z jak najwi�ksz� liczb�\ntw�rczo�ci Juliana Tuwima.\nB�dziesz eksplorowa� miejsca z wierszy\ni bawi� si� w gry nawi�zuj�ce do nich!\n\nMo�esz otwiera� ten notatnik klikaj�c w\nlicznik post�pu w prawym g�rnym rogu.\n\nMi�ej gry!" },
        { "Okulary", "Gdzie one mog� by�?\n\nTa stara szafa...\nPianino w domu?\nMyszy tutaj chodz�...\nTo lustro... wygl�da dziwnie." },
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
