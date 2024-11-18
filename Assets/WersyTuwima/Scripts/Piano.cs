using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : InteractableObject
{
    public AudioClip[] pianoSounds;

    private PoemSpawner _poemSpawner;
    private ButtonPromptMinigame _buttonPromptMinigame;

    private void Start()
    {
        _poemSpawner = GetComponentInChildren<PoemSpawner>();
        _buttonPromptMinigame = GetComponentInChildren<ButtonPromptMinigame>();
        ObjectCollider = GetComponentInChildren<CapsuleCollider2D>();
    }

    protected override void Interact()
    {
        InteractableObject.CanInteract = false;
        AlexController.Instance.CanMove = false;

        _buttonPromptMinigame.OnCorrectClick += () =>
        {
            PlayRandomSound();
        };

        _buttonPromptMinigame.OnFinish += () =>
        {
            _poemSpawner.SpawnPoem();
            InteractableObject.CanInteract = true;
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
