using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPromptMinigame : MonoBehaviour
{
    [SerializeField]
    private Canvas _minigameCanvas;

    [SerializeField]
    private GameObject _buttonPromptPrefab;

    [SerializeField]
    private int _numberOfPrompts = 10;

    public event Action OnCorrectClick;
    public event Action OnFinish;
    public bool PlaySuccessSound { get; set; } = true;

    public void StartMinigame()
    {
        StartCoroutine(ShowPrompts());
    }

    private IEnumerator ShowPrompts()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < _numberOfPrompts; i++)
        {
            GameObject buttonPromptObject = Instantiate(_buttonPromptPrefab, _minigameCanvas.transform);
            ButtonPrompt buttonPrompt = buttonPromptObject.GetComponent<ButtonPrompt>();
            buttonPrompt.ButtonPromptMinigame = this;
            buttonPrompt.PlaySuccessSound = PlaySuccessSound;

            buttonPrompt.TimeToPressSeconds = Mathf.Lerp(1.5f, 0.9f, i / (float)_numberOfPrompts);

            yield return buttonPrompt.ShowPrompt();
            yield return new WaitForSeconds(0.5f);

            if (!buttonPrompt.IsSuccess)
            {
                i = 0;
                yield return new WaitForSeconds(0.5f);
            }
        }

        OnFinish?.Invoke();
    }

    public void InvokeCorrectClick()
    {
        OnCorrectClick?.Invoke();
    }
}
