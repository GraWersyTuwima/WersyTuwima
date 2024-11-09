using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _musicClips;

    private AudioSource _musicSource;

    private void Start()
    {
        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.volume = 0.5f;
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        int index = Random.Range(0, _musicClips.Length);
        while (true)
        {
            _musicSource.clip = _musicClips[index];
            _musicSource.Play();
            yield return new WaitForSeconds(_musicClips[index].length);
            index = (index + 1) % _musicClips.Length;
        }
    }
}
