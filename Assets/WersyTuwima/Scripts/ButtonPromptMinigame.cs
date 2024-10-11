using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPromptMinigame : MonoBehaviour
{
    [SerializeField]
    private GameObject _buttonPromptPrefab;

    [SerializeField]
    private int _numberOfPrompts = 10;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Insert))
        {
            StartMinigame();
        }
    }

    private void StartMinigame()
    {
        StartCoroutine(ShowPrompts());
    }

    private IEnumerator ShowPrompts()
    {
        for (int i = 0; i < _numberOfPrompts; i++)
        {
            GameObject buttonPromptObject = Instantiate(_buttonPromptPrefab, transform);
            ButtonPrompt buttonPrompt = buttonPromptObject.GetComponent<ButtonPrompt>();

            buttonPrompt.TimeToPressSeconds = Mathf.Lerp(1.5f, 0.9f, i / (float)_numberOfPrompts);

            buttonPrompt.ShowPrompt();
            yield return new WaitForSeconds(buttonPrompt.TimeToPressSeconds + 0.15f);

            if (!buttonPrompt.IsSuccess)
            {
                i = 0;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
