using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _musicClips;

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<AudioManager>();
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
    private float _musicVolume = 0.4f;
    private float _soundVolume = 0.8f;

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

        InitializeMusicPlayer();
    }

    private void InitializeMusicPlayer()
    {
        Transform musicPlayerTransform = transform.Find("Music Player");
        if (musicPlayerTransform != null)
        {
            _musicSource = musicPlayerTransform.GetComponent<AudioSource>();
            return;
        }

        GameObject musicPlayer = new("Music Player");
        musicPlayer.transform.parent = transform;
        _musicSource = musicPlayer.AddComponent<AudioSource>();

        _musicSource.volume = _musicVolume;
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        int lastClipIndex = -1;
        while (true)
        {
            int clipIndex = Random.Range(0, _musicClips.Length);
            if (clipIndex == lastClipIndex)
            {
                clipIndex = (clipIndex + 1) % _musicClips.Length;
            }

            lastClipIndex = clipIndex;
            _musicSource.clip = _musicClips[clipIndex];
            _musicSource.Play();

            yield return new WaitForSeconds(_musicSource.clip.length);
        }
    }

    public AudioSource PlaySound(AudioClip clip, float? volume = null)
    {
        if (clip == null) return null;

        GameObject soundGameObject = new("Sound");
        AudioSource soundSource = soundGameObject.AddComponent<AudioSource>();
        soundGameObject.transform.SetParent(transform);

        soundSource.volume = volume ?? _soundVolume;
        soundSource.PlayOneShot(clip);
        Destroy(soundGameObject, clip.length);

        return soundSource;
    }

    public IEnumerator FadeAudioSource(AudioSource source, float duration, float targetVolume)
    {
        float startVolume = source.volume;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            source.volume = Mathf.Lerp(startVolume, targetVolume, (Time.time - startTime) / duration);
            yield return null;
        }
    }

    public IEnumerator FadeMusic(float duration, bool fadeIn) => FadeAudioSource(_musicSource, duration, fadeIn ? _musicVolume : 0f);
    public IEnumerator FadeOutSound(AudioSource soundSource, float duration) => FadeAudioSource(soundSource, duration, 0f);
}