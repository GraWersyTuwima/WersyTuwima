using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public AudioClip[] pianoSounds;

    private Collider2D _pianoCollider;
    private BoxCollider2D _playerCollider;

    private void Start()
    {
        _pianoCollider = GetComponent<Collider2D>();
        _playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_pianoCollider.IsTouching(_playerCollider))
            {
                PlayRandomSound();
            }
        }
    }

    private void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, pianoSounds.Length);
        AudioManager.Instance.PlaySound(pianoSounds[randomIndex]);
    }
}
