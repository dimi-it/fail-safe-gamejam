using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _rotationSpeed;
    private CharacterController _controller;
    private CharacterMain _characterMain;
    private Vector3 _velocity;
    private float _characterSpeed;
    private bool _isGrounded;
    private Vector3 _movement;
    private Vector2 _lookDirection;
    private bool _canDuplicate = true;
    private GameObject _child;

    private void Start()
    {
        _controller = this.GetComponent<CharacterController>();
        _characterMain = this.GetComponent<CharacterMain>();
        _child = this.transform.GetChild(0).gameObject;
        UpdateSpeed();
    }

    private void Update()
    {
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
        }
        
        _controller.Move(_movement * (Time.deltaTime * _characterSpeed));
        
        // if (_movement != Vector3.zero)
        // {
        //     gameObject.transform.forward = _movement;
        // }
        
        // if (Input.GetButtonDown("Jump") && _isGrounded)
        // {
        //     _velocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
        // }
        
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        if (_lookDirection != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(_lookDirection.x, _lookDirection.y) * Mathf.Rad2Deg;
            _child.transform.rotation = Quaternion
                .Lerp(
                    _child.transform.rotation, 
                    Quaternion.Euler(0, targetRotation, 0), 
                    _rotationSpeed * Time.deltaTime
                    );
        }
        
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        _movement.x = move.x;
        _movement.z = move.y;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookDirection = context.ReadValue<Vector2>();
    }

    public void OnPortalEnter(Portal exitPortal)
    {
        if (!_canDuplicate)
        {
            return;
        }

        _canDuplicate = false;
        _controller.enabled = false;
        this.transform.position = exitPortal.transform.position;
        _controller.enabled = true;
    }

    public void OnPortalExit()
    {
        _canDuplicate = true;
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        _characterSpeed = _movementSpeed * _characterMain.CharacterData.speed;
    }
}
