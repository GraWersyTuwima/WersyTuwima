using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private AudioClip[] _grassFootstepSounds;

    [SerializeField]
    private AudioClip[] _planksFootstepSounds;

    private float _horizontalInput;
    private float _verticalInput;

    private Rigidbody2D _rb;
    private Animator _animator;

    private bool _isRunning;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        StartCoroutine(PlayFootstepSound());
    }

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Flip();

        _isRunning = _rb.velocity.magnitude > 0.1f;
        _animator.SetBool("IsRunning", _isRunning);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = new(_horizontalInput, _verticalInput);
        if (direction.magnitude > 1f) direction.Normalize();

        Vector2 movement = direction * _speed;
        _rb.velocity = movement;
    }

    private void Flip()
    {
        if (_horizontalInput == 0) return;
        transform.localScale = new Vector3(Mathf.Sign(_horizontalInput), 1, 1);
    }

    private IEnumerator PlayFootstepSound()
    {
        while (true)
        {
            if (_isRunning)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, 1.7f, 0),
                    Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

                if (hit.collider == null)
                {
                    yield return null;
                    continue;
                }

                AudioClip[] footstepSounds = hit.collider.CompareTag("Grass") ? _grassFootstepSounds : _planksFootstepSounds;

                int randomIndex = Random.Range(0, footstepSounds.Length);
                AudioManager.Instance.PlaySound(footstepSounds[randomIndex]);

                yield return new WaitForSeconds(0.35f);
            }
            else
            {
                yield return null;
            }
        }
    }
}
