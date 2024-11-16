using System.Collections;
using UnityEngine;

public class MouseMinigame : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _mouseSounds;

    private CanvasGroup _canvasGroup;

    private MinigameMouseHole[] _mouseHoles;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        _mouseHoles = GetComponentsInChildren<MinigameMouseHole>();
    }

    public void StartMinigame()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        StartCoroutine(Fader.FadeComponent(_canvasGroup,
            (value) => _canvasGroup.alpha = value, null, duration: 0.5f, targetValue: 1f));

        StartCoroutine(ShowMice());
    }

    private IEnumerator ShowMice()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.75f, 1.5f));

            var randomMouseHole = _mouseHoles[Random.Range(0, _mouseHoles.Length)];
            PlayMouseSound();

            randomMouseHole.Mouse.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.75f, 1.1f));
            randomMouseHole.Mouse.enabled = false;
        }
    }

    public void PlayMouseSound()
    {
        AudioClip sound = _mouseSounds[Random.Range(0, _mouseSounds.Length)];
        AudioManager.Instance.PlaySound(sound);
    }
}
