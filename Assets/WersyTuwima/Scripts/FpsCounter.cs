using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();

        StartCoroutine(UpdateFps());
    }

    private IEnumerator UpdateFps()
    {
        while (true)
        {
            _text.text = (1.0f / Time.deltaTime).ToString("F0");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
