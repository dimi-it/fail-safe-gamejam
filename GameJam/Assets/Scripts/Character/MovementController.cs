using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravity = -9.81f;
    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isGrounded;
    private Vector3 _movement;

    private void Start()
    {
        _controller = this.GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
        }
        
        _controller.Move(_movement * (Time.deltaTime * _speed));

        if (_movement != Vector3.zero)
        {
            gameObject.transform.forward = _movement;
        }
        
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        _movement.x = move.x;
        _movement.z = move.y;
    }
}
