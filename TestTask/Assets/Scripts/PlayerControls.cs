using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Transform checkGroundPosition;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float playerSpeed;

    private Rigidbody _rigidbody;

    private Vector2 _moveInput;
    private bool _isGrounded { get => IsGrounded(); }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isGrounded)
            Move();
    }

    private void OnInput(InputValue inputValue)
    {
        _moveInput = inputValue.Get<Vector2>();
    }

    private void OnAttack()
    {

    }


    private void Move()
    {
        Vector3 velocity = Vector3.forward * _moveInput.y + Vector3.right * _moveInput.x;
        velocity *= playerSpeed; 
        velocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = velocity;
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(checkGroundPosition.position, Vector3.down, groundCheckDistance);
    }
}
