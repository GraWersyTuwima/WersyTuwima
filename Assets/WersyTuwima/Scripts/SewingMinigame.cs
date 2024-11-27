using UnityEngine;

public class SewingMinigame : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _shirt;
    [SerializeField] private GameObject _lineRenderer;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _shirt.SetActive(false);
        _lineRenderer.SetActive(false);
    }

    public void Run()
    {
        InteractableObject.AnyInteractionsEnabled = false;
        AlexController.Instance.CanMove = false;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _shirt.SetActive(true);
        _lineRenderer.SetActive(true);

        StartCoroutine(Fader.FadeComponent(_canvasGroup,
            (value) => _canvasGroup.alpha = value, null, duration: 0.5f, targetValue: 1f));
    }

    public void CompleteMinigame()
    {
        InteractableObject.AnyInteractionsEnabled = true;
        AlexController.Instance.CanMove = true;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        StartCoroutine(Fader.FadeComponent(_canvasGroup,
            (value) => _canvasGroup.alpha = value, () =>
            {
                _shirt.SetActive(false);
                _lineRenderer.SetActive(false);
            }, duration: 0.5f, targetValue: 0f));
    }
}
