using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

// This script uses the new Unity Input System for obtaining key inputs.
public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    public event EventHandler OnInteractAction;
    public event EventHandler<OnScrollActionEventArgs> OnScrollAction;

    public class OnScrollActionEventArgs : EventArgs
    {
        public int scrollSign;
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Scroll.performed += Scroll_performed;
    }

    // Scrolling Input
    private void Scroll_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnScrollAction?.Invoke(this, new OnScrollActionEventArgs
        {
            scrollSign = GetScrollInputSign()
        });
    }

    // Interaction input
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    // Movement input, queried whenever this function is called, typically every frame
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    // Reduces scroll input to a sign, note that its values are typically 120, 0, or -120 (at least on fez's end)
    private int GetScrollInputSign()
    {
        float axis = playerInputActions.Player.Scroll.ReadValue<float>();

        if (axis != 0)
        {
            axis = Mathf.Sign(axis);
        }

        return (int)axis;
    }
}
