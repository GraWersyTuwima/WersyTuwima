using System.Collections;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected SpriteRenderer _spriteRenderer;
    private Coroutine _colorCoroutine;

    protected virtual bool IsInteractable { get; set; } = true;
    protected virtual Color DefaultColor => new(1, 1, 1);
    protected virtual Color HighlightColor => new(0.85f, 0.85f, 0.85f);
    protected virtual Collider2D ObjectCollider { get; set; }

    private Collider2D _playerCollider;

    public static bool AnyInteractionsEnabled { get; set; } = true;

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerCollider = GameObject.FindGameObjectWithTag("AleksCollider").GetComponent<Collider2D>();
        ObjectCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!AnyInteractionsEnabled || !IsInteractable) return;

        if (Input.GetKeyDown(KeyCode.E) && ObjectCollider.IsTouching(_playerCollider))
        {
            Interact();
        }
    }

    protected abstract void Interact();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!AnyInteractionsEnabled || !IsInteractable) return;

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