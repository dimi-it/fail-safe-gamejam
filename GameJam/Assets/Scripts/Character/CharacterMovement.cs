using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravity = -9.81f;
    private CharacterController _controller;
    private CharacterMain _characterMain;
    private Vector3 _velocity;
    private float _characterSpeed;
    private bool _isGrounded;
    private Vector3 _movement;
    private Vector3 _lookDirection;
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

        if (_lookDirection != Vector3.zero)
        {
            _child.transform.forward = _lookDirection;
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
        Vector2 look = context.ReadValue<Vector2>();
        _lookDirection.x = look.x;
        _lookDirection.z = look.y;
        Debug.Log(_lookDirection.ToString());
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
        _characterSpeed = _speed * _characterMain.CharacterData.speed;
    }
}
