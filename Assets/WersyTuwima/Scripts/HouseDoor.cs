using System.Collections;
using UnityEngine;

public class HouseDoor : InteractableObject
{
    [SerializeField]
    private Vector3 _teleportPosition;

    [SerializeField]
    private bool _isEntrance;

    private CanvasGroup _fadePanel;
    private Transform _player;
    private HouseLevel _houseLevel;

    private bool _isTeleporting;

    private void Start()
    {
        _fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<CanvasGroup>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _houseLevel = GameObject.FindGameObjectWithTag("HouseLevel").GetComponent<HouseLevel>();
    }

    protected override void Interact()
    {
        if (_isTeleporting) return;

        StartCoroutine(Teleport(_teleportPosition));
    }

    private IEnumerator Teleport(Vector3 targetPosition)
    {
        _isTeleporting = true;

        yield return StartCoroutine(Fader.FadeComponent(_fadePanel,
            (value) => _fadePanel.alpha = value, null, duration: 0.35f, targetValue: 1f));

        yield return new WaitForSeconds(0.1f);

        _player.position = targetPosition;
        HandleDoorTransition();

        yield return new WaitForSeconds(0.1f);

        yield return StartCoroutine(Fader.FadeComponent(_fadePanel,
            (value) => _fadePanel.alpha = value, null, duration: 0.35f, targetValue: 0f));

        _isTeleporting = false;
    }

    private void HandleDoorTransition()
    {
        if (_isEntrance)
        {
            _houseLevel.Enter();
        }
        else
        {
            _houseLevel.Exit();
        }
    }
}