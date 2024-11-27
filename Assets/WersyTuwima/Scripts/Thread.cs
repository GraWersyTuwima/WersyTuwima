using UnityEngine;

public class Thread : InteractableObject
{
    [SerializeField]
    private SewingMinigame _sewingMinigame;

    private PoemSpawner _poemSpawner;

    protected override bool IsInteractable => _sewingMinigame != null;

    private void Start()
    {
        _poemSpawner = GetComponentInChildren<PoemSpawner>();
    }

    protected override void Interact()
    {
        _sewingMinigame.OnMinigameComplete += () =>
        {
            _sewingMinigame = null;
            _poemSpawner.SpawnPoem();
        };

        _sewingMinigame.Run();
    }
}
