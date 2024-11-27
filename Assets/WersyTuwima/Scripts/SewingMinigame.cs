using UnityEngine;
using UnityEngine.UI;

public class SewingMinigame : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    [SerializeField] private Image _shirtOutline;
    [SerializeField] private Image _shirt;
    [SerializeField] private LineRenderer _lineRenderer;

    public event System.Action OnMinigameComplete;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _shirtOutline.gameObject.SetActive(false);
        _lineRenderer.gameObject.SetActive(false);
    }

    public void Run()
    {
        InteractableObject.AnyInteractionsEnabled = false;
        AlexController.Instance.CanMove = false;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _shirtOutline.gameObject.SetActive(true);
        _lineRenderer.gameObject.SetActive(true);

        StartCoroutine(Fader.FadeComponent(_canvasGroup,
            (value) => _canvasGroup.alpha = value, null, duration: 0.5f, targetValue: 1f));
    }

    public void CompleteMinigame()
    {
        StartCoroutine(CompleteMinigameRoutine());
    }

    private System.Collections.IEnumerator CompleteMinigameRoutine()
    {
        yield return StartCoroutine(Fader.FadeComponent(_shirtOutline,
            (value) =>
            {
                _shirtOutline.color = new Color(_shirtOutline.color.r, _shirtOutline.color.g, _shirtOutline.color.b, value);
                Color lineColor = new(_lineRenderer.startColor.r, _lineRenderer.startColor.g, _lineRenderer.startColor.b, value);
                _lineRenderer.startColor = lineColor;
                _lineRenderer.endColor = lineColor;
            },
            null, duration: 0.75f, targetValue: 0f));

        _shirt.gameObject.SetActive(true);
        _shirt.color = new Color(_shirt.color.r, _shirt.color.g, _shirt.color.b, 0);
        yield return StartCoroutine(Fader.FadeComponent(_shirt,
            (value) => _shirt.color = new Color(_shirt.color.r, _shirt.color.g, _shirt.color.b, value),
            null, duration: 0.75f, targetValue: 1f));

        yield return new WaitForSeconds(1f);

        InteractableObject.AnyInteractionsEnabled = true;
        AlexController.Instance.CanMove = true;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        StartCoroutine(Fader.FadeComponent(_canvasGroup,
            (value) => _canvasGroup.alpha = value, () =>
            {
                _shirtOutline.gameObject.SetActive(false);
                _lineRenderer.gameObject.SetActive(false);
            }, duration: 0.5f, targetValue: 0f));

        OnMinigameComplete?.Invoke();
    }
}
