using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    private float _horizontalInput;
    private float _verticalInput;

    void Start()
    {
        
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
    }

    private void Flip()
    {
        if (_horizontalInput == 0) return;
        transform.localScale = new Vector3(Mathf.Sign(_horizontalInput), 1, 1);
    }
}
