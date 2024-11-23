using UnityEngine;
using UnityEngine.UI;

public class Introduction : MonoBehaviour
{
    [SerializeField]
    private Notebook _notebook;

    [SerializeField]
    [TextArea(3, 10)]
    private string _introductionText;

    private void Start()
    {
        _notebook.SetText(_introductionText);
        _notebook.Toggle();

        CanvasGroup fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<CanvasGroup>();

        fadePanel.interactable = false;
        fadePanel.blocksRaycasts = false;
        fadePanel.alpha = 1f;
        StartCoroutine(Fader.FadeComponent(fadePanel,
            (value) => fadePanel.alpha = value, null, targetValue: 0f));
    }
}
