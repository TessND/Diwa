using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private Rigidbody2D _rigidBody;
    private InputMapping _input;
    private Vector2 _inputVector;
    private Vector2 _movementVector;
    private Vector2 _velocity;
    

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _input = new InputMapping();
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _inputVector = GetMovementInput();
        _movementVector = Vector2.SmoothDamp(_movementVector, _inputVector, ref _velocity, 0.1f);
        _rigidBody.velocity = _movementVector * _movementSpeed;
    }

    private Vector2 GetMovementInput()
    {
        return _input.Gameplay.Movement.ReadValue<Vector2>();
    }


    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

}

