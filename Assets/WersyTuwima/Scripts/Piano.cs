using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public AudioClip[] pianoSounds;

    private Collider2D _pianoTrigger;
    private BoxCollider2D _playerCollider;
    private PoemSpawner _poemSpawner;

    private int _counter = 0;

    private void Start()
    {
        _pianoTrigger = GetComponentInChildren<CapsuleCollider2D>();
        _poemSpawner = GetComponentInChildren<PoemSpawner>();
        _playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_pianoTrigger.IsTouching(_playerCollider))
            {
                PlayRandomSound();
                _counter++;

                if (_counter == 5)
                {
                    _poemSpawner.SpawnPoem();
                }
            }
        }
    }

    private void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, pianoSounds.Length);
        AudioManager.Instance.PlaySound(pianoSounds[randomIndex], 1f);
    }
}
