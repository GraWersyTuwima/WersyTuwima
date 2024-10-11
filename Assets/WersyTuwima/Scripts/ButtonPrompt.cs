using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonPrompt : MonoBehaviour
{
    public float TimeToPressSeconds = 2f;
    public bool IsSuccess { get; set; }

    [SerializeField] private AudioClip _showSound;
    [SerializeField] private AudioClip _successSound;
    [SerializeField] private AudioClip _failSound;

    private TextMeshProUGUI _text;
    private Animator _animator;

    private ButtonPromptLetter[] _letters = new ButtonPromptLetter[]
    {
        new() { Letter = "Q", Key = KeyCode.Q },
        new() { Letter = "W", Key = KeyCode.W },
        new() { Letter = "E", Key = KeyCode.E },
        new() { Letter = "R", Key = KeyCode.R },
        new() { Letter = "A", Key = KeyCode.A },
        new() { Letter = "S", Key = KeyCode.S },
        new() { Letter = "D", Key = KeyCode.D },
        new() { Letter = "F", Key = KeyCode.F },
        new() { Letter = "Z", Key = KeyCode.Z },
        new() { Letter = "X", Key = KeyCode.X },
        new() { Letter = "C", Key = KeyCode.C },
        new() { Letter = "V", Key = KeyCode.V }
    };

    private ButtonPromptLetter _currentLetter;

    private float _timeToPress;
    private bool _isActive;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _animator = GetComponent<Animator>();
    }

    public void ShowPrompt()
    {
        if (_isActive) return;

        _isActive = true;
        _currentLetter = _letters[Random.Range(0, _letters.Length)];
        _text.text = _currentLetter.Letter;
        _timeToPress = Time.time + TimeToPressSeconds;
        _animator.SetTrigger("Show");
        AudioManager.Instance.PlaySound(_showSound);
    }

    public void HidePrompt()
    {
        _animator.SetTrigger("Hide");
        ResetPrompt();
        IsSuccess = true;
        AudioManager.Instance.PlaySound(_successSound);
    }

    public void FailPrompt()
    {
        if (!_isActive) return;

        _animator.SetTrigger("Fail");
        ResetPrompt();
        AudioManager.Instance.PlaySound(_failSound);
    }

    private void ResetPrompt()
    {
        _currentLetter = null;
        _isActive = false;
        _timeToPress = 0f;
    }

    private void Update()
    {
        if (!_isActive || _currentLetter == null) return;


        if (Input.GetKeyUp(_currentLetter.Key))
        {
            HidePrompt();
            return;
        }
        if (Time.time > _timeToPress)
        {
            FailPrompt();
            return;
        }

        foreach (var letter in _letters)
        {
            if (letter.Key != _currentLetter.Key && Input.GetKeyUp(letter.Key))
            {
                FailPrompt();
                break;
            }
        }
    }
}

public record ButtonPromptLetter
{
    public string Letter { get; set; }
    public KeyCode Key { get; set; }
}