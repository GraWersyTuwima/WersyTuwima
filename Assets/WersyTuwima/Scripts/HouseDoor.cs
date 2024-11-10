using System.Collections;
using UnityEngine;

public class HouseDoor : InteractableObject
{
    [SerializeField]
    private Vector3 _teleportPosition;

    private CanvasGroup _fadePanel;
    private Transform _player;

    private bool _isTeleporting;

    private void Start()
    {
        _fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<CanvasGroup>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Interact()
    {
        if (_isTeleporting) return;
        _isTeleporting = true;

        StartCoroutine(Teleport(_teleportPosition));
    }

    private IEnumerator Teleport(Vector3 targetPosition)
    {
        yield return StartCoroutine(Fader.FadeComponent(_fadePanel,
            (value) => _fadePanel.alpha = value, null, duration: 0.5f, targetValue: 1f));

        yield return new WaitForSeconds(0.2f);

        _player.position = targetPosition;
        _isTeleporting = false;

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(Fader.FadeComponent(_fadePanel,
            (value) => _fadePanel.alpha = value, null, duration: 0.5f, targetValue: 0f));
    }
}