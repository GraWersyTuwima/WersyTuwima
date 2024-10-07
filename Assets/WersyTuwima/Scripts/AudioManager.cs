using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null)
                {
                    GameObject go = new("Audio Manager");
                    _instance = go.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    private AudioSource _soundSource;
    private AudioSource _musicSource;
    private Coroutine _currentSoundFade;
    private float _musicVolume = 0.5f;
    private float _soundVolume = 1f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        if (!TryGetComponent(out _soundSource))
        {
            _soundSource = gameObject.AddComponent<AudioSource>();
            _soundSource.volume = _soundVolume;
        }

        Transform musicPlayerTransform = transform.Find("Music Player");
        if (musicPlayerTransform == null)
        {
            GameObject musicPlayer = new("Music Player");
            musicPlayer.transform.parent = transform;
            _musicSource = musicPlayer.AddComponent<AudioSource>();

            _musicSource.volume = _musicVolume;
            _musicSource.playOnAwake = true;
            _musicSource.loop = true;
        }
        else
        {
            _musicSource = musicPlayerTransform.GetComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        if (_currentSoundFade != null)
        {
            StopCoroutine(_currentSoundFade);
        }

        _soundSource.volume = _soundVolume;
        _soundSource.clip = clip;
        _soundSource.Play();
    }

    public float GetSoundLength(AudioClip clip)
    {
        return clip != null ? clip.length : 0f;
    }

    public IEnumerator PlaySoundAndWait(AudioClip clip)
    {
        if (clip == null) yield break;

        PlaySound(clip);
        yield return new WaitForSeconds(clip.length);
    }

    public void StopSoundWithFade(float duration)
    {
        if (_currentSoundFade != null)
        {
            StopCoroutine(_currentSoundFade);
        }
        _currentSoundFade = StartCoroutine(FadeOutSound(duration));
    }

    private IEnumerator FadeOutSound(float duration)
    {
        float startVolume = _soundSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            _soundSource.volume = Mathf.Lerp(startVolume, 0f, (Time.time - startTime) / duration);
            yield return null;
        }

        _soundSource.Stop();
        _soundSource.volume = _soundVolume;
        _currentSoundFade = null;
    }

    public IEnumerator FadeInMusic(float duration)
    {
        float startVolume = _musicSource.volume;
        float targetVolume = _musicVolume;

        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            _musicSource.volume = Mathf.Lerp(startVolume, targetVolume, (Time.time - startTime) / duration);
            yield return null;
        }
    }

    public IEnumerator FadeOutMusic(float duration)
    {
        float startVolume = _musicSource.volume;
        float targetVolume = 0f;

        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            _musicSource.volume = Mathf.Lerp(startVolume, targetVolume, (Time.time - startTime) / duration);
            yield return null;
        }
    }
}