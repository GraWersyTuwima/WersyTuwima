using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Introduction : MonoBehaviour
{
    [SerializeField]
    private Notebook _notebook;

    private void Start()
    {
        _notebook.SetTaskNote(Notebook.Note.Wprowadzenie);
        _notebook.Toggle();

        _notebook.OnVisibilityChanged += OnNotebookVisibilityChanged;

        CanvasGroup fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<CanvasGroup>();

        fadePanel.interactable = false;
        fadePanel.blocksRaycasts = false;
        fadePanel.alpha = 1f;
        StartCoroutine(Fader.FadeComponent(fadePanel,
            (value) => fadePanel.alpha = value, null, targetValue: 0f));

        int poemFragmentsNeeded = GameObject.FindGameObjectWithTag("WszyscyDlaWszystkich").GetComponent<WszyscyDlaWszystkichLevel>().PoemFragmentsNeeded;
        GameObject.FindGameObjectWithTag("PoemCounter").GetComponent<PoemCounter>().SetFragmentsNeeded(poemFragmentsNeeded);
    }

    private void OnNotebookVisibilityChanged(bool _)
    {
        StartCoroutine(ResetNotebook());
        _notebook.OnVisibilityChanged -= OnNotebookVisibilityChanged;
    }

    private IEnumerator ResetNotebook()
    {
        yield return new WaitForSeconds(0.25f);
        _notebook.SetTaskNote(Notebook.Note.Pusta);
    }
}
