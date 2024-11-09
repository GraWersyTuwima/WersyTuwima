using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _mainMenuCanvasGroup;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = MenuMusicManager.Instance.GetComponent<AudioSource>();
    }

    public void Play()
    {
        _mainMenuCanvasGroup.interactable = false;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        StartCoroutine(Fader.FadeComponent(_mainMenuCanvasGroup,
            (value) => _mainMenuCanvasGroup.alpha = value, null));

        StartCoroutine(Fader.FadeComponent(_audioSource,
            (value) => _audioSource.volume = value,
            () => _audioSource.Stop()));

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
