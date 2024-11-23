using System.Collections;
using UnityEngine;

public class MouseMinigame : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private int _neededScore = 15;

    [SerializeField] private AudioClip[] _mouseSounds;
    [SerializeField] private PoemSpawner _poemSpawner;


    private CanvasGroup _canvasGroup;
    private MinigameMouseHole[] _mouseHoles;
    private Coroutine _showMiceCoroutine;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        _mouseHoles = GetComponentsInChildren<MinigameMouseHole>();

        foreach (var mouseHole in _mouseHoles)
        {
            mouseHole.OnMouseClick += () => OnMouseClick(mouseHole);
        }
    }

    public void OnMouseClick(MinigameMouseHole mouseHole)
    {
        _score++;

        PlayMouseSound();
        StartCoroutine(Fader.FadeComponent(mouseHole.Mouse,
            (value) => mouseHole.Mouse.color = new Color(1, 1, 1, value),
            () => mouseHole.Mouse.enabled = false, duration: 0.10f, targetValue: 0f));

        if (_score >= _neededScore)
        {
            Win();
        }
    }

    public void Win()
    {
        if (_showMiceCoroutine != null) StopCoroutine(_showMiceCoroutine);

        InteractableObject.AnyInteractionsEnabled = true;
        AlexController.Instance.CanMove = true;
        StartCoroutine(Fader.FadeComponent(_canvasGroup,
            (value) => _canvasGroup.alpha = value, null, duration: 0.5f, targetValue: 0f));

        _poemSpawner.SpawnPoem();
    }

    public void StartMinigame()
    {
        InteractableObject.AnyInteractionsEnabled = false;
        AlexController.Instance.CanMove = false;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        StartCoroutine(Fader.FadeComponent(_canvasGroup,
            (value) => _canvasGroup.alpha = value, null, duration: 0.5f, targetValue: 1f));

        _showMiceCoroutine = StartCoroutine(ShowMice());
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
