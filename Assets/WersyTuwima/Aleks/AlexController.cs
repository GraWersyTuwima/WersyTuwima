using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    private float _horizontalInput;
    private float _verticalInput;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Move();
        Flip();
    }

    private void Move()
    {
        Vector3 direction = new(_horizontalInput, _verticalInput, 0);
        if (direction.magnitude > 1f) direction.Normalize();
        transform.position += _speed * Time.deltaTime * direction;

        _animator.SetBool("IsRunning", direction.magnitude > 0);
    }

    private void Flip()
    {
        if (_horizontalInput == 0) return;
        transform.localScale = new Vector3(Mathf.Sign(_horizontalInput), 1, 1);
    }
}
