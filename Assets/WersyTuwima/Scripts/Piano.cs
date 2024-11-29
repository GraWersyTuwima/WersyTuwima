using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : InteractableObject
{
    public AudioClip[] pianoSounds;

    private PoemSpawner _poemSpawner;
    private ButtonPromptMinigame _buttonPromptMinigame;
    private Notebook _notebook;

    private bool _hasPlayedPiano = false;

    private void Start()
    {
        _poemSpawner = GetComponentInChildren<PoemSpawner>();
        _buttonPromptMinigame = GetComponentInChildren<ButtonPromptMinigame>();
        _notebook = GameObject.FindGameObjectWithTag("Notebook").GetComponent<Notebook>();
        ObjectCollider = GetComponentInChildren<CapsuleCollider2D>();
    }

    protected override void Interact()
    {
        if (!_hasPlayedPiano)
        {
            _hasPlayedPiano = true;
            RunMinigame();
        }
        else
        {
            PlayRandomSound();
        }
    }

    private void RunMinigame()
    {
        InteractableObject.AnyInteractionsEnabled = false;
        AlexController.Instance.CanMove = false;

        _buttonPromptMinigame.OnCorrectClick += () =>
        {
            PlayRandomSound();
        };

        _buttonPromptMinigame.OnFinish += () =>
        {
            _notebook.AddPage(Notebook.Note.Tuwim2);
            _poemSpawner.SpawnPoem();
            InteractableObject.AnyInteractionsEnabled = true;
            AlexController.Instance.CanMove = true;
        };

        _buttonPromptMinigame.PlaySuccessSound = false;
        _buttonPromptMinigame.StartMinigame();
    }

    private void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, pianoSounds.Length);
        AudioManager.Instance.PlaySound(pianoSounds[randomIndex], 1f);
    }
}
