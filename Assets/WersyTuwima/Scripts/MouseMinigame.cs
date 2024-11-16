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
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));

            var mouseHole = _mouseHoles[Random.Range(0, _mouseHoles.Length)];
            PlayMouseSound();

            mouseHole.Mouse.color = new Color(1, 1, 1, 0);
            mouseHole.Mouse.enabled = true;
            StartCoroutine(Fader.FadeComponent(mouseHole.Mouse,
                (value) => mouseHole.Mouse.color = new Color(1, 1, 1, value), null, duration: 0.10f, targetValue: 1f));

            yield return new WaitForSeconds(Random.Range(0.7f, 1f));
            
            StartCoroutine(Fader.FadeComponent(mouseHole.Mouse,
                (value) => mouseHole.Mouse.color = new Color(1, 1, 1, value), 
                () => mouseHole.Mouse.enabled = false, duration: 0.10f, targetValue: 0f));
        }
    }

    public void PlayMouseSound()
    {
        AudioClip sound = _mouseSounds[Random.Range(0, _mouseSounds.Length)];
        AudioManager.Instance.PlaySound(sound);
    }
}
