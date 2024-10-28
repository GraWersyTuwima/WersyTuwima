using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : InteractableObject
{
    public AudioClip[] pianoSounds;

    private PoemSpawner _poemSpawner;

    private int _counter = 0;

    private void Start()
    {
        _poemSpawner = GetComponentInChildren<PoemSpawner>();
        ObjectCollider = GetComponentInChildren<CapsuleCollider2D>();
    }

    protected override void Interact()
    {
        PlayRandomSound();
        _counter++;

        if (_counter == 5)
        {
            _poemSpawner.SpawnPoem();
        }
    }

    private void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, pianoSounds.Length);
        AudioManager.Instance.PlaySound(pianoSounds[randomIndex], 1f);
    }
}
