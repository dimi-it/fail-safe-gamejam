using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, PlayerInputAction.ITestMapActions
{
    private PlayerInputAction _playerInputAction;

    public static event Action<Vector2> OnMovePerformed; 
    private void OnEnable()
    {
        if(_playerInputAction != null)
            return;

        _playerInputAction = new PlayerInputAction();
        _playerInputAction.TestMap.SetCallbacks(this);
        _playerInputAction.TestMap.Enable();
        Debug.Log("Input manager ENABLED");
    }

    private void OnDisable()
    {
        _playerInputAction.TestMap.Disable();
        Debug.Log("Input manager DISABLED");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMovePerformed?.Invoke(context.ReadValue<Vector2>());
        Debug.Log(context.ReadValue<Vector2>().ToString());
    }
}
