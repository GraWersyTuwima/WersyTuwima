using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    private float _horizontalInput;
    private float _verticalInput;

    private Rigidbody2D _rb;
    private Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Flip();
        _animator.SetBool("IsRunning", _rb.velocity.magnitude > 0.1f);
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
}
