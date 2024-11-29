using UnityEngine;

public class Thread : InteractableObject
{
    [SerializeField]
    private SewingMinigame _sewingMinigame;

    private PoemSpawner _poemSpawner;
    private Notebook _notebook;

    protected override bool IsInteractable => _sewingMinigame != null;

    private void Start()
    {
        _poemSpawner = GetComponentInChildren<PoemSpawner>();
        _notebook = GameObject.FindGameObjectWithTag("Notebook").GetComponent<Notebook>();
    }

    protected override void Interact()
    {
        _sewingMinigame.OnMinigameComplete += () =>
        {
            _sewingMinigame = null;
            _notebook.AddPage(Notebook.Note.Tuwim1);
            _poemSpawner.SpawnPoem();
        };

        _sewingMinigame.Run();
    }
}
