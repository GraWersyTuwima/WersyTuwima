using System.Collections;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected SpriteRenderer _spriteRenderer;
    private Coroutine _colorCoroutine;

    protected virtual Color DefaultColor => new(1, 1, 1);
    protected virtual Color HighlightColor => new(0.75f, 0.75f, 0.75f);

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    protected abstract void Interact();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AleksCollider"))
        {
            RunColorChangeCoroutine(HighlightColor);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("AleksCollider"))
        {
            RunColorChangeCoroutine(DefaultColor);
        }
    }

    private void RunColorChangeCoroutine(Color color)
    {
        if (_colorCoroutine != null)
        {
            StopCoroutine(_colorCoroutine);
        }
        _colorCoroutine = StartCoroutine(ChangeColor(color));
    }

    private IEnumerator ChangeColor(Color color)
    {
        Color currentColor = _spriteRenderer.color;
        float time = 0;
        float duration = 0.2f;
        while (time < duration)
        {
            time += Time.deltaTime;
            _spriteRenderer.color = Color.Lerp(currentColor, color, time / duration);
            yield return null;
        }

        _colorCoroutine = null;
    }
}