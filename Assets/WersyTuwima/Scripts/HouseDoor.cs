using UnityEngine;

public class HouseDoor : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AleksCollider"))
        {
            Debug.Log("AleksCollider");
            _spriteRenderer.color = new Color(0.9f, 0.9f, 0.9f, 1);
        }
    }
}
